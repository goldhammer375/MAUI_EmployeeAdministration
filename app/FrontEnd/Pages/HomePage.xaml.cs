using FrontEnd.PageModels;

namespace FrontEnd.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageModel model)
	{
		BindingContext = model;
		SizeChanged += OnPageSizeChanged;
		InitializeComponent();
	}

	public void OnPageSizeChanged(object sender, EventArgs e)
	{
		EmployeeCollection.Span = Convert.ToInt32(Math.Max(1, Math.Floor(Width / 520)));
	}
}
