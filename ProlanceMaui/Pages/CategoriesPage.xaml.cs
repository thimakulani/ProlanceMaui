using ProlanceMaui.DBConnect;
using ProlanceMaui.Models;
using System.Collections.ObjectModel;

namespace ProlanceMaui.Pages;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();
        _=LoadCategories();

    }
    private ObservableCollection<Categories> categories = new ObservableCollection<Categories>();
    public ObservableCollection<Categories> Categories { get { return categories; } set { categories = value; } }
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
            query.Query.Listen(snapshot =>
            {
                if (snapshot != null)
                {
                    foreach (var item in snapshot.Changes)
                    {
                        var cat = item.Document.ConvertTo<Categories>();
                        switch (item.ChangeType)
                        {
                            case Google.Cloud.Firestore.DocumentChange.Type.Added:
                                categories.Add(cat);
                                break;
                            case Google.Cloud.Firestore.DocumentChange.Type.Removed:
                                break;
                            case Google.Cloud.Firestore.DocumentChange.Type.Modified:
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
    private void ImgService_Clicked(object sender, EventArgs e)
    {

    }
    

    private async void BtnAddCategory_Clicked(object sender, EventArgs e)
    {
       //await Shell.Current.GoToAsync("AddCategoryDialog");
        await Navigation.PushModalAsync(new AddCategoryDialog());
    }
}