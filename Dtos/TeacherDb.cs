using Microsoft.EntityFrameworkCore;

namespace TodoApi.Dtos;

public class TeacherDb:DbContext
{
    public TeacherDb(DbContextOptions<TeacherDb> options):base(options){}
    public DbSet<Teacher> Teachers => Set<Teacher>();
}
