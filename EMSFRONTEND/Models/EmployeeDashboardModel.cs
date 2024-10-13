namespace EMSFRONTEND.Models
{
    public class EmployeeDashboardModel
    {
        public EmployeeDashboardCardModel DashboardCardModel { get; set; }
        public List<DashboardTableModel> DashboardTableModel { get; set; }
    }

    public class EmployeeDashboardCardModel
    {
        public int TotalTaskAssigned { get; set; }
        public int TotalTaskCompleted { get; set; }
    }
}
