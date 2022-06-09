using ProlanceMaui.Pages;

namespace ProlanceMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddCategoryDialog), typeof(AddCategoryDialog));
    }
}
