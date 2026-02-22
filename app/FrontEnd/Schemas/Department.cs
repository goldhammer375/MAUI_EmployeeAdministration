namespace FrontEnd.Schemas;
public class Department
{
	public int Id { get; set; }
	public string Name { get; set; }

	public override bool Equals(object? obj)
	{
		return obj is Department department &&
			   Id == department.Id;
	}
	public override int GetHashCode() => Id.GetHashCode();
}