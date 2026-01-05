using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevGearbox.Utils;

namespace DevGearbox.Components;

public partial class WindowsCleanerView : UserControl
{
    private List<CleanupItemViewModel> _items = new();
    private WindowsCleaner.CleanupResult? _lastScanResult;
    private GridViewColumnHeader? _lastHeaderClicked;
    private ListSortDirection _lastDirection = ListSortDirection.Ascending;

    public WindowsCleanerView()
    {
        InitializeComponent();
    }

    private async void ScanButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Disable buttons and show progress
            ScanButton.IsEnabled = false;
            CleanButton.IsEnabled = false;
            SelectAllButton.IsEnabled = false;
            DeselectAllButton.IsEnabled = false;
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
            StatusText.Text = "Scanning for temporary files and caches...";
            EmptyStateText.Visibility = Visibility.Collapsed;

            // Clear previous results
            _items.Clear();
            ResultsListView.ItemsSource = null;

            // Build scan settings from UI
            var settings = new WindowsCleaner.ScanSettings
            {
                IncludeWindowsTemp = IncludeWindowsTempCheckBox.IsChecked == true,
                IncludePrefetch = IncludePrefetchCheckBox.IsChecked == true,
                IncludeRecycleBin = IncludeRecycleBinCheckBox.IsChecked == true,
                IncludeBrowserCaches = IncludeBrowserCachesCheckBox.IsChecked == true
            };

            // Perform scan in background
            var result = await Task.Run(() => WindowsCleaner.ScanForCleanup(settings));
            _lastScanResult = result;

            // Convert to view models
            _items = result.Items.Select(item => new CleanupItemViewModel
            {
                Path = item.Path,
                Size = item.Size,
                SizeFormatted = WindowsCleaner.FormatBytes(item.Size),
                Category = item.Category,
                CanDelete = item.CanDelete,
                StatusText = item.CanDelete ? "Ready" : item.ErrorMessage
            }).ToList();

            // Update UI
            ResultsListView.ItemsSource = _items;
            TotalFilesText.Text = result.TotalFiles.ToString();
            TotalSizeText.Text = WindowsCleaner.FormatBytes(result.TotalSize);
            
            // Show errors if any
            if (result.Errors.Any())
            {
                var errorSummary = $"Found {result.TotalFiles} items ({WindowsCleaner.FormatBytes(result.TotalSize)}). " +
                                 $"{result.Errors.Count} location(s) could not be scanned (may require admin).";
                StatusText.Text = errorSummary;
            }
            else
            {
                StatusText.Text = $"Scan complete: Found {result.TotalFiles} items ({WindowsCleaner.FormatBytes(result.TotalSize)})";
            }

            // Enable buttons
            SelectAllButton.IsEnabled = _items.Any();
            DeselectAllButton.IsEnabled = _items.Any();
            CleanButton.IsEnabled = false; // Enable only when items are selected

            // Show empty state if no items
            if (!_items.Any())
            {
                EmptyStateText.Text = "No temporary files found. Your system is clean!";
                EmptyStateText.Visibility = Visibility.Visible;
            }
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error during scan: {ex.Message}";
            MessageBox.Show($"Error scanning for cleanup items:\n\n{ex.Message}", "Scan Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            ScanButton.IsEnabled = true;
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Visibility = Visibility.Collapsed;
        }
    }

    private async void CleanButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedItems = ResultsListView.SelectedItems.Cast<CleanupItemViewModel>().ToList();
        
        if (!selectedItems.Any())
        {
            MessageBox.Show("Please select items to clean.", "No Selection", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        // Confirm deletion
        var totalSize = WindowsCleaner.FormatBytes(selectedItems.Sum(i => i.Size));
        var message = $"Are you sure you want to delete {selectedItems.Count} item(s) ({totalSize})?\n\n" +
                     "This action cannot be undone.";
        
        var confirmResult = MessageBox.Show(message, "Confirm Cleanup", 
            MessageBoxButton.YesNo, MessageBoxImage.Warning);
        
        if (confirmResult != MessageBoxResult.Yes)
            return;

        try
        {
            // Disable buttons and show progress
            ScanButton.IsEnabled = false;
            CleanButton.IsEnabled = false;
            SelectAllButton.IsEnabled = false;
            DeselectAllButton.IsEnabled = false;
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
            StatusText.Text = "Cleaning up selected items...";

            // Convert view models back to cleanup items
            var itemsToDelete = selectedItems.Select(vm => new WindowsCleaner.CleanupItem
            {
                Path = vm.Path,
                Size = vm.Size,
                Category = vm.Category,
                CanDelete = vm.CanDelete
            }).ToList();

            // Perform cleanup in background
            var result = await Task.Run(() => WindowsCleaner.PerformCleanup(itemsToDelete));

            // Remove deleted items from the list
            foreach (var item in selectedItems)
            {
                _items.Remove(item);
            }
            
            // Refresh the list view
            ResultsListView.ItemsSource = null;
            ResultsListView.ItemsSource = _items;

            // Update totals
            var remainingSize = _items.Sum(i => i.Size);
            TotalFilesText.Text = _items.Count.ToString();
            TotalSizeText.Text = WindowsCleaner.FormatBytes(remainingSize);
            SelectedText.Text = "0 items (0 B)";

            // Show summary
            var summary = $"Cleanup complete: Deleted {result.DeletedFiles} items ({WindowsCleaner.FormatBytes(result.DeletedSize)})";
            if (result.Errors.Any())
            {
                summary += $"\n{result.Errors.Count} item(s) could not be deleted (in use or locked).";
            }
            StatusText.Text = summary;

            MessageBox.Show(summary, "Cleanup Complete", 
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Show empty state if no items remain
            if (!_items.Any())
            {
                EmptyStateText.Text = "All items cleaned! Your system is now cleaner.";
                EmptyStateText.Visibility = Visibility.Visible;
            }
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error during cleanup: {ex.Message}";
            MessageBox.Show($"Error during cleanup:\n\n{ex.Message}", "Cleanup Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            ScanButton.IsEnabled = true;
            SelectAllButton.IsEnabled = _items.Any();
            DeselectAllButton.IsEnabled = _items.Any();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Visibility = Visibility.Collapsed;
        }
    }

    private void SelectAllButton_Click(object sender, RoutedEventArgs e)
    {
        ResultsListView.SelectAll();
    }

    private void DeselectAllButton_Click(object sender, RoutedEventArgs e)
    {
        ResultsListView.SelectedItems.Clear();
    }

    private void ResultsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItems = ResultsListView.SelectedItems.Cast<CleanupItemViewModel>().ToList();
        var selectedSize = selectedItems.Sum(i => i.Size);
        
        SelectedText.Text = $"{selectedItems.Count} items ({WindowsCleaner.FormatBytes(selectedSize)})";
        CleanButton.IsEnabled = selectedItems.Any();
    }

    private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        var headerClicked = e.OriginalSource as GridViewColumnHeader;
        if (headerClicked == null || headerClicked.Role == GridViewColumnHeaderRole.Padding)
            return;

        ListSortDirection direction;

        if (headerClicked != _lastHeaderClicked)
        {
            direction = ListSortDirection.Ascending;
        }
        else
        {
            direction = _lastDirection == ListSortDirection.Ascending 
                ? ListSortDirection.Descending 
                : ListSortDirection.Ascending;
        }

        var columnBinding = headerClicked.Tag as string;
        if (string.IsNullOrEmpty(columnBinding))
            return;

        Sort(columnBinding, direction);

        // Update header visual indicator
        if (_lastHeaderClicked != null)
        {
            _lastHeaderClicked.Content = _lastHeaderClicked.Content.ToString()?.Replace(" ▲", "").Replace(" ▼", "");
        }

        headerClicked.Content = headerClicked.Content.ToString() + (direction == ListSortDirection.Ascending ? " ▲" : " ▼");

        _lastHeaderClicked = headerClicked;
        _lastDirection = direction;
    }

    private void Sort(string sortBy, ListSortDirection direction)
    {
        var dataView = CollectionViewSource.GetDefaultView(ResultsListView.ItemsSource);
        if (dataView == null)
            return;

        dataView.SortDescriptions.Clear();

        // For Size column, sort by the numeric Size property instead of formatted string
        if (sortBy == "Size")
        {
            dataView.SortDescriptions.Add(new SortDescription("Size", direction));
        }
        else
        {
            dataView.SortDescriptions.Add(new SortDescription(sortBy, direction));
        }

        dataView.Refresh();
    }
}

public class CleanupItemViewModel
{
    public string Path { get; set; } = string.Empty;
    public long Size { get; set; }
    public string SizeFormatted { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool CanDelete { get; set; }
    public string StatusText { get; set; } = string.Empty;
}
