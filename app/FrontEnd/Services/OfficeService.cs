using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FrontEnd.Schemas;

namespace FrontEnd.Services;

public class OfficeService(HttpClient client, JsonSerializerOptions options) : IRepoService<Office>
{
	Uri uri = new("offices/", UriKind.Relative);
	public async Task<List<Office>> GetAllAsync()
	{
		try
		{
			HttpResponseMessage response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				List<Office>? offices = await response.Content.ReadFromJsonAsync<List<Office>>(options);
				return offices!;
			}
			else
			{
				return new();
			}
		}
		catch (Exception)
		{
			return new();
		}
	}
	public async Task<Office?> GetAsync(int id)
	{
		try
		{
			HttpResponseMessage response = await client.GetAsync($"{uri}{id}");
			if (response.IsSuccessStatusCode)
			{
				Office? office = await response.Content.ReadFromJsonAsync<Office>(options);
				return office!;
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
	public async Task<Office?> AddAsync(Office item)
	{
		try
		{
			string json = JsonSerializer.Serialize<Office>(item, options);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync($"{uri}", content);
			if (response.IsSuccessStatusCode)
			{
				Office? office = await response.Content.ReadFromJsonAsync<Office>(options);
				return office!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception) { return null; }
	}

	public async Task<Office?> UpdateAsync(Office item)
	{
		try
		{
			string json = JsonSerializer.Serialize<Office>(item, options);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PatchAsync($"{uri}{item.Id}", content);
			if (response.IsSuccessStatusCode) {
				Office? office = await response.Content.ReadFromJsonAsync<Office>(options);
				return office!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception) { return null;  }
	}

	public async Task DeleteAsync(int id)
	{
		await client.DeleteAsync($"{uri}{id}");
	}
}
