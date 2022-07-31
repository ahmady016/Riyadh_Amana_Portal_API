using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public enum Gender : byte
{
    Male = 1,
    Female = 2
}

public class User : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string NationalId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Mobile { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    public virtual ICollection<UserPermission> UsersPermissions { get; set; } = new HashSet<UserPermission>();
}

public class UserConfig : EntityConfig<User, Guid>
{
    public override void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");
        base.Configure(entity);

        entity.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("first_name")
            .HasColumnType("nvarchar(30)");
        
        entity.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(70)
            .HasColumnName("last_name")
            .HasColumnType("nvarchar(70)");

        entity.Property(e => e.Gender)
            .IsRequired()
            .HasColumnName("gender")
            .HasColumnType("tinyint");

        entity.Property(e => e.BirthDate)
            .HasColumnName("birth_date")
            .HasColumnType("datetime");

        entity.Property(e => e.NationalId)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("national_id")
            .HasColumnType("varchar(15)");

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("email")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(75)
            .HasColumnName("password")
            .HasColumnType("varchar(75)");

        entity.Property(e => e.Mobile)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("mobile")
            .HasColumnType("varchar(20)");

        entity.HasIndex(e => e.Email)
            .HasDatabaseName("users_email_unique_index")
            .IsUnique();

    }
}
