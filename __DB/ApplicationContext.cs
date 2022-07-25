using Microsoft.EntityFrameworkCore;
using DB.Entities;

namespace DB;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<UserPermission> UsersPermissions { get; set; }
    public virtual DbSet<Advertisement> Advertisements { get; set; }
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<News> News { get; set; }
    public virtual DbSet<ContactUs> ContactsUs { get; set; }
    public virtual DbSet<Award> Awards { get; set; }
    public virtual DbSet<Partner> Partners { get; set; }
    public virtual DbSet<Document> Documents { get; set; }
    public virtual DbSet<AppFeature> AppFeatures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfig());
        modelBuilder.ApplyConfiguration(new PermissionConfig());
        modelBuilder.ApplyConfiguration(new UserPermissionConfig());
        modelBuilder.ApplyConfiguration(new AdvertisementConfig());
        modelBuilder.ApplyConfiguration(new ArticleConfig());
        modelBuilder.ApplyConfiguration(new NewsConfig());
        modelBuilder.ApplyConfiguration(new ContactUsConfig());
        modelBuilder.ApplyConfiguration(new AwardConfig());
        modelBuilder.ApplyConfiguration(new PartnerConfig());
        modelBuilder.ApplyConfiguration(new DocumentConfig());
        modelBuilder.ApplyConfiguration(new AppFeatureConfig());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
