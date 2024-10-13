using System;

namespace EMSFRONTEND.Models
{
    public class TaskWithFullNameDto
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }  // FullName added here

        public DateTime AssignedDate { get; set; }

        public DateTime DeadlineDate { get; set; }

        public string Description { get; set; }

        public string TaskStatus { get; set; }

        public string Resources { get; set; }
    }
}