using ProlanceMaui.DBConnect;

namespace ProlanceMaui.Pages;

public partial class AddCategoryDialog : ContentPage
{
    public string token;
	public AddCategoryDialog()
	{
		InitializeComponent();
	}

    private void Add()
    {
        FirebaseConnect dbconnect = new FirebaseConnect();
        var storage = dbconnect.GetFirebaseStorage(token);
        string name = Guid.NewGuid().ToString();
        var stream = File.Open(path, FileMode.Open);
        var result_storage = storage.Child(name)
            .PutAsync(stream);
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
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }
    private async void ImgBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}