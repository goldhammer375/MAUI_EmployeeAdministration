using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FrontEnd.Schemas;
using FrontEnd.Services;

namespace FrontEnd.PageModels;

public partial class OfficePageModel : ObservableObject
{
	private readonly IRepoService<Office> service;
	private readonly IMessenger messenger;

	[ObservableProperty]
	private ObservableCollection<Office> _offices;
	[ObservableProperty]
	private Office _selectedOffice;

	public OfficePageModel(IRepoService<Office> service, IMessenger messenger)
	{
		this.service = service;
		this.messenger = messenger;
		RetrieveData();
		this.messenger.Register<OfficeUpdatedMessage>(this, async (r, m) =>
		{
			await RetrieveData();
		});
	}

	private async Task RetrieveData()
	{
		Offices = new ObservableCollection<Office>(await service.GetAllAsync());
	}

	[RelayCommand]
	private async Task GoToDetail()
	{
		await Shell.Current.GoToAsync($"officeDetail?id={SelectedOffice.Id}");
		SelectedOffice = null;
	}

	[RelayCommand]
	private async Task CreateOffice()
	{
		Office o = new() { Address = "New Address", City = "New City", Country = "New Country", Phone = "+34 000-000-000" };
		var response = await service.AddAsync(o);
		if (response != null)
		{
			Offices.Add(response);
			SelectedOffice = response;
			await GoToDetail();
		}
	}
}
