using FrontEnd.PageModels;

namespace FrontEnd.Pages;

public partial class OfficePage : ContentPage
{
	public OfficePage(OfficePageModel model)
	{
		BindingContext = model;
		SizeChanged += OnPageSizeChanged;
		InitializeComponent();
	}

	private void OnPageSizeChanged(object? sender, EventArgs e)
	{
		var oldSpan = OfficeCollection.Span;

		var newSpan = Convert.ToInt32(Math.Max(1, Math.Floor(Width / 670)));

		if (oldSpan != newSpan)
		{
			OfficeCollection.Span = newSpan;
		}
	}
}