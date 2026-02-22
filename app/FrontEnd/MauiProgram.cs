using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using FrontEnd.PageModels;
using FrontEnd.Pages;
using FrontEnd.Schemas;
using FrontEnd.Services;
using Microsoft.Extensions.Logging;

namespace FrontEnd;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<DepartmentPage>();
        builder.Services.AddTransient<OfficePage>();

        builder.Services.AddTransient<EmployeeDetailPage>();
        builder.Services.AddTransient<DepartmentDetailPage>();
        builder.Services.AddTransient<OfficeDetailPage>();


        builder.Services.AddTransient<HomePageModel>();
        builder.Services.AddTransient<DepartmentPageModel>();
        builder.Services.AddTransient<OfficePageModel>();

        builder.Services.AddTransient<EmployeeDetailPageModel>();
        builder.Services.AddTransient<DepartmentDetailPageModel>();
        builder.Services.AddTransient<OfficeDetailPageModel>();

        builder.Services.AddTransient<IRepoService<Employee>, EmployeeService>();
        builder.Services.AddTransient<IRepoService<Department>, DepartmentService>();
        builder.Services.AddTransient<IRepoService<Office>, OfficeService>();

        builder.Services.AddSingleton<HttpClient>(new HttpClient()
		{
			BaseAddress = new Uri("http://localhost:8000/")
		});
        builder.Services.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions()
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
		});

        builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
