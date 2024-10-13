namespace EMSFRONTEND.Models
{
    public class ManagerDashboardModel
    {
        public ManagerDashboardCardModel DashboardCardModel { get; set; }
        public List<DashboardTableModel> DashboardTableModel { get; set; }
    }

    public class DashboardTableModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string TaskTitle { get; set; }
        public string TaskStatus { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public int CompletedCount { get; set; }
    }

    public class ManagerDashboardCardModel
    {
        public int TotalEmployee { get; set; }
        public int TotalTask { get; set; }
    }
}
