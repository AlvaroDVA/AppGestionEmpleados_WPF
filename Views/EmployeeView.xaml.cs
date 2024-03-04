using GestionEmpleados.models;
using GestionEmpleados.ViewModels;
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


public partial class EmployeeView : UserControl
{
    public EmployeeView()
    {
        InitializeComponent();
        Loaded += EmployeeView_Loaded;
    }

    private void EmployeeView_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is EmployeeViewModel viewModel)
        {
            viewModel.UpdateViewCommand.Execute(null);
        }
        
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var datagrid = (DataGrid)sender;

        var selectedEmployee = (Employee)datagrid.SelectedItem;

        if (DataContext is EmployeeViewModel viewModel)
        {
            viewModel.SelectEmployeeCommand.Execute(selectedEmployee);
        }
    }

    private void GridImage_MouseLeftDown (object sender, MouseEventArgs e)
    {
        if (DataContext is EmployeeViewModel viewModel)
        {
            viewModel.OpenFileExplorer.Execute(null);
        }
    }

}
