
namespace EA_Store.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
           return _context.Category.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                       .OrderBy(x => x.Text)
                       .AsNoTracking()
                     .ToList();
        }
    }
}
