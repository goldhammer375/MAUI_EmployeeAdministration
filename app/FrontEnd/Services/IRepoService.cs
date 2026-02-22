namespace FrontEnd.Services;

public interface IRepoService<T>
{
	public Task<List<T>> GetAllAsync();
	public Task<T?> GetAsync(int id);
	public Task<T?> AddAsync(T item);
	public Task<T?> UpdateAsync(T item);
	public Task DeleteAsync(int id);
}
