using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;

namespace StudentManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<Identity> Identities { get; set; }
            public DbSet<Mark> Marks { get; set; }
            public DbSet<Subject> Subjects { get; set; }

            public void AddStudent(Identity student)
            {
                Identities.Add(student);
                SaveChanges();
            }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Identity>().ToTable("Identities");
                // modelBuilder.Entity<Salary>().ToTable("Salaries");
                // modelBuilder.Entity<Attendance>().ToTable("Attendances");
                // modelBuilder.Entity<Salary>().Property(s => s.SalaryAmount).HasColumnType("decimal(18, 2)");

                // modelBuilder.Entity<Attendance>()
                //     .Property(a => a.EmpId)
                //     .ValueGeneratedNever(); 

            }




    }
}
