using System;

namespace TaskManagement.API.Dtos
{
    public class UpdateTaskDto
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
