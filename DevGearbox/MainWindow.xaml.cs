using System.Windows;
using System.Windows.Controls;
using DevGearbox.Services;
using Wpf.Ui.Controls;
using System.Linq;
using DevGearbox.Models;

namespace DevGearbox;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    private List<ToolItem> _allTools;
    private List<RadioButton> _allNavButtons;

    public MainWindow()
    {
        InitializeComponent();
        _allTools = new List<ToolItem>();
        _allNavButtons = new List<RadioButton>();
        LoadTools();
    }

    /// <summary>
    /// Dynamically load all tools from the factory and create navigation
    /// </summary>
    private void LoadTools()
    {
        _allTools = ToolFactory.Instance.GetAllTools().ToList();

        foreach (var tool in _allTools)
        {
            // Create navigation button
            var navButton = new RadioButton
            {
                Content = tool.Name,
                Tag = tool.Icon,
                Style = (Style)FindResource("NavItemStyle"),
                ToolTip = tool.Description
            };

            // Handle selection
            navButton.Checked += (s, e) => NavigateToTool(tool);

            _allNavButtons.Add(navButton);
            NavigationPanel.Children.Add(navButton);
        }

        // Select the first tool by default
        if (_allNavButtons.Count > 0)
        {
            _allNavButtons[0].IsChecked = true;
        }
    }

    /// <summary>
    /// Navigate to a specific tool
    /// </summary>
    private void NavigateToTool(ToolItem tool)
    {
        // Update header
        ToolIconText.Text = tool.Icon;
        ToolNameText.Text = tool.Name;
        ToolDescriptionText.Text = tool.Description;

        // Update content
        ToolContentControl.Content = tool.View;

        // Update status
        StatusText.Text = $"{tool.Icon} {tool.Name} - Ready";
    }

    /// <summary>
    /// Search/filter tools
    /// </summary>
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = SearchBox.Text.ToLower();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Show all tools
            foreach (var btn in _allNavButtons)
            {
                btn.Visibility = Visibility.Visible;
            }
        }
        else
        {
            // Filter tools
            for (int i = 0; i < _allNavButtons.Count; i++)
            {
                var tool = _allTools[i];
                var button = _allNavButtons[i];

                var matches = tool.Name.ToLower().Contains(searchText) ||
                             tool.Description.ToLower().Contains(searchText);

                button.Visibility = matches ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
