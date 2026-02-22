namespace FrontEnd.Schemas;
public class Office
{
	public int Id { get; set; }
	public string Address { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
	public string Phone { get; set; }

	public override bool Equals(object? obj)
	{
		return obj is Office o && Id == o.Id;
	}
	public override int GetHashCode() => Id.GetHashCode();
}