using ProlanceMaui.DBConnect;
using ProlanceMaui.Models;
using System.Collections.ObjectModel;

namespace ProlanceMaui.Pages;

public partial class ServicesPage : ContentPage
{
	public ServicesPage()
	{
		InitializeComponent();
        _=LoadCategories();

    }
    private ObservableCollection<Services> services = new ObservableCollection<Services>();
    public ObservableCollection<Services> Services { get { return services; } set { services = value; } }
    private async Task LoadCategories()
    {
        ServiceCategory.BindingContext = this;
        ServiceCategory.ItemsSource = Services;


        FirebaseConnect dBConnection = new FirebaseConnect();
        try
        {
            var query = await dBConnection.GetFirestoreDBconnection()
                .Collection("SERVICES")
                .GetSnapshotAsync();
            query.Query.Listen(snapshot =>
            {
                if (snapshot != null)
                {
                    foreach (var item in snapshot.Changes)
                    {
                        var serv = item.Document.ConvertTo<Services>();
                        switch (item.ChangeType)
                        {
                            case Google.Cloud.Firestore.DocumentChange.Type.Added:
                                services.Add(serv);
                                break;
                            case Google.Cloud.Firestore.DocumentChange.Type.Removed:
                                break;
                            case Google.Cloud.Firestore.DocumentChange.Type.Modified:
                                //int pos = items.FindIndex(x => x.KeyId == dc.Document.Id);
                                //if (pos >= 0)
                                //{
                                //    items.RemoveAt(pos);
                                //}
                                //adapter.NotifyDataSetChanged();
                                int counter = 0;
                                foreach (var x in services)
                                {
                                    if(x.Id==item.Document.Id)
                                    {
                                        services[counter] = x;
                                        counter++;
                                        break;
                                    }
                                }
                                
                                break;
                        }
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void BtnAddService_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AddServicePage());
    }

    private async void BtnModify_Clicked(object sender, EventArgs e)
    {
        var obj = (sender) as Button;
        if(obj.CommandParameter.ToString() != null)
        {
            await Navigation.PushModalAsync(new UpdateServicePage(obj.CommandParameter.ToString()));
        }
    }
}