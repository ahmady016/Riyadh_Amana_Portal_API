using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using DB.Common;

namespace DB.Entities;

public class RefreshToken : Entity<Guid>
{
    public string Value { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string CreatedIP { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string RevokedIP { get; set; }
    public string RevokedReason { get; set; }
    public Guid UserId { get; set; }

    [NotMapped]
    public bool IsRevoked => RevokedAt != null;
    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    [NotMapped]
    public bool IsValid => !IsRevoked && !IsExpired;

    public User User { get; set; }
}

public class RefreshTokenConfig : EntityConfig<RefreshToken, Guid>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> entity)
    {
        entity.ToTable("refresh_tokens");
        base.Configure(entity);

        entity.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("value")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.ExpiresAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("expires_at");

        entity.Property(e => e.CreatedIP)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("created_ip")
            .HasColumnType("varchar(15)");

        entity.Property(e => e.RevokedAt)
            .HasColumnType("datetime")
            .HasColumnName("revoked_at");

        entity.Property(e => e.RevokedIP)
            .HasMaxLength(15)
            .HasColumnName("revoked_ip")
            .HasColumnType("varchar(15)");

        entity.Property(e => e.RevokedReason)
            .HasMaxLength(200)
            .HasColumnName("revoked_reason")
            .HasColumnType("varchar(200)");

        entity.Property(refresh_token => refresh_token.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uniqueidentifier");

        entity.HasOne(refresh_token => refresh_token.User)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(refresh_token => refresh_token.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("refresh_tokens_users_fk");

    }
}
