using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityTodo.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<TodoItem?> TodoItems { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
}
