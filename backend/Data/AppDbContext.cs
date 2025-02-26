using Microsoft.EntityFrameworkCore;
using Backend.Models; // 🔹 Certifique-se de que está importando os modelos corretos

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        try{
            Database.EnsureCreated();
            Console.WriteLine("Conectado ao banco de dados SQLite!");
        }
        catch (Exception ex){
            Console.WriteLine($"Erro ao conectar no banco: {ex.Message}");
        }
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
