using JokeApp.Interfaces;
using JokeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JokeApp.Repositories;

public class JokeRepository : IJokeRepository
{
    private readonly JokesContext _context;
    public JokeRepository(JokesContext context)
    {
        _context = context;
    }
    public async Task AddDislike(int id)
    {
        var jokeToUpdate = await _context.Jokes.FindAsync(id);
        jokeToUpdate.Dislikes++;
        _context.SaveChangesAsync();
    }
    public async Task AddLike(int id)
    {
        var jokeToUpdate = await _context.Jokes.FindAsync(id);
        jokeToUpdate.Likes++;
        _context.SaveChangesAsync();
    }

    public async Task<bool> SaveToDb()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Joke>> GetJokesToShow()
    {
        return await _context.Jokes.ToListAsync();
    }

    public async Task<Joke> GetJokeToShow(int jokeId)
    {
        var jokeToShow = await _context.Jokes.FirstOrDefaultAsync(j => j.Id == jokeId);

        return jokeToShow;
    }
}
