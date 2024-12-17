namespace BibLib.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private string _userDataPath = "";
    private string _applicationConfigurationPath = "";
    private string _databasePath = "";

    public string UserDataPath
    {
        get => _userDataPath;
        set => Set(ref _userDataPath, value);
    }

    public string ApplicationConfigurationPath
    {
        get => _applicationConfigurationPath;
        set => Set(ref _applicationConfigurationPath, value);
    }

    public string DatabasePath
    {
        get => _databasePath;
        set => Set(ref _databasePath, value);
    }
}
