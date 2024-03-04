using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionEmpleados.models;
using GestionEmpleados.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using models;
using GestionEmpleados.Properties;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using LiveCharts;
using LiveCharts.Wpf;
using Axis = LiveCharts.Wpf.Axis;
using LiveChartsCore.SkiaSharpView.Painting;
using System.Diagnostics;

namespace GestionEmpleados.ViewModels;

partial class GraphViewModel : ObservableObject 
{

    private IServiceEmployee EmployeeService { get; }

    [ObservableProperty]
    public ObservableCollection<Employee> employeeList = [];

    public SeriesCollection SeriesCollection { get; set; }

    public AxesCollection XAxes { get; set; }

    public RelayCommand UpdateViewCommand { get; }

    public Dictionary<string, int> EmployeesPerDepartment { get; set; }

    public GraphViewModel(IServiceEmployee employeeService)
    {
        EmployeeService = employeeService;
        EmployeeList = new(EmployeeService.FindAllEmployees());

        UpdateViewCommand = new RelayCommand(UpdateView);

        SeriesCollection = [];

        LoadCharts();

    }

    private void UpdateView()
    {
        EmployeeList = new(EmployeeService.FindAllEmployees());

        LoadCharts();
    }

    private void LoadCharts()
    {

        Debug.WriteLine($" Reloading Chart ");

        EmployeesPerDepartment = EmployeeList
            .GroupBy(e => e.DepartamentoEmp != null ? e.DepartamentoEmp.NombreDepartamento : Resources.NoneDepartment)
            .ToDictionary(g => g.Key, g => g.Count());


        SeriesCollection = [];

        foreach (var dep in EmployeesPerDepartment)
        {
            SeriesCollection.Add(new ColumnSeries
            {
                Title = dep.Key,
                Values = new ChartValues<int> { dep.Value }
            });
        }

        XAxes = [
            new Axis
            {
                Labels = new string[] { Resources.DepartmentText },
                LabelsRotation = 0,
            }
        ];

    }
}
