namespace MvcIoC.Models
{
    public interface IProteinTrackingService
    {
        int Total { get; set; }
        int Goal { get; set; }
        void AddProtein(int amount);
    }
}