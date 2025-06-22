using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using keykeeper_backend.Domain.Entities;
using keykeeper_backend.domain.Entities;

namespace keykeeper_backend.Infrastructure.KeykepperDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Municipalite> Municipalites { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SaleListing> SaleListings { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Street> Streets {  get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<ListingPhoto> ListingsPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(a => a.AddressId);

                entity.HasOne(a => a.Settlement)
                    .WithMany(a => a.Addresses)
                    .HasForeignKey(a => a.SettlementId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.District)
                    .WithMany(a => a.Addresses)
                    .HasForeignKey(a => a.DistrictId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(a => a.Street)
                    .WithMany(a => a.Addresses)
                    .HasForeignKey(a => a.StreetId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.Property(a => a.HouseNumber)
                    .HasMaxLength(20);

                /*entity.HasIndex(a => a.Location)
                    .HasMethod("GIST");*/
            });

            modelBuilder.Entity<District>().HasKey(d => d.DistrictId);

            modelBuilder.Entity<Municipalite>(entity =>
            {
                entity.HasKey(m => m.MunicipalityId);

                entity.HasOne(m => m.Region)
                .WithMany(m => m.Municipalites)
                .HasForeignKey(m => m.RegionId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PropertyType>().HasKey(p => p.PropertyTypeId);

            modelBuilder.Entity<Region>().HasKey(r => r.RegionId);

            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);

            modelBuilder.Entity<SaleListing>(entity =>
            {
                entity.HasKey(s => s.SaleListingId);

                entity.HasOne(s => s.PropertyType)
                .WithMany(s => s.SaleListings)
                .HasForeignKey(s => s.PropertyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.User)
                .WithMany(u => u.Listings)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Address)
                .WithMany(u => u.SaleListings)
                .HasForeignKey(s=> s.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(s => s.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<Settlement>(entity =>
            {
                entity.HasKey(s => s.SettlementId);

                entity.HasOne(s => s.Municipalite)
                .WithMany(s=> s.Settlements)
                .HasForeignKey(s => s.MunicipalityId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Street>().HasKey(s => s.StreetId);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.HasOne(u => u.Role)
                .WithMany(u=> u.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(u => u.FirstName)
                .HasMaxLength(100);

                entity.Property(u => u.LastName)
                .HasMaxLength(100);

                entity.Property(u => u.Email).HasMaxLength(255);

                entity.Property(u => u.PasswordHash).HasMaxLength(255);

                entity.Property(u => u.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(uf => new { uf.UserId, uf.SaleListingId });

                entity.HasOne(uf => uf.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(uf => uf.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uf => uf.SaleListing)
                    .WithMany(sl => sl.Favorites)
                    .HasForeignKey(uf => uf.SaleListingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ListingPhoto>(entity =>
            {
                entity.HasKey(lp => lp.ListingPhotoId);

                entity.HasOne(lp => lp.SaleListing)
                    .WithMany(u => u.ListingPhotos)
                    .HasForeignKey(lp => lp.SaleListingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
