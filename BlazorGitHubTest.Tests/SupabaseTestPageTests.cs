using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bunit;
using Xunit;
using BlazorGitHubTest.Models;
using BlazorGitHubTest.Pages;
using BlazorGitHubTest.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorGitHubTest.Tests;

public class FakeSupabaseService : ISupabaseService
{
    public List<TestTrip> Trips { get; } = new();

    public Task<List<TestTrip>> GetAllTripsAsync()
    {
        return Task.FromResult(new List<TestTrip>(Trips));
    }

    public Task<TestTrip?> AddTripAsync(string name, string? description)
    {
        var created = new TestTrip
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
        Trips.Add(created);
        return Task.FromResult<TestTrip?>(created);
    }

    public Task DeleteTripAsync(Guid id)
    {
        Trips.RemoveAll(t => t.Id == id);
        return Task.CompletedTask;
    }

    public Task<TestTrip?> UpdateTripAsync(TestTrip trip)
    {
        var index = Trips.FindIndex(t => t.Id == trip.Id);
        if (index >= 0)
        {
            Trips[index] = trip;
            return Task.FromResult<TestTrip?>(trip);
        }
        return Task.FromResult<TestTrip?>(null);
    }
}

public class SupabaseTestPageTests : TestContext
{
    [Fact]
    public void ShowsEmptyMessage_WhenNoTrips()
    {
        var fake = new FakeSupabaseService();
        Services.AddSingleton<ISupabaseService>(fake);

        var cut = RenderComponent<SupabaseTest>();

        Assert.Contains("Brak podróży", cut.Markup);
    }

    [Fact]
    public void AddsTrip_AndShowsSuccessMessage()
    {
        var fake = new FakeSupabaseService();
        Services.AddSingleton<ISupabaseService>(fake);

        var cut = RenderComponent<SupabaseTest>();

        // Set inputs
        cut.Find("input[placeholder='Nazwa podróży']").Change("Wycieczka");
        cut.Find("input[placeholder='Opis (opcjonalnie)']").Change("Opis");

        // Click add
        cut.Find("button.btn.btn-primary").Click();

        // After add, wait for UI update and assert
        cut.WaitForAssertion(() => Assert.Contains("Podróż dodana pomyślnie!", cut.Markup));
    }
}



