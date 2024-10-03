using Microsoft.Maui.Storage;
namespace CasaOs_Viewer;

public partial class MainPage : ContentPage
{
	private string savedUrlName = "casaOsURL";
	public MainPage()
	{
		InitializeComponent();
		string url = Preferences.Get(savedUrlName, "");

		if (!string.IsNullOrWhiteSpace(url))
		{
            casaosURL.Text = url;
		}
    }

	private async void OnConnectClick(object sender, EventArgs e)
	{
		var url = Preferences.Get(savedUrlName, "");

        if (!URLPassedWithoutErrors())
		{
			return;
		}

        if (casaosURL.Text != url)
        {
			if (await Shell.Current.DisplayAlert("New URL", "URL provided is different to the one saved. Do you want to overwrite existing one?", "Yes", "No"))
			{
                SaveAndConnect(casaosURL.Text);
            }
			else
			{
				Connect();
			}
        }

        
    }

	private void SaveAndConnect(string newUrl)
	{
		Preferences.Set(savedUrlName, newUrl);
		Connect();
    }

    private void Connect()
    {
        casaOSView.IsVisible = true;
		stack.IsVisible = false;
        casaOSView.Source = casaosURL.Text;
    }

	private bool URLPassedWithoutErrors()
	{
		string url = casaosURL?.Text;
        if (string.IsNullOrWhiteSpace(url))
        {
            Shell.Current.DisplayAlert("Warning!", "URL is empty!", "OK");
            return false;
        }

		if (!url.Contains("http"))
        {
            Shell.Current.DisplayAlert("Warning!", "URL is not a valid link!", "OK");
            return false;
		}

        if (!url.Contains("https"))
        {
            Shell.Current.DisplayAlert("Warning!", "URL is not using HTTPS protocol.", "OK");
        }

		

        return true;
    }
	private bool CheckContentOfPage()
	{
		//need to check that the page is actually CasaOS webpage.
		return true;
	}
}

