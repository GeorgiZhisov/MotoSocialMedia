using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoGrid.Data.Models;

namespace MotoGrid.Data.Configurations;

public class ListingCategoryConfiguration : IEntityTypeConfiguration<ListingCategory>
{
    public void Configure(EntityTypeBuilder<ListingCategory> category)
    {
        category.HasIndex(c => c.Name).IsUnique();
    }
}

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> listing)
    {
        listing.Property(l => l.Price).HasPrecision(18, 2);

        listing.HasOne(l => l.Category)
            .WithMany(c => c.Listings)
            .HasForeignKey(l => l.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        listing.HasMany(l => l.Images)
            .WithOne(i => i.Listing)
            .HasForeignKey(i => i.ListingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for the most common search filters
        listing.HasIndex(l => new { l.CategoryId, l.Status, l.Price });
        listing.HasIndex(l => l.EngineCc);
        listing.HasIndex(l => l.City);
    }
}

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> review)
    {
        review.HasOne(r => r.Listing)
            .WithMany()
            .HasForeignKey(r => r.ListingId)
            .OnDelete(DeleteBehavior.SetNull);

        review.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Reviews_Rating", "[Rating] BETWEEN 1 AND 5");
            t.HasCheckConstraint("CK_Reviews_NotSelf", "[ReviewerId] <> [SellerId]");
        });
    }
}

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> conversation)
    {
        conversation.HasOne(c => c.Buyer)
            .WithMany()
            .HasForeignKey(c => c.BuyerId);

        conversation.HasOne(c => c.Seller)
            .WithMany()
            .HasForeignKey(c => c.SellerId);

        conversation.HasOne(c => c.Listing)
            .WithMany()
            .HasForeignKey(c => c.ListingId)
            .OnDelete(DeleteBehavior.SetNull);

        conversation.HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        conversation.HasIndex(c => new { c.BuyerId, c.SellerId, c.ListingId });
    }
}

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> message)
    {
        message.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId);

        message.HasIndex(m => new { m.ConversationId, m.CreatedOn });
    }
}
