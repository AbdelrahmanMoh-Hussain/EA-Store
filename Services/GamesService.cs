


namespace EA_Store.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public List<Game> GetAllGames()
        {
            return _context.Game.Include(x => x.Category)
                .Include(x => x.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetGameById(int id)
        {
            return _context.Game
                .Include(x => x.Category)
                .Include(x => x.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game newGame = new Game()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = coverName,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices
                    .Select(x => new GameDevice { DeviceId = x})
                    .ToList()
            };
            await _context.AddAsync(newGame);
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = await _context.Game
                .Include(x => x.Devices)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if(game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;


            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices
                .Select(x => new GameDevice { DeviceId = x })
                .ToList();

            

            if (hasNewCover)
                game.Cover = await SaveCover(model.Cover!);
            
            var effectedRows = await _context.SaveChangesAsync();
            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var oldCoverPath = Path.Combine(_imagesPath, oldCover);
                    if (File.Exists(oldCoverPath))
                        File.Delete(oldCoverPath);
                }
                return game;
            }
            else
            {
                if (hasNewCover)
                {
                    var newCoverPath = Path.Combine(_imagesPath, game.Cover);
                    if (File.Exists(newCoverPath))
                        File.Delete(newCoverPath);
                }
                return null;
            }
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
