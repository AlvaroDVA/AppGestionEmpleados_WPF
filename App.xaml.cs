using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls.Primitives;
using GestionEmpleados.Properties;
using GestionEmpleados.repositories;
using GestionEmpleados.Services;
using GestionEmpleados.ViewModels;
using GestionEmpleados.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GestionEmpleados;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServiceCollection serviceCollection = new();

        ConfigureServices(serviceCollection);

        InitializeLenguage();
        InitializeTheme();


        var serviceProvider = serviceCollection.BuildServiceProvider();

        var view = serviceProvider.GetService<MainWindow>();

        view.DataContext = serviceProvider.GetService<MainViewModel>();

        view.Show();
    }

    private void InitializeTheme()
    {
       
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

        mergedDictionaries.Clear();
        string themePath;
        // No puedo acceder a Resources , asi que le pongo el String directamente
         if (Settings.Default.Theme == "Dark") 
        {
            themePath = "Themes/Dark/DarkTheme.xaml";
        } 
        else 
        {
            themePath = "Themes/Light/LightTheme.xaml"; 
        };

        mergedDictionaries.Add(new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) });
    }

    private void InitializeLenguage()
    {
        string savedLenguage = Settings.Default.Lenguage;

        try
        {
            CultureInfo culture = new CultureInfo(savedLenguage);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        catch (CultureNotFoundException)
        {
            CultureInfo defaultCulture = new CultureInfo("en_EN");
            Thread.CurrentThread.CurrentCulture = defaultCulture;
            Thread.CurrentThread.CurrentUICulture = defaultCulture;

            Settings.Default.Lenguage = "en_EN";
            Settings.Default.Save();

        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<MainWindow>();
        services.AddScoped<EmployeeView>();
        services.AddScoped<DepartmentView>();
        services.AddScoped<GraphView>();
        services.AddScoped<SettingsView>();

        services.AddScoped<MainViewModel>();
        services.AddScoped<HomeViewModel>();
        services.AddScoped<EmployeeViewModel>();
        services.AddScoped<DepartmentViewModel>();
        services.AddScoped<GraphViewModel>();
        services.AddScoped<SettingsViewModel>();

        services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
        
        services.AddSingleton<IServiceEmployee, ServiceEmployee>();

    }

}

