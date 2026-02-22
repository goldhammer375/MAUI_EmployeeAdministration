using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FrontEnd.Schemas;

namespace FrontEnd.Services;

public class EmployeeService(HttpClient client, JsonSerializerOptions options) : IRepoService<Employee>
{
	Uri uri = new("employees/", UriKind.Relative);
	public async Task<List<Employee>> GetAllAsync()
	{
		try
		{
			HttpResponseMessage response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				List<Employee>? employees = await response.Content.ReadFromJsonAsync<List<Employee>>(options);
				return employees!;
			}
			else
			{
				return new();
			}
		} catch (Exception)
		{
			return new();
		}
	}
	public async Task<Employee?> GetAsync(int id)
	{
		try
		{
			HttpResponseMessage response = await client.GetAsync($"{uri}{id}");
			if (response.IsSuccessStatusCode)
			{
				Employee? employee = await response.Content.ReadFromJsonAsync<Employee>(options);
				return employee!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception)
		{
			return null	;
		}
	}
	public async Task<Employee?> AddAsync(Employee item)
	{
		try
		{
			var postData = new
			{
				name = item.Name,
				position = item.Position,
				image_url = item.ImageUrl,
				department_id = item.Department?.Id ?? 0,
				office_id = item.Office?.Id ?? 0,
				boss_id = item.Boss?.Id ?? 0
			};
			string json = JsonSerializer.Serialize(postData, options);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync($"{uri}", content);
			if (response.IsSuccessStatusCode)
			{
				Employee? employee = await response.Content.ReadFromJsonAsync<Employee>(options);
				return employee!;
			}
			else
			{
				return null;
			}
		}
		catch (Exception) { return null; }
	}

	public async Task<Employee?> UpdateAsync(Employee item)
	{
		try
		{
			var patchData = new
			{
				name = item.Name,
				position = item.Position,
				image_url = item.ImageUrl,
				department_id = item.Department?.Id ?? 0,
				office_id = item.Office?.Id ?? 0,
				boss_id = item.Boss?.Id ?? 0
			};
			
			string json = JsonSerializer.Serialize(patchData, options);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PatchAsync($"{uri}{item.Id}", content);
			if (response.IsSuccessStatusCode) {
				Employee? employee = await response.Content.ReadFromJsonAsync<Employee>(options);
				return employee!;
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
