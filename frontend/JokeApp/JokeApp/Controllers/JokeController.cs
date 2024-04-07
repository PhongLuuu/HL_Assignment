using JokeApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace JokeApp.Controllers;

public class JokeController : Controller
{
    private readonly IJokeRepository _jokeRepository;
    public JokeController(IJokeRepository jokeRepository)
    {
        _jokeRepository = jokeRepository;
    }
    public async Task<IActionResult> Index()
    {
        int? jokeId = null;
        if (Request.Cookies.TryGetValue("jokeId", out var jokeIdValue))
        {
            int.TryParse(jokeIdValue, out int parsedJokeId);
            jokeId = parsedJokeId;
        }

        jokeId ??= 1;

        var joke = await _jokeRepository.GetJokeToShow(jokeId.Value);
        return View(joke);
    }


    [HttpPost]
    public async Task<IActionResult> NextJoke(int id, bool isFunny)
    {
        if (isFunny)
        {
            await _jokeRepository.AddLike(id);
        }
        else
        {
            await _jokeRepository.AddDislike(id);
        }
        //Get new joke id
        int newId = id + 1;

        //Replace old cookie
        if (Request.Cookies.ContainsKey("jokeId"))
        {
            Response.Cookies.Delete("jokeId");
        }
        Response.Cookies.Append("jokeId", newId.ToString(), new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(1)
        });

        //Get new joke
        var joke = await _jokeRepository.GetJokeToShow(newId);
        if (joke != null)
        {
            return Json(new { joke });
        }
        else
        {
            return NotFound("Joke not found.");
        }
    }
}