using ProlanceMaui.DBConnect;
using ProlanceMaui.Models;
using System.Collections.ObjectModel;

namespace ProlanceMaui.Pages;

public partial class UsersPage : ContentPage
{
	public UsersPage()
	{
		InitializeComponent();
        _= UserData();

    }
    private ObservableCollection<Users> users = new ObservableCollection<Users>();
    public ObservableCollection<Users> Users { get { return users; } set { users = value; } }
	private async Task UserData()
    {
        LstUsers.BindingContext = this;
        //LstUsers.ItemsSource = Users;
        FirebaseConnect dbconnect = new FirebaseConnect();
        try
        {
            var query = await dbconnect.GetFirestoreDBconnection()
            .Collection("USERS")
            .GetSnapshotAsync();
            query.Query.Listen(v =>
            {
                if (v != null)
                {
                    foreach (var item in v.Changes)
                    {
                        var user = item.Document.ConvertTo<Users>();
                        switch (item.ChangeType)
                        {
                            case Google.Cloud.Firestore.DocumentChange.Type.Added:

                                 users.Add(user);
                                break;
                            case Google.Cloud.Firestore.DocumentChange.Type.Removed:
                                break;
                            case Google.Cloud.Firestore.DocumentChange.Type.Modified:
                                break;
                            default:
                                break;
                        }
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}