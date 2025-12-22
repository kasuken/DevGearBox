using System.Windows.Controls;
namespace DevGearbox.Models;
public class ToolItem
{
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UserControl? View { get; set; }
    public ToolItem(string name, string icon, string description, UserControl view)
    {
        Name = name;
        Icon = icon;
        Description = description;
        View = view;
    }
}
