using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionEmpleados.models;
using GestionEmpleados.repositories;
using GestionEmpleados.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using System.Diagnostics;
using models;
using GestionEmpleados.validators;
using System.Windows;
using Microsoft.Win32;
using GestionEmpleados.Properties;

namespace GestionEmpleados.ViewModels;

partial class EmployeeViewModel : ObservableObject
{

    private readonly IServiceEmployee EmployeeService;

    [ObservableProperty]
    public ObservableCollection<Employee> employeeList;

    [ObservableProperty]
    public ObservableCollection<Department> departmentList;

    // Only Change In Save //
    [ObservableProperty]
    private Employee? selectedEmployee = null;

    public RelayCommand<Employee?> SelectEmployeeCommand { get; }

    public RelayCommand NewEmployeeCommand { get; }

    public RelayCommand ClearEmployeeCommand { get; }

    public RelayCommand UpdateEmployeeCommand { get; }

    public RelayCommand DeleteEmployeeCommand { get; }

    public RelayCommand OpenFileExplorer { get;  }

    public RelayCommand UpdateViewCommand { get;}

    [ObservableProperty]
    private string nameTextValue = string.Empty;

    [ObservableProperty]
    private string dniTextValue = string.Empty;

    [ObservableProperty]
    private string emailTextValue = string.Empty;

    [ObservableProperty]
    private string phoneTextValue = string.Empty;

    [ObservableProperty]
    private string positionTextValue = string.Empty;

    [ObservableProperty]
    private string departmentTextValue = string.Empty;

    [ObservableProperty]
    private string imageUriValue = string.Empty;

    public EmployeeViewModel(IServiceEmployee employeeService)
    {
        this.EmployeeService = employeeService;
        EmployeeList = new(EmployeeService.FindAllEmployees());
        DepartmentList = new(EmployeeService.FindAllDepartments());

        SelectEmployeeCommand = new RelayCommand<Employee?>(SelectEmployee);
        NewEmployeeCommand = new RelayCommand(NewEmployeeSave);
        ClearEmployeeCommand = new RelayCommand(ClearEmployee);
        UpdateEmployeeCommand = new RelayCommand(UpdateEmployee);
        DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
        OpenFileExplorer = new RelayCommand(SearchImage);
        UpdateViewCommand = new RelayCommand(UpdateView);

        NewEmployeeStateButtons();

    }

    private void DeleteEmployee()
    {
        MessageBoxResult result = MessageBox.Show(Properties.Resources.DeleteEmployeeConfirmation, Resources.ConfirmationTitle , MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            if (SelectedEmployee != null)
            {               
                EmployeeService.DeleteEmployee(SelectedEmployee);
                EmployeeList.Remove(SelectedEmployee);
                ClearEmployee();

                MessageBox.Show(Properties.Resources.EmployeeDeleted, Resources.ConfirmationTitle);
            }
        }
    }

    private void UpdateView()
    {
        DepartmentList = new(EmployeeService.FindAllDepartments());
        EmployeeList = new(EmployeeService.FindAllEmployees());
        SelectedEmployee = null;
        ClearEmployee();
    }

    private void UpdateEmployee()
    {
        if (SelectedEmployee != null && CheckValidator()) 
        {
            SelectedEmployee.Nombre = NameTextValue;
            SelectedEmployee.Dni = DniTextValue;
            SelectedEmployee.Email = EmailTextValue;
            SelectedEmployee.Telefono = int.Parse(PhoneTextValue);
            SelectedEmployee.Puesto = PositionTextValue;
            SelectedEmployee.DepartamentoEmp = EmployeeService.FindDepartmentsByName(DepartmentTextValue);
            SelectedEmployee.ImagenPath = ImageUriValue;

            ClearEmployee();

            MessageBox.Show(Properties.Resources.EmployeeUpdated, Resources.ConfirmationTitle);
            EmployeeList = new(EmployeeService.FindAllEmployees());
            
        }
        
    }

    private void NewEmployeeSave()
    {
        
        if (CheckValidator()) 
        {
            SelectedEmployee = GenerateNewEmployee();
            if (SelectedEmployee != null)
            { 
                EmployeeService.AddEmployee(SelectedEmployee);
                ClearEmployee();
                EmployeeList = new(EmployeeService.FindAllEmployees());
                MessageBox.Show(Properties.Resources.NewEmployeeSaved, Resources.ConfirmationTitle);
            }
        }
    }

    private Employee? GenerateNewEmployee() => new Employee(
                dni: DniTextValue,
                nombre: NameTextValue,
                email: EmailTextValue,
                telefono: int.Parse(PhoneTextValue),
                puesto: PositionTextValue,
                departamento: EmployeeService.FindDepartmentsByName(DepartmentTextValue),
                imagenPath: ImageUriValue
        );
    

    private bool CheckValidator()
    {
        if (!EmpleadoValidator.ValidarNombre(NameTextValue))
        {
            MessageBox.Show(Properties.Resources.EmployeeNameEmpty, Resources.NewEmployeeError);
            return false;
        }
        else if (!EmpleadoValidator.ValidarDni(DniTextValue))
        {
            MessageBox.Show(Properties.Resources.EmployeeDniEmpty, Resources.NewEmployeeError);
            return false;
        }
        else if (!EmpleadoValidator.ValidarEmail(EmailTextValue))
        {
            MessageBox.Show(Properties.Resources.EmployeeEmailEmpty, Resources.NewEmployeeError);
            return false;
        }
        else if (!EmpleadoValidator.ValidarTelefono(PhoneTextValue))
        {
            MessageBox.Show(Properties.Resources.EmployeePhoneEmpty, Resources.NewEmployeeError);
            return false;
        }
        else if (!EmpleadoValidator.ValidarPuesto(PositionTextValue))
        {
            MessageBox.Show(Properties.Resources.EmployeePositionEmpty, Resources.NewEmployeeError);
            return false;
        }
        else
        {
            // Check DNI Duplicated
            foreach (Employee employee in EmployeeList)
            {
                if (employee.Dni == DniTextValue && employee.Dni != SelectedEmployee.Dni) {
                    MessageBox.Show(Properties.Resources.DuplicatedDni, Resources.NewEmployeeError );
                    return false;
                }
            }
            
            return true;
        }
    }

    private void ClearEmployee()
    {
        SelectedEmployee = null;

        NameTextValue = string.Empty;
        PhoneTextValue = string.Empty;
        DniTextValue = string.Empty;
        EmailTextValue = string.Empty;
        PositionTextValue = string.Empty;
        DepartmentTextValue = string.Empty;
        ImageUriValue = string.Empty;

        NewEmployeeStateButtons();
        
    }

    

    private void SelectEmployee(Employee? selectedEmployee)
    {
        SelectEmployeeStateButtons();
        SelectedEmployee = selectedEmployee;
        ShowDataEmployee();
    }

    private void ShowDataEmployee()
    {
        if (SelectedEmployee != null)
        {
            NameTextValue = SelectedEmployee.Nombre;
            PhoneTextValue = SelectedEmployee.Telefono.ToString();
            DniTextValue = SelectedEmployee.Dni;
            EmailTextValue = SelectedEmployee.Email;
            PositionTextValue = SelectedEmployee.Puesto;
            if (SelectedEmployee.DepartamentoEmp != null) { DepartmentTextValue = SelectedEmployee.DepartamentoEmp.ToString(); } else { DepartmentTextValue = string.Empty; };
            if (SelectedEmployee.ImagenPath != null) { ImageUriValue = SelectedEmployee.ImagenPath.ToString(); } else { ImageUriValue = string.Empty; }
        }
    }

    [ObservableProperty]
    private bool newButtonEnable;
    [ObservableProperty]
    private bool deleteButtonEnable;
    [ObservableProperty]
    private bool updateButtonEnable;

    private void SelectEmployeeStateButtons()
    {
        UpdateButtonEnable = true;
        DeleteButtonEnable = true;
        NewButtonEnable = false;
    }

    private void NewEmployeeStateButtons()
    {
        UpdateButtonEnable = false;
        DeleteButtonEnable = false;
        NewButtonEnable = true;
    }

    private void SearchImage()
    { 
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = Resources.SelectFile;
        openFileDialog.Filter = $"{Resources.ImageFiles}|*.jpg;*.jpeg;*.png;|{Resources.AllFiles}|*.*";

        if (openFileDialog.ShowDialog() == true)
        {
            ImageUriValue = openFileDialog.FileName;
        }  
    }

}

