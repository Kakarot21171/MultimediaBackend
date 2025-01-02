using MultimediaManagementBackend.Models;
using Microsoft.EntityFrameworkCore;


namespace MultimediaManagementBackend.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define a DbSet for File Metadata
        public DbSet<FileMetadata> Files { get; set; }
    }
}

