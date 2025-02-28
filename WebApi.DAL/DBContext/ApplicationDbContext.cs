using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // For IdentityDbContext
using Microsoft.EntityFrameworkCore;

namespace WebApi.DAL.DBContext
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorAddress> AuthorAddress { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Publication> Publication { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    // Replace the connection string with your actual connection string.
        //    // Below is an example for a SQL Server database.
        //    //optionsBuilder.UseSqlServer(@"Server=DESKTOP-49MCF8P\SQLEXPRESS; Database = WebApi ;Integrated Security = true;Trusted_Connection=True;TrustServerCertificate=True;");

        //    // For SQLite, it might look like this:

        //    // optionsBuilder.UseSqlite("Data Source=blogging.db");

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookId);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Genre).IsRequired().HasMaxLength(50);
                entity.HasOne(b => b.Publication).WithMany(p => p.Book).HasForeignKey(b => b.PublicationId);
                entity.HasMany(b => b.Authors).WithMany(a => a.Books);
                entity.HasMany(b => b.Borrows).WithOne(br => br.Book).HasForeignKey(br => br.BookId);

            });

        }
        public void ExecuteAddingStudents()
        {
            this.Database.ExecuteSqlRaw("EXEC AddingStudents");
        }
    }
}