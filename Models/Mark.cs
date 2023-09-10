using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Data
{
    public class Mark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarkId { get; set; }

        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Class { get; set; }
        
        public string? Subject1 { get; set; }
        public string? Subject1Name { get; set; }

        public string? Subject2 { get; set; }
        public string? Subject2Name { get; set; }

        public string? Subject3 { get; set; }
        public string? Subject3Name { get; set; }

        public string? Subject4 { get; set; }
        public string? Subject4Name { get; set; }

        public string? Subject5 { get; set; }
        public string? Subject5Name { get; set; }

        public string? Subject6 { get; set; }
        public string? Subject6Name { get; set; }
        
        public int  Total { get; set; }
        

    }
}