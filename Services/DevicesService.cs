
namespace EA_Store.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly ApplicationDbContext _context;

        public DevicesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetDevices()
        {
            return _context.Device.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name.ToUpper() })
                .OrderBy(x => x.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
