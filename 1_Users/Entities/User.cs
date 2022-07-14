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
}

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

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
            .HasMaxLength(50)
            .HasColumnName("password")
            .HasColumnType("varchar(50)");

        entity.Property(e => e.Mobile)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("mobile")
            .HasColumnType("varchar(20)");

        entity.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_deleted")
            .HasColumnType("bit");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_active")
            .HasColumnType("bit");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime");
        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasDefaultValue("app_dev")
            .HasMaxLength(100)
            .HasColumnName("created_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime");
        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasColumnName("updated_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("datetime");
        entity.Property(e => e.DeletedBy)
            .HasMaxLength(100)
            .HasColumnName("deleted_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.ActivatedAt)
            .HasColumnName("activated_at")
            .HasColumnType("datetime");
        entity.Property(e => e.ActivatedBy)
            .HasMaxLength(100)
            .HasColumnName("activated_by")
            .HasColumnType("varchar(100)");
    }
}
