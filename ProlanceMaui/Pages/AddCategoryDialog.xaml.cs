using ProlanceMaui.DBConnect;

namespace ProlanceMaui.Pages;

public partial class AddCategoryDialog : ContentPage
{
    public string token;
	public AddCategoryDialog()
	{
		InitializeComponent();
        _ = getToken();
    }
    private async Task getToken()
    {
        token = await SecureStorage.Default.GetAsync("token");
    }
    private async Task Add()
    {
        try
        {
            
            FirebaseConnect dbconnect = new FirebaseConnect();
            var storage = dbconnect.GetFirebaseStorage(token);
            string name = Guid.NewGuid().ToString();
            var stream = File.Open(path, FileMode.Open);
            var result_storage = storage.Child(name)
                .PutAsync(stream);
            result_storage.Progress.ProgressChanged += (a, b) =>
            {

            };
            var url = await result_storage;

            Dictionary<string, object> map = new Dictionary<string, object>()
                    {
                        {"name", InputCategory.Text.Trim().ToUpper() },
                        {"description", InputDescription.Text.Trim().ToUpper() },
                        {"download_url", url },
                    };
            await dbconnect.GetFirestoreDBconnection()
                .Collection("CATEGORIES")
                .AddAsync(map);
            await DisplayAlert("SUCCESS", "SUCCESSFULLY ADDED", "Got it");
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "Got it");
        }

    }
    private void BtnImagePicker_Clicked(object sender, EventArgs e)
    {
        TakePhoto();
    }
    private string path = null;
    public async void TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                path = photo.FullPath;
                // save the file into local storage
                BtnImagePicker.Text = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                
            }
        }
    }
    private async void ImgBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void BtnAddCat_Clicked(object sender, EventArgs e)
    {
        Add();
    }
}