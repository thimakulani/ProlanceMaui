using ProlanceMaui.Pages;

namespace ProlanceMaui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new LoginPage();
	}
}
