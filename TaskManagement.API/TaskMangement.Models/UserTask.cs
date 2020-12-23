using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMangement.Models
{
    [Table(name: "UserTask")]
    public class UserTask
    {
        [Key]
        public int TaskId { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        public string TaskName { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string TaskDescription { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsOpen { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Status { get; set; }

        [Required]
        public virtual User User
        {
            get; set;
        }
    }

}
