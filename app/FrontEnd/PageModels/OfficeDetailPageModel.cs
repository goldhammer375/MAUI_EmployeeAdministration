using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FrontEnd.Schemas;
using FrontEnd.Services;

namespace FrontEnd.PageModels;

[QueryProperty(nameof(Id), "id")]
public partial class OfficeDetailPageModel : ObservableObject
{
	private readonly IRepoService<Office> service;
	private readonly IMessenger messenger;


	[ObservableProperty]
	private int _id;
	[ObservableProperty]
	private Office _office;

	public OfficeDetailPageModel(IRepoService<Office> service, IMessenger messenger)
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
		Office = await service.GetAsync(Id);
	}


	[RelayCommand]
	private async Task Save()
	{
		messenger.Send(new OfficeUpdatedMessage(Office));
		await service.UpdateAsync(Office);
		await Shell.Current.GoToAsync("//OfficesPage");
	}

	[RelayCommand]
	private async Task Delete()
	{
		messenger.Send(new OfficeUpdatedMessage(Office));
		await service.DeleteAsync(Office.Id);
		await Shell.Current.GoToAsync("//OfficesPage");
	}
}

public class OfficeUpdatedMessage(Office value) : ValueChangedMessage<Office>(value) {}