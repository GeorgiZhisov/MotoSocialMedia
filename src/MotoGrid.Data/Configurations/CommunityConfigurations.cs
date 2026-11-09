using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoGrid.Data.Models;

namespace MotoGrid.Data.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> group)
    {
        group.HasIndex(g => g.Name).IsUnique();

        group.HasOne(g => g.Creator)
            .WithMany()
            .HasForeignKey(g => g.CreatorId);

        group.HasMany(g => g.Members)
            .WithOne(m => m.Group)
            .HasForeignKey(m => m.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
{
    public void Configure(EntityTypeBuilder<GroupMember> member)
    {
        member.HasKey(m => new { m.GroupId, m.UserId });
    }
}

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> ev)
    {
        ev.HasOne(e => e.Organizer)
            .WithMany()
            .HasForeignKey(e => e.OrganizerId);

        ev.HasOne(e => e.Group)
            .WithMany(g => g.Events)
            .HasForeignKey(e => e.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        ev.HasOne(e => e.Route)
            .WithMany(r => r.Events)
            .HasForeignKey(e => e.RouteId)
            .OnDelete(DeleteBehavior.SetNull);

        ev.HasMany(e => e.Participants)
            .WithOne(p => p.Event)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        // Upcoming events are listed by start date
        ev.HasIndex(e => e.StartsOn);
    }
}

public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> participant)
    {
        participant.HasKey(p => new { p.EventId, p.UserId });
    }
}
