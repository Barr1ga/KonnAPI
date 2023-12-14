namespace KonnAPI.Data;

using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {

    }
    public DbSet<User> Users { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<ContactCategory> ContactCategories { get; set; }

    public DbSet<Social> Socials { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>()
          .HasMany(c => c.Addresses)
          .WithOne(a => a.Contact)
          .HasForeignKey(a => a.ContactId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contact>()
          .HasMany(c => c.Socials)
          .WithOne(cc => cc.Contact)
          .HasForeignKey(cc => cc.ContactId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contact>()
          .HasMany(c => c.ContactCategories)
          .WithOne(cc => cc.Contact)
          .HasForeignKey(cc => cc.ContactId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
          .HasMany(c => c.ContactCategories)
          .WithOne(cc => cc.Category)
          .HasForeignKey(cc => cc.CategoryId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
          .HasMany(w => w.Workspaces)
          .WithOne(a => a.User)
          .HasForeignKey(a => a.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Workspace>()
          .HasMany(w => w.Categories)
          .WithOne(a => a.Workspace)
          .HasForeignKey(a => a.WorkspaceId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Workspace>()
          .HasMany(w => w.Contacts)
          .WithOne(a => a.Workspace)
          .HasForeignKey(a => a.WorkspaceId)
          .OnDelete(DeleteBehavior.Cascade);
    }
}
