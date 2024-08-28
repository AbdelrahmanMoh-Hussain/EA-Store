
namespace EA_Store.Services
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetCategories();
    }   
}
