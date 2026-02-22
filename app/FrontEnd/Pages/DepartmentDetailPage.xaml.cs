using FrontEnd.PageModels;

namespace FrontEnd.Pages;

public partial class DepartmentDetailPage : ContentPage
{
	public DepartmentDetailPage(DepartmentDetailPageModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}
}