using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FrontEnd.Schemas;
using FrontEnd.Services;

namespace FrontEnd.PageModels;

public partial class DepartmentPageModel : ObservableObject
{
	private readonly IRepoService<Department> service;
	private readonly IMessenger messenger;

	[ObservableProperty]
	private ObservableCollection<Department> _departments;
	[ObservableProperty]
	private Department _selectedDepartment;
	[ObservableProperty]
	private string _message;

	public DepartmentPageModel(IRepoService<Department> service, IMessenger messenger)
	{
		this.service = service;
		this.messenger = messenger;
		RetrieveData();
		this.messenger.Register<DepartmentUpdatedMessage>(this, async (r, m) =>
		{
			await RetrieveData();
		});
	}

	private async Task RetrieveData()
	{
		Departments = new ObservableCollection<Department>(await service.GetAllAsync());
	}

	[RelayCommand]
	public async Task CreateDepartment()
	{
		Department d = new() { Name = "New Department" };
		var response = await service.AddAsync(d);
		if (response != null)
		{
			Departments.Add(response);
			SelectedDepartment = response;
			await GoToDetail();
		}
	}

	[RelayCommand]
	public async Task GoToDetail()
	{
		await Shell.Current.GoToAsync($"departmentDetail?id={SelectedDepartment.Id}");
		SelectedDepartment = null;
	}

	
}
