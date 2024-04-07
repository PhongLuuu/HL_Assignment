using JokeApp.Models;

namespace JokeApp.Interfaces
{
    public interface IJokeRepository
    {
        Task<IEnumerable<JokeApp.Models.Joke>> GetJokesToShow();
        Task<Joke> GetJokeToShow(int jokeId);
        Task AddLike(int id);
        Task AddDislike(int id);
        Task<bool> SaveToDb();
    }
}
