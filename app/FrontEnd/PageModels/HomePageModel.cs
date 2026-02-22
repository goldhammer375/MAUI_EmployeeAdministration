using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FrontEnd.Schemas;
using FrontEnd.Services;

namespace FrontEnd.PageModels;

public partial class HomePageModel : ObservableObject
{
	private readonly IRepoService<Employee> service;
	private readonly IMessenger messenger;
	[ObservableProperty]
	private ObservableCollection<Employee> _employees;
	[ObservableProperty]
	private Employee _selectedEmployee;


	public HomePageModel(IRepoService<Employee> service, IMessenger messenger)
	{
		this.service = service;
		this.messenger = messenger;
		RetrieveData();	
		this.messenger.Register<EmployeeUpdatedMessage>(this, async (r, m) =>
		{
			await RetrieveData();
		});
	}

	private async Task RetrieveData()
	{
		Employees = new ObservableCollection<Employee>(await service.GetAllAsync());
	}

	[RelayCommand]
	public async Task CreateEmployee()
	{
		Employee e = new() { Name = "New Employee", Position = "New Position", ImageUrl = "https://placecats.com/300/200" };
		var response = await service.AddAsync(e);
		if (response != null)
		{
			Employees.Add(response);
			SelectedEmployee = response;
			await GoToDetail();
		}
	}

	[RelayCommand]
	private async Task GoToDetail()
	{
		await Shell.Current.GoToAsync($"employeeDetail?id={SelectedEmployee.Id}");
		SelectedEmployee = null;
	}
}
