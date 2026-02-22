using FrontEnd.PageModels;

namespace FrontEnd.Pages;

public partial class EmployeeDetailPage : ContentPage
{
	public EmployeeDetailPage(EmployeeDetailPageModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}
}