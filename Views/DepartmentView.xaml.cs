using GestionEmpleados.models;
using GestionEmpleados.ViewModels;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionEmpleados.Views;

/// <summary>
/// Lógica de interacción para DepartmentView.xaml
/// </summary>
public partial class DepartmentView : UserControl
{
    public DepartmentView()
    {
        InitializeComponent();
        Loaded += DepartmentView_Loaded;
    }

    private void DepartmentView_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is DepartmentViewModel viewModel)
        {
            viewModel.UpdateViewCommand.Execute(null);
        }

    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var datagrid = (DataGrid)sender;

        var selectedDepartment = (Department)datagrid.SelectedItem;

        if (DataContext is DepartmentViewModel viewModel)
        {
            viewModel.SelectDepartmentCommand.Execute(selectedDepartment);
        }
    }

    

    private void EmployeeInDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var datagrid = (DataGrid)sender;

        var selectedEmployee = (Employee)datagrid.SelectedItem;

        if (DataContext is DepartmentViewModel viewModel)
        {
            viewModel.SelectEmployeeInDepartmentCommand.Execute(selectedEmployee);
        }

    }

    private void EmployeeOutDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var datagrid = (DataGrid)sender;

        var selectedEmployee = (Employee)datagrid.SelectedItem;

        if (DataContext is DepartmentViewModel viewModel)
        {
            viewModel.SelectEmployeeOutDepartmentCommand.Execute(selectedEmployee);
        }

    }
}
