using BlazorGitHubTest.Models;

namespace BlazorGitHubTest.Services;

public interface ISupabaseService
{
    Task<List<TestTrip>> GetAllTripsAsync();
    Task<TestTrip?> AddTripAsync(string name, string? description);
    Task DeleteTripAsync(Guid id);
}


