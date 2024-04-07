namespace JokeApp.Models;

public partial class Joke
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int? Likes { get; set; }

    public int? Dislikes { get; set; }
}
