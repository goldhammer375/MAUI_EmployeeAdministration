using FrontEnd.PageModels;

namespace FrontEnd.Pages;

public partial class OfficeDetailPage : ContentPage
{
	public OfficeDetailPage(OfficeDetailPageModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}
}