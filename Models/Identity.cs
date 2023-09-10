using StudentManagement.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Data
{
    public class Identity
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? Designation { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string? Area { get; set; }
        public string? BloodGroup { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}