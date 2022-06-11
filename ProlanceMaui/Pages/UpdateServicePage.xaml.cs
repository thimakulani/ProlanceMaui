using ProlanceMaui.DBConnect;
using ProlanceMaui.Models;

namespace ProlanceMaui.Pages;

public partial class UpdateServicePage : ContentPage
{
	private string id = null;
	public UpdateServicePage(string v)
	{
		InitializeComponent();
		id = v;
        SID.Text = $"ID: {id}";
        _ = GetName();
    }
    private async Task GetName()
    {
        try
        {
            FirebaseConnect db = new FirebaseConnect();
            var query = await db.GetFirestoreDBconnection()
                .Collection("SERVICES")
                .Document(id).GetSnapshotAsync();
            var s = query.ConvertTo<Services>();
            OldServiceName.Text = $"CURRENT SERVICE NAME: {s.Name}";

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void BtnUpdate_Clicked(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(NewServiceName.Text))
        {
            FirebaseConnect db = new FirebaseConnect();
            await db.GetFirestoreDBconnection()
                .Collection("SERVICES")
                .Document(id)
                .UpdateAsync("Name", NewServiceName.Text);
            await DisplayAlert("SUCCESSFUL", "Successfully updated", "Got it");

        }
    }

    private async void ImgBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}