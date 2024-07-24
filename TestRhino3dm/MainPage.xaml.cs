using Rhino.FileIO;

namespace TestRhino3dm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnPickRhino3dmFile(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS,  [] }, // UTType values
                    { DevicePlatform.Android,  [] }, // MIME type
                    { DevicePlatform.WinUI, new[] { "*.3dm" } }, // file extension
                    { DevicePlatform.macOS,  [] }, // UTType values
                });
            PickOptions options = new()
            {
                PickerTitle = "Please select a Rhino3dm file",
                FileTypes = customFileType,
            };

            var pickResult = await FilePicker.PickAsync(options);
            if (pickResult is not null)
            {
                try
                {
                    var file3dm = File3dm.Read(pickResult.FullPath);
                }
                catch (Exception exception)
                {
                    if (Application.Current?.MainPage is not null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Can' read 3dm file", exception.ToString(), "Ok");
                    }
                }
            }
        }
    }
}