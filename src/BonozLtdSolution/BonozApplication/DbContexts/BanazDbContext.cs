namespace BonozApplication.DbContexts
{
    public class BanazDbContext : DbContext
    {
        public BanazDbContext(DbContextOptions<BanazDbContext> options) : base(options) { }

        #region All Entities

        #region User

        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }


        #endregion User


        #region Sales
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<ProductCategory> Categories { get; set; }
        //public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
       // public virtual DbSet<Review> Reviews { get; set; }

        #endregion Sales

        #endregion All Entities

        #region ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary key for UserRoles
            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => new { ur.AppUserId, ur.AppRoleId });

            // Configure the one-to-many relationship between User and UserRoles
            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.AppUser)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.AppUserId);

            // Configure the one-to-many relationship between AppRole and UserRoles
            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.AppRole)
                .WithMany(r => r.AppUserRoles)
                .HasForeignKey(ur => ur.AppRoleId);

            modelBuilder.Entity<ChatMessage>(e =>
            {
                e.HasOne(m => m.Sender).WithMany().OnDelete(DeleteBehavior.NoAction);
                e.HasOne(m => m.Receiver).WithMany().OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(modelBuilder);

        }

        #endregion ModelBuilder
    }
}
