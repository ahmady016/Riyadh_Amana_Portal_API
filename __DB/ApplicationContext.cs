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
    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<Photo> Photos { get; set; }
    public virtual DbSet<AppPage> AppPages { get; set; }
    public virtual DbSet<PageKey> PagesKeys { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Reply> Replies { get; set; }
    public virtual DbSet<Nav> Navs { get; set; }
    public virtual DbSet<NavLink> NavsLinks { get; set; }
    public virtual DbSet<Video> Videos { get; set; }
    public virtual DbSet<Lookup> Lookups { get; set; }

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
        modelBuilder.ApplyConfiguration(new AlbumConfig());
        modelBuilder.ApplyConfiguration(new PhotoConfig());
        modelBuilder.ApplyConfiguration(new AppPageConfig());
        modelBuilder.ApplyConfiguration(new PageKeyConfig());
        modelBuilder.ApplyConfiguration(new CommentConfig());
        modelBuilder.ApplyConfiguration(new ReplyConfig());
        modelBuilder.ApplyConfiguration(new NavConfig());
        modelBuilder.ApplyConfiguration(new NavLinkConfig());
        modelBuilder.ApplyConfiguration(new VideoConfig());
        modelBuilder.ApplyConfiguration(new LookupConfig());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
