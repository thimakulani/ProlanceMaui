using Google.Cloud.Firestore;
using ProlanceMaui.DBConnect;
using ProlanceMaui.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProlanceMaui.Pages;

public partial class AddServicePage : ContentPage
{
	public AddServicePage()
	{
		InitializeComponent();
        _ = LoadCategories();

    }
    private ObservableCollection<string> categories = new ObservableCollection<string>();
    public ObservableCollection<string> Categories { get { return categories; } set { categories = value; } }
    
    private async Task LoadCategories()
    {
        ServiceCategory.BindingContext = this;
        ServiceCategory.ItemsSource = Categories;


        FirebaseConnect dBConnection = new FirebaseConnect();
        try
        {
            var query = await dBConnection.GetFirestoreDBconnection()
                .Collection("CATEGORIES")
                .GetSnapshotAsync();
            
            if(query != null)
            {
                
                foreach (var item in query.Documents)
                {
                    var data = item.ConvertTo<Categories>();
                    categories.Add(data.Name);
                    keys.Add(data.Id);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ImgBack_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
    private List<string> keys = new List<string>();
    private async void BtnAddService_Clicked(object sender, EventArgs e)
    {
        if (ServiceCategory.SelectedIndex < 0)
        {
            return;
        }
        
        if (string.IsNullOrEmpty(InputServiceName.Text.Trim()))
        {
            InputServiceName.Focus();
            return;
        }

        Dictionary<string, object> map = new()
            {
                {"Name", InputServiceName.Text.Trim() },
                {"Category", keys[ServiceCategory.SelectedIndex] },
            };

        try
        {
            FirebaseConnect FirebaseConnect = new FirebaseConnect();

            var data = FirebaseConnect.GetFirestoreDBconnection()
                .Collection("SERVICES")
                .WhereEqualTo("Name", InputServiceName.Text.ToUpper());
            var query = await data.GetSnapshotAsync();
            if (query.Documents.Count == 0)
            {


                FirebaseConnect = new FirebaseConnect();
                await FirebaseConnect.GetFirestoreDBconnection()
                    .Collection("SERVICES")
                    .AddAsync(map);
                await DisplayAlert("SUCCESS", "SUCCESSFULLY ADDED", "Got It");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "SERVICE ALREADY EXISTS", "Got It");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Got It");
        }
    }
}