namespace FrontEnd.Schemas;

public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Position { get; set; }
	public string ImageUrl { get; set; }
	public Department? Department { get; set; }
	public Office? Office { get; set; }
	public Employee? Boss { get; set; }

	public override bool Equals(object? obj)
	{
		return obj is Employee employee &&
			   Id == employee.Id;
	}
	public override int GetHashCode() => Id.GetHashCode();
}
