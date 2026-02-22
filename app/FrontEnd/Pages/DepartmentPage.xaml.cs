using FrontEnd.PageModels;

namespace FrontEnd.Pages;

public partial class DepartmentPage : ContentPage
{
	public DepartmentPage(DepartmentPageModel model)
	{
		BindingContext = model;
		SizeChanged += OnPageSizeChanged;
		InitializeComponent();
	}
	private void OnPageSizeChanged(object? sender, EventArgs e)
	{
		var oldSpan = DepartmentCollection.Span;
		var newSpan = Convert.ToInt32(Math.Max(1, Math.Floor(Width / 270)));
		if (oldSpan != newSpan)
		{
			DepartmentCollection.Span = newSpan;
		}
	}
}