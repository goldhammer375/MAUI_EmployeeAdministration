using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FrontEnd.Schemas;
using FrontEnd.Services;

namespace FrontEnd.PageModels;

[QueryProperty(nameof(Id), "id")]
public partial class DepartmentDetailPageModel : ObservableObject
{
	private readonly IRepoService<Department> service;
	private readonly IMessenger messenger;
	[ObservableProperty]
	private int _id;
	[ObservableProperty]
	private Department _department;

	public DepartmentDetailPageModel(IRepoService<Department> service, IMessenger messenger)
	{
		this.service = service;
		this.messenger = messenger;
	}

	partial void OnIdChanged(int value)
	{
		RetrieveData();
	}

	private async Task RetrieveData()
	{
		Department = await service.GetAsync(Id);
	}

	[RelayCommand]
	private async Task Save()
	{
		messenger.Send(new DepartmentUpdatedMessage(Department));
		await service.UpdateAsync(Department);
		await Shell.Current.GoToAsync("//DepartmentsPage");
	}

	[RelayCommand]
	private async Task Delete()
	{
		await service.DeleteAsync(Id);
		messenger.Send(new DepartmentUpdatedMessage(Department));
		await Shell.Current.GoToAsync("//DepartmentsPage");
	}
}

public class DepartmentUpdatedMessage(Department value) : ValueChangedMessage<Department>(value) {}