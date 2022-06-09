using Firebase.Auth;
using ProlanceMaui.DBConnect;

namespace ProlanceMaui.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        try
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAerXOP2_XncVWxe9QSuJlc5J1_STr77hM"));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(InputEmail.Text.Trim(), InputPassword.Text.Trim());
            if (auth.User != null)
            {
                App.Current.MainPage = new AppShell();
            }
        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Error", ex.Reason.ToString(), "Got it");
        }
    }
}