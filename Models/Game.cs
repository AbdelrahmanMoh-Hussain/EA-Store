
namespace EA_Store.Models
{
    public class Game
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Cover { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

    }
}
