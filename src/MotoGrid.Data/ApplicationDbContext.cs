using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoGrid.Common.Constants;
using MotoGrid.Data.Common;
using MotoGrid.Data.Models;

namespace MotoGrid.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Motorcycle> Motorcycles { get; set; } = null!;

    public DbSet<Post> Posts { get; set; } = null!;

    public DbSet<Media> Media { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<PostLike> PostLikes { get; set; } = null!;

    public DbSet<Follow> Follows { get; set; } = null!;

    public DbSet<Story> Stories { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<GroupMember> GroupMembers { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<EventParticipant> EventParticipants { get; set; } = null!;

    public DbSet<Route> Routes { get; set; } = null!;

    public DbSet<ListingCategory> ListingCategories { get; set; } = null!;

    public DbSet<Listing> Listings { get; set; } = null!;

    public DbSet<ListingImage> ListingImages { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;

    public DbSet<Conversation> Conversations { get; set; } = null!;

    public DbSet<Message> Messages { get; set; } = null!;

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyEntityRules();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.ApplyEntityRules();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Identity tables (users, roles, claims...)
        base.OnModelCreating(builder);

        // All IEntityTypeConfiguration classes from the Configurations folder
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // A user is never physically deleted (soft delete), so the relations to the
        // users must not cascade. This also avoids the "multiple cascade paths" error of SQL Server.
        var userForeignKeys = builder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.Namespace == typeof(Post).Namespace)
            .SelectMany(e => e.GetForeignKeys())
            .Where(fk => fk.PrincipalEntityType.ClrType == typeof(ApplicationUser));

        foreach (var foreignKey in userForeignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    /// <summary>
    /// Fills in CreatedOn for new entities and ModifiedOn for changed ones.
    /// </summary>
    private void ApplyEntityRules()
    {
        var now = DateTime.UtcNow;

        // The default Identity registration page does not ask for a display name yet,
        // so until it is customized the name is taken from the e-mail (before the @).
        foreach (var entry in this.ChangeTracker.Entries<ApplicationUser>()
                     .Where(e => e.State == EntityState.Added && string.IsNullOrWhiteSpace(e.Entity.DisplayName)))
        {
            var userName = entry.Entity.UserName ?? entry.Entity.Email ?? "user";
            var displayName = userName.Split('@')[0];
            entry.Entity.DisplayName = displayName.Length > ValidationConstants.User.DisplayNameMaxLength
                ? displayName[..ValidationConstants.User.DisplayNameMaxLength]
                : displayName;
        }

        foreach (var entry in this.ChangeTracker.Entries<IAuditInfo>())
        {
            if (entry.State == EntityState.Added && entry.Entity.CreatedOn == default)
            {
                entry.Entity.CreatedOn = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedOn = now;
            }
        }
    }
}
