using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class UserPermission : Entity<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}

public class UserPermissionConfig : EntityConfig<UserPermission, Guid>
{
    public override void Configure(EntityTypeBuilder<UserPermission> entity)
    {
        entity.ToTable("users_permissions");
        base.Configure(entity);

        entity.Property(user_permission => user_permission.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(user_permission => user_permission.PermissionId)
            .HasColumnName("permission_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(user_permission => new { user_permission.UserId, user_permission.PermissionId })
            .HasDatabaseName("user_id_permission_id_unique_index")
            .IsUnique(true);

        entity
            .HasOne(user_permission => user_permission.User)
            .WithMany(user => user.UsersPermissions)
            .HasForeignKey(user_permission => user_permission.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("users_permissions_users_fk");

        entity
            .HasOne(user_permission => user_permission.Permission)
            .WithMany(user => user.UsersPermissions)
            .HasForeignKey(user_permission => user_permission.PermissionId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("users_permissions_permissions_fk");

    }
}
