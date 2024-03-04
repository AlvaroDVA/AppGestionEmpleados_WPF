using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using GestionEmpleados.Properties;
using System.Windows;

namespace GestionEmpleados.ViewModels;

partial class SettingsViewModel : ObservableObject
{

    [ObservableProperty]
    public string _activeLenguage;

    public ObservableCollection<string> AllLenguages { get; private set; }

    [ObservableProperty]
    public string _activeTheme;

    public ObservableCollection<string> AllThemes { get; private set; }

    public ICommand SaveCommand { get; } 


    private Dictionary<string, string> cultureToLanguageMap = new Dictionary<string, string>
    {
        { "en-US", $"{Properties.Resources.EnglishLenguage}" },
        { "es-ES", $"{Properties.Resources.SpanishLenguage}" },
    };

    private Dictionary<string, string> lenguageToCultureMap = new Dictionary<string, string>
    {
        { $"{Properties.Resources.EnglishLenguage}", "en-US" },
        { $"{Properties.Resources.SpanishLenguage}","es-ES" },
    };

    public SettingsViewModel()     
    {
        SaveCommand = new RelayCommand(SaveConfiguration);
        FillComboBoxLenguage();
        FillComboBoxTheme();
    }

   

    private void FillComboBoxTheme()
    {
        ActiveTheme = Settings.Default.Theme;

        AllThemes = new ObservableCollection<string>();
        AllThemes.Add(Properties.Resources.DarkTheme);
        AllThemes.Add(Properties.Resources.LightTheme);



    }

    private void FillComboBoxLenguage()
    {
        AllLenguages = new ObservableCollection<string>();
        AllLenguages.Add(Properties.Resources.SpanishLenguage);
        AllLenguages.Add(Properties.Resources.EnglishLenguage);

        if (cultureToLanguageMap.TryGetValue(Settings.Default.Lenguage, out var languageName))
        {
            ActiveLenguage = languageName;
        }

    }

    public void SaveConfiguration()
    {
        try
        {

            if (lenguageToCultureMap.TryGetValue(ActiveLenguage, out var cultureCode))
            {
                Settings.Default.Lenguage = cultureCode;
                Settings.Default.Save();
                
            }
            
             Settings.Default.Theme = ActiveTheme;
             Settings.Default.Save();
            

            MessageBox.Show($"{Properties.Resources.SaveSettingsText}", $"{Properties.Resources.ConfirmationTitle}", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }


    
}
