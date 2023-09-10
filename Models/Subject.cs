using StudentManagement.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Data
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subjectId { get; set; }

        public string? Class { get; set; }

        public string? Subject1 { get; set; }
        
        public string? Subject2 { get; set; }
        
        public string? Subject3 { get; set; }
        
        public string? Subject4 { get; set; }
        
        public string? Subject5 { get; set; }
        
        public string? Subject6 { get; set; }
        
    }
}