using Supabase;
using BlazorGitHubTest.Models;

namespace BlazorGitHubTest.Services;

public class SupabaseService
{
    private readonly Client _client;

    public SupabaseService(Client client)
    {
        _client = client;
    }

    // Pobierz wszystkie podróże
    public async Task<List<TestTrip>> GetAllTripsAsync()
    {
        var response = await _client
            .From<TestTrip>()
            .Get();
        
        return response.Models;
    }

    // Dodaj nową podróż
    public async Task<TestTrip?> AddTripAsync(string name, string? description)
    {
        var newTrip = new TestTrip
        {
            Name = name,
            Description = description
        };

        var response = await _client
            .From<TestTrip>()
            .Insert(newTrip);

        return response.Models.FirstOrDefault();
    }

    // Usuń podróż
    public async Task DeleteTripAsync(Guid id)
    {
        await _client
            .From<TestTrip>()
            .Where(x => x.Id == id)
            .Delete();
    }
}


