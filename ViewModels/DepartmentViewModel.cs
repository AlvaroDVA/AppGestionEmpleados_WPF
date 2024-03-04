using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionEmpleados.models;
using GestionEmpleados.Properties;
using GestionEmpleados.Services;
using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace GestionEmpleados.ViewModels;
partial class DepartmentViewModel : ObservableObject
{

    private readonly IServiceEmployee EmployeeService;

    [ObservableProperty]
    public ObservableCollection<Employee> employeeList;

    [ObservableProperty]
    public ObservableCollection<Employee> employeesNoDepartment = [];

    [ObservableProperty]
    public ObservableCollection<Department> departmentList;

    [ObservableProperty]
    public Department? selectedDepartment = null;

    [ObservableProperty]
    public ObservableCollection<Employee> employeesInDepartment = [];

    [ObservableProperty]
    private string departmentName = string.Empty;

    public RelayCommand<Department?> SelectDepartmentCommand { get; }

    public RelayCommand NewDeparmentCommand { get; }

    public RelayCommand ClearDeparmentCommand { get; }

    public RelayCommand UpdateDeparmentCommand { get; }

    public RelayCommand DeleteDeparmentCommand { get; }

    public RelayCommand UpdateViewCommand { get; }



    public DepartmentViewModel(IServiceEmployee employeeService) 
    {
        this.EmployeeService = employeeService;
        
        GenerateLists();
        CounterPerDepartmentCalculator();

        // All Departments && New / Update Departments
        SelectDepartmentCommand = new RelayCommand<Department?>(SelectDepartment);
        NewDeparmentCommand = new RelayCommand(NewDepartmentSave);
        ClearDeparmentCommand = new RelayCommand(ClearDepartment);
        UpdateDeparmentCommand = new RelayCommand(UpdateDepartment);
        DeleteDeparmentCommand = new RelayCommand(DeleteDepartment);
        UpdateViewCommand = new RelayCommand(UpdateView);

        // All Employees in Deparment
        SelectEmployeeInDepartmentCommand = new RelayCommand<Employee?>(SelectEmployeeInDepartment); 
        RemoveEmployeeFromDepartmentCommand = new RelayCommand(RemoveEmployeeFromDepartment);

        // All Employees with Department null
        SelectEmployeeOutDepartmentCommand = new RelayCommand<Employee?>(SelectEmployeeOutDepartment);
        AddEmployeeToDepartmentCommand = new RelayCommand(AddEmployeeToDepartment);

        NewDepartmentStateButtons();
    }

    private void UpdateView()
    {
        DepartmentList = new(EmployeeService.FindAllDepartments());
        EmployeeList = new(EmployeeService.FindAllEmployees());

        EmployeesNoDepartment = new(EmployeeList.Where(emp => emp.DepartamentoEmp == null));

        CounterPerDepartmentCalculator();
        LoadEmployeesInDepartment();

    }

    private void SelectDepartment(Department? selectedDepartment)
    {
        if (selectedDepartment != null)
        {
            if (EmployeeOutDepartmentSelected != null) { AddToDeparment = true; }
            SelectDepartmentStateButtons();
            SelectedDepartment = selectedDepartment;
            DepartmentName = SelectedDepartment.NombreDepartamento;
            LoadEmployeesInDepartment();
            
            RemoveFromDeparment = false;
        }
    }

    private void LoadEmployeesInDepartment() 
    {
        if (SelectedDepartment != null)
        {
            EmployeesInDepartment = new(EmployeeList.Where(e => e.DepartamentoEmp != null && e.DepartamentoEmp.NombreDepartamento == SelectedDepartment.NombreDepartamento));
        }
    }

    private void DeleteDepartment()
    {

        MessageBoxResult result = MessageBox.Show(Properties.Resources.DeleteDepartmentConfirmation, Resources.ConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            if (SelectedDepartment != null)
            {
                EmployeesInDepartment = [];

                foreach (Employee emp in EmployeeList)
                {
                    if (emp.DepartamentoEmp != null && emp.DepartamentoEmp.NombreDepartamento == SelectedDepartment.NombreDepartamento)
                    {
                        emp.DepartamentoEmp = null;
                    }
                }


                EmployeeService.DeleteDepartment(SelectedDepartment);
                DepartmentList.Remove(SelectedDepartment);
                EmployeesInDepartment = [];
                AddToDeparment = false;
                SelectedDepartment = null;
                DepartmentName = string.Empty;
                UpdateView();
                NewDepartmentStateButtons();
                MessageBox.Show(Properties.Resources.DepartmentDeleted, Resources.ConfirmationTitle);
                
            }
        }
    }

    private void NewDepartmentSave()
    {
        if (!string.IsNullOrEmpty(DepartmentName))
        {
            foreach (Department department in DepartmentList)
            {
                if (department.NombreDepartamento == DepartmentName)
                {
                    MessageBox.Show(Properties.Resources.DepartmentDuplicated);
                    return;
                }
            }

            SelectedDepartment = new Department(DepartmentName);
            EmployeeService.AddDepartment(SelectedDepartment);
            ClearDepartment();
            DepartmentList = new(EmployeeService.FindAllDepartments());
            MessageBox.Show(Properties.Resources.NewDepartmentSaved, Resources.ConfirmationTitle);
        }
    }

    private void UpdateDepartment()
    {
        if (SelectedDepartment != null && !string.IsNullOrEmpty(DepartmentName))
        {
            foreach (Department department in DepartmentList)
            {
                if (department.NombreDepartamento == DepartmentName && department.NombreDepartamento != SelectedDepartment.NombreDepartamento)
                {
                    MessageBox.Show(Properties.Resources.DepartmentDuplicated);
                    return;
                }
            }

            var oldName = SelectedDepartment.NombreDepartamento;

            SelectedDepartment.NombreDepartamento = DepartmentName;

            UpdateEmployeesDeparment(oldName);

            EmployeesInDepartment = new(EmployeeList.Where(e => e.DepartamentoEmp != null && e.DepartamentoEmp.NombreDepartamento == SelectedDepartment.NombreDepartamento));

            UpdateView();

            MessageBox.Show(Properties.Resources.NewDepartmentSaved);


        }else
        {
            MessageBox.Show(Properties.Resources.NewDepartmentError);
        }

    }

    private void UpdateEmployeesDeparment(string oldName)
    {   
        foreach (Employee employee in EmployeeList)
        {
            if (employee.DepartamentoEmp != null && employee.DepartamentoEmp.NombreDepartamento == oldName)
            {
                employee.DepartamentoEmp = SelectedDepartment;
            }
        }
    }

    private void GenerateLists()
    {

        EmployeeList = new(EmployeeService.FindAllEmployees());
        DepartmentList = new(EmployeeService.FindAllDepartments());
        GenerateNoDepartmentList();

    }

    private void GenerateNoDepartmentList()
    {
        foreach (Employee employee in EmployeeList)
        {
            if (employee.DepartamentoEmp == null)
            {
                EmployeesNoDepartment.Add(employee);
            }
        }
    }

    private void ShowDepartmentEmployees()
    {
        if (SelectedDepartment != null)
        {
            EmployeesInDepartment.Clear();
            foreach (Employee employee in EmployeeList)
            {
                if (employee.DepartamentoEmp != null &&
                    employee.DepartamentoEmp.ToString == SelectedDepartment.ToString)
                {
                    EmployeesInDepartment.Add(employee);
                }
            }
        }
    }
    private void CounterPerDepartmentCalculator()
    {
        foreach (var department in departmentList)
        {
           department.CountEmployees = EmployeeList
                .Count(e => e.DepartamentoEmp != null && e.DepartamentoEmp.ToString() == department.ToString());

        }
    }

    private void ClearDepartment()
    {
        SelectedDepartment = null;

        DepartmentName = string.Empty;

        EmployeesInDepartment = [];

        AddToDeparment = false;

        NewDepartmentStateButtons();

    }

    [ObservableProperty]
    private bool newButtonEnable;
    [ObservableProperty]
    private bool deleteButtonEnable;
    [ObservableProperty]
    private bool updateButtonEnable;

    private void SelectDepartmentStateButtons()
    {
        UpdateButtonEnable = true;
        DeleteButtonEnable = true;
        NewButtonEnable = false;
    }

    private void NewDepartmentStateButtons()
    {
        UpdateButtonEnable = false;
        DeleteButtonEnable = false;
        NewButtonEnable = true;

    }

    [ObservableProperty]
    private Employee? employeeInDepartmentSelected = null;

    public RelayCommand <Employee?> SelectEmployeeInDepartmentCommand { get; }

    [ObservableProperty]
    private bool removeFromDeparment = false;

    public RelayCommand RemoveEmployeeFromDepartmentCommand { get;} 

    private void RemoveEmployeeFromDepartment()
    {
        if (EmployeeInDepartmentSelected != null)
        {
            MessageBoxResult result = MessageBox.Show(Properties.Resources.RemoveEmployeeFromDepartment, Resources.ConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            { 
                EmployeeInDepartmentSelected.DepartamentoEmp = null;
                EmployeesNoDepartment.Add(EmployeeInDepartmentSelected);
                EmployeesInDepartment.Remove(EmployeeInDepartmentSelected);

                EmployeeInDepartmentSelected = null;
                UpdateView();
                RemoveFromDeparment = false;
            }
        }
    }

    private void SelectEmployeeInDepartment(Employee? selectedEmployee)
    {
        
        EmployeeInDepartmentSelected = selectedEmployee;
        RemoveFromDeparment = true;
       
    }

    [ObservableProperty]
    private Employee? employeeOutDepartmentSelected = null;

    public RelayCommand<Employee?> SelectEmployeeOutDepartmentCommand { get; }

    [ObservableProperty]
    private bool addToDeparment = false;

    public RelayCommand AddEmployeeToDepartmentCommand { get; }

    private void SelectEmployeeOutDepartment (Employee? selectedEmployee)
    {
        EmployeeOutDepartmentSelected = selectedEmployee;
        if (SelectedDepartment != null) { AddToDeparment = true; }
        
    }

    private void AddEmployeeToDepartment()
    {
        if (EmployeeOutDepartmentSelected != null && SelectedDepartment != null)
        {
            EmployeeOutDepartmentSelected.DepartamentoEmp = SelectedDepartment;
            EmployeesNoDepartment.Remove(EmployeeOutDepartmentSelected);
            EmployeesInDepartment.Add(EmployeeOutDepartmentSelected);

            EmployeeOutDepartmentSelected = null;
            UpdateView();
            AddToDeparment = false;
        }
        
    }
}

