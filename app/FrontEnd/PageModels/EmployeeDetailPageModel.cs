using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FrontEnd.Schemas;
using FrontEnd.Services;

namespace FrontEnd.PageModels;

[QueryProperty(nameof(Id), "id")]
public partial class EmployeeDetailPageModel : ObservableObject
{
	[ObservableProperty]
	private int _id;
	[ObservableProperty]
	private Employee _employee;

	[ObservableProperty]
	private ObservableCollection<Department> _departments;
	[ObservableProperty]
	private ObservableCollection<Employee> _employees;
	[ObservableProperty]
	private ObservableCollection<Office> _offices;


	private readonly IRepoService<Department> departmentService;
	private readonly IRepoService<Office> officeService;
	private readonly IRepoService<Employee> employeeService;
	private readonly IMessenger messenger;

	public EmployeeDetailPageModel(IRepoService<Department> departmentService, IRepoService<Office> officeService, IRepoService<Employee> employeeService, IMessenger messenger)
	{
		this.departmentService = departmentService;
		this.officeService = officeService;
		this.employeeService = employeeService;
		this.messenger = messenger;
	}

	partial void OnIdChanged(int value)
	{
		RetrieveData();
	}

	private async Task RetrieveData()
	{
		Departments = new ObservableCollection<Department>(await departmentService.GetAllAsync());
		Offices = new ObservableCollection<Office>(await officeService.GetAllAsync());
		var employees = await employeeService.GetAllAsync();
		Employees = new ObservableCollection<Employee>(employees.Where(e => e.Id != Id));

		Employee = await employeeService.GetAsync(Id);
	}

	[RelayCommand]
	private async Task Save()
	{
		messenger.Send(new EmployeeUpdatedMessage(Employee));
		await employeeService.UpdateAsync(Employee);
		await Shell.Current.GoToAsync("//EmployeesPage");
	}

	[RelayCommand]
	private async Task Delete()
	{
		await employeeService.DeleteAsync(Employee.Id);
		messenger.Send(new EmployeeUpdatedMessage(Employee));
		await Shell.Current.GoToAsync("//EmployeesPage");
	}

}

public class EmployeeUpdatedMessage(Employee value) : ValueChangedMessage<Employee>(value) {}