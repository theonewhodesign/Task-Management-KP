using System;

namespace TaskManagement.API.Dtos
{
    public class CreateTaskDto
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? UserId { get; set; }
    }
}
