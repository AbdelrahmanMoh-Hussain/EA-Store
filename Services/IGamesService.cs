using EA_Store.ViewModels;

namespace EA_Store.Services
{
    public interface IGamesService
    {
        List<Game> GetAllGames();
        Game? GetGameById(int id);
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Update(EditGameFormViewModel model);
    }
}
