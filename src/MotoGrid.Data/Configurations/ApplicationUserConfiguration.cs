using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoGrid.Data.Models;

namespace MotoGrid.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> user)
    {
        user.HasMany(u => u.Motorcycles)
            .WithOne(m => m.Owner)
            .HasForeignKey(m => m.OwnerId);

        user.HasMany(u => u.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId);

        user.HasMany(u => u.Stories)
            .WithOne(s => s.Author)
            .HasForeignKey(s => s.AuthorId);

        user.HasMany(u => u.Followers)
            .WithOne(f => f.Followee)
            .HasForeignKey(f => f.FolloweeId);

        user.HasMany(u => u.Following)
            .WithOne(f => f.Follower)
            .HasForeignKey(f => f.FollowerId);

        user.HasMany(u => u.GroupMemberships)
            .WithOne(gm => gm.User)
            .HasForeignKey(gm => gm.UserId);

        user.HasMany(u => u.EventParticipations)
            .WithOne(ep => ep.User)
            .HasForeignKey(ep => ep.UserId);

        user.HasMany(u => u.Routes)
            .WithOne(r => r.Author)
            .HasForeignKey(r => r.AuthorId);

        user.HasMany(u => u.Listings)
            .WithOne(l => l.Seller)
            .HasForeignKey(l => l.SellerId);

        user.HasMany(u => u.ReceivedReviews)
            .WithOne(r => r.Seller)
            .HasForeignKey(r => r.SellerId);

        user.HasMany(u => u.WrittenReviews)
            .WithOne(r => r.Reviewer)
            .HasForeignKey(r => r.ReviewerId);

        user.HasIndex(u => u.IsDeleted);
    }
}
