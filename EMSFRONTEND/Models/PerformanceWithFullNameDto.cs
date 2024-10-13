namespace EMSFRONTEND.Models
{


    public class PerformanceWithFullNameDto
    {
        public int PerformanceId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }  // Added FullName

        public int NumberOfTasksAssigned { get; set; }

        public int CompletedCount { get; set; }

        public double PerformancePercentage
        {
            get
            {
                if (NumberOfTasksAssigned == 0) return 0;
                return Math.Round((double)CompletedCount / NumberOfTasksAssigned * 100, 2);
            }
        }
    }
}
