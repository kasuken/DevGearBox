using System.Windows;
using System.Windows.Controls;
using DevGearbox.Services;

namespace DevGearbox;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LoadTools();
    }

    /// <summary>
    /// Dynamically load all tools from the factory
    /// </summary>
    private void LoadTools()
    {
        var tools = ToolFactory.Instance.GetAllTools();

        foreach (var tool in tools)
        {
            var tabItem = new TabItem
            {
                Header = $"{tool.Icon} {tool.Name}",
                ToolTip = tool.Description,
                Content = tool.View
            };

            MainTabControl.Items.Add(tabItem);
        }

        // Select the first tab by default
        if (MainTabControl.Items.Count > 0)
        {
            MainTabControl.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Handle tab selection changes (optional - for future enhancements)
    /// </summary>
    private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // This can be used for lazy loading or analytics in the future
        // For now, all components are pre-loaded
    }
}

