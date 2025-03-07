using Microsoft.EntityFrameworkCore;

namespace TodoApi.Dtos;

public class StudentDb:DbContext
{
    public StudentDb(DbContextOptions<StudentDb> options):base(options){}

    public DbSet<Student> Students => Set<Student>();
}
