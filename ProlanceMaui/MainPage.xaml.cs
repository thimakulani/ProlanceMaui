using ProlanceMaui.DBConnect;

namespace ProlanceMaui;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		getStats();

    }
	public async void getStats()
	{
		FirebaseConnect firebaseConnect = new FirebaseConnect();
		var sp_query = await firebaseConnect.GetFirestoreDBconnection()
			.Collection("USERS")
			.WhereEqualTo("Role", "S").GetSnapshotAsync();
		var c_query = await firebaseConnect.GetFirestoreDBconnection()
			.Collection("USERS")
			.WhereEqualTo("Role", "C").GetSnapshotAsync();

        var services = await firebaseConnect.GetFirestoreDBconnection()
            .Collection("SERVICES").GetSnapshotAsync();

        var categories = await firebaseConnect.GetFirestoreDBconnection()
			.Collection("CATEGORIES").GetSnapshotAsync();

		CountClients.Text = c_query.Count.ToString();
		CountServiceCategories.Text = categories.Count.ToString();
		CountServiceProviders.Text = sp_query.Count.ToString();
		CountServices.Text = services.Count.ToString();


    }
}

