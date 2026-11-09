using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoGrid.Data.Models;

namespace MotoGrid.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> post)
    {
        post.HasOne(p => p.Motorcycle)
            .WithMany()
            .HasForeignKey(p => p.MotorcycleId)
            .OnDelete(DeleteBehavior.SetNull);

        post.HasOne(p => p.Group)
            .WithMany(g => g.Posts)
            .HasForeignKey(p => p.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        post.HasMany(p => p.Media)
            .WithOne(m => m.Post)
            .HasForeignKey(m => m.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        post.HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Feed query: newest posts first
        post.HasIndex(p => p.CreatedOn);
    }
}

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> comment)
    {
        comment.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId);
    }
}

public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> like)
    {
        like.HasKey(l => new { l.PostId, l.UserId });

        like.HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        like.HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId);
    }
}

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> follow)
    {
        follow.HasKey(f => new { f.FollowerId, f.FolloweeId });

        // A user cannot follow themselves
        follow.ToTable(t => t.HasCheckConstraint("CK_Follows_NotSelf", "[FollowerId] <> [FolloweeId]"));
    }
}

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> story)
    {
        // Used to find the active (not expired) stories quickly
        story.HasIndex(s => s.ExpiresOn);
    }
}
