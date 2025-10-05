using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorGitHubTest.Models;

[Table("test_trips")]
public class TestTrip : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }
}


