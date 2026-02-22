using FrontEnd.Pages;

namespace FrontEnd;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeRouting();
		InitializeComponent();
	}

	private void InitializeRouting()
	{
		Routing.RegisterRoute("employeeDetail", typeof(EmployeeDetailPage));
		Routing.RegisterRoute("departmentDetail", typeof(DepartmentDetailPage));
		Routing.RegisterRoute("officeDetail", typeof(OfficeDetailPage));
	}
}
