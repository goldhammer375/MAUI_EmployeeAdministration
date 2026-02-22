using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FrontEnd.Schemas;

namespace FrontEnd.Services;

public class DepartmentService(HttpClient client, JsonSerializerOptions options) : IRepoService<Department>
{
	Uri uri = new("departments/", UriKind.Relative);
	public async Task<List<Department>?> GetAllAsync()
	{
		try
		{
			HttpResponseMessage response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				List<Department>? departments = await response.Content.ReadFromJsonAsync<List<Department>>(options);
				return departments!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<Department?> GetAsync(int id)
	{
		try
		{
			HttpResponseMessage response = await client.GetAsync(uri + $"{id}");
			if (response.IsSuccessStatusCode)
			{
				Department? department = await response.Content.ReadFromJsonAsync<Department>(options);
				return department!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<Department?> AddAsync(Department item)
	{
		try
		{
			string json = JsonSerializer.Serialize<Department>(item, options);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync($"{uri}", content);
			if (response.IsSuccessStatusCode)
			{
				Department? department = await response.Content.ReadFromJsonAsync<Department>(options);
				return department!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception) { return null; }
	}

	public async Task<Department?> UpdateAsync(Department item)
	{
		try
		{
			string json = JsonSerializer.Serialize<Department>(item, options);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PatchAsync($"{uri}{item.Id}", content);
			if (response.IsSuccessStatusCode) {
				Department? department = await response.Content.ReadFromJsonAsync<Department>(options);
				return department!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception) { return null; }
	}

	public async Task DeleteAsync(int id)
	{
		await client.DeleteAsync($"{uri}{id}");
	}

}
