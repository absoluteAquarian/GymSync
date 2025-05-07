using GymSync.Models;

namespace GymSync.Views
{
    public record class EquipmentView(int EquipmentID, string Name, string Location, DateTime LastMaintenance, bool InUse);
}
