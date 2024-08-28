namespace EA_Store.Services
{
    public interface IDevicesService
    {
        IEnumerable<SelectListItem> GetDevices();
    }
}
