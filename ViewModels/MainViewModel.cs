

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionEmpleados.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionEmpleados.ViewModels;

partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object _activeView;

    public HomeViewModel HomeViewModel { get; } = new HomeViewModel();

    public EmployeeViewModel _employeeViewModel;
    public DepartmentViewModel _departmentViewModel;
    public GraphViewModel _graphViewModel;
    public SettingsViewModel SettingsViewModel { get; } = new SettingsViewModel();

    public MainViewModel(
        EmployeeViewModel employeeViewModel,
        DepartmentViewModel departmentViewModel,
        GraphViewModel graphViewModel
        )
    {
        _employeeViewModel = employeeViewModel;
        _departmentViewModel = departmentViewModel;
        _graphViewModel = graphViewModel;
        _activeView = HomeViewModel;
    }

    [RelayCommand]
    private void ActivateHomeView() => ActiveView = HomeViewModel;
    [RelayCommand]
    private void ActivateEmployeeView() => ActiveView = _employeeViewModel;
    [RelayCommand]
    private void ActivateDepartmentView() => ActiveView = _departmentViewModel;

    [RelayCommand]
    private void ActivateGraphView() => ActiveView = _graphViewModel;
    [RelayCommand]
    private void ActivateSettingsView() => ActiveView = SettingsViewModel;
    [RelayCommand]
    private void CloseCommand() 
    {
        Application.Current.Shutdown();
    }

}
