using System.ComponentModel.DataAnnotations;

namespace EA_Store.Models
{
    public class Device
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Icon { get; set; }

    }
}
