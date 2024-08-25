
namespace EA_Store.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}