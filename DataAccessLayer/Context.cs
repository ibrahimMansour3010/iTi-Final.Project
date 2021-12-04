using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Admin;
using Models.Cart;
using Models.CartItem;
using Models.Customer;
using Models.Department;
using Models.Product;
using Models.ProductCategory;
using Models.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Context : IdentityDbContext<CustomerEntity>
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AdminEntity> AdminEntities { get; set; }
        public DbSet<CartEntity> CartEntities { get; set; }
        public DbSet<CartItemEntity> CartItemEntities { get; set; }
        public DbSet<CustomerEntity> CustomerEntities { get; set; }
        public DbSet<DepartmentEntity> DepartmentEntities { get; set; }
        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageEntityConfiguration());

            // one to many Admin To Products
            modelBuilder.Entity<ProductEntity>()
                .HasOne(prd => prd.Admin)
                .WithMany(admin => admin.Products)
                .HasForeignKey(pdr=>pdr.AdmainID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many Customer To CartItem
            modelBuilder.Entity<CartItemEntity>()
                .HasOne(cartITem => cartITem.CustomerEntity)
                .WithMany(customer => customer.CartItemEntities)
                .HasForeignKey(cartItem=>cartItem.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many Product To CartItem
            modelBuilder.Entity<CartItemEntity>()
                .HasOne(cartITem => cartITem.ProductEntity)
                .WithMany(pdr => pdr.CartItemEntities)
                .HasForeignKey(cartItem=>cartItem.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many Cart To CartItem
            //modelBuilder.Entity<CartItemEntity>()
            //    .HasOne(cartITem => cartITem.CartEntity)
            //    .WithMany(cart => cart.CartItemEntities)
            //    .HasForeignKey(cartItem => cartItem.CartID)
            //    .OnDelete(DeleteBehavior.Cascade);
            // one to many Deparment to Category
            modelBuilder.Entity<CategoryEntity>()
                .HasOne(cat => cat.Department)
                .WithMany(dept => dept.CategoryEntities)
                .HasForeignKey(cat=>cat.DepartmentID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many Category To Products
            modelBuilder.Entity<ProductEntity>()
                .HasOne(pdr => pdr.Category)
                .WithMany(cat => cat.Products)
                .HasForeignKey(pdr=>pdr.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many Product To ProductImage
            modelBuilder.Entity<ProductImageEntity>()
                .HasOne(pdrimg => pdrimg.ProductEntity)
                .WithMany(prd => prd.ProductImageEntities)
                .HasForeignKey(pdeimg=>pdeimg.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to one Product To Cart
            modelBuilder.Entity<CustomerEntity>()
             .HasOne<CartEntity>(customer => customer.CartEntity)
             .WithOne(cart => cart.CustomerEntity)
             .OnDelete(DeleteBehavior.Cascade)
             .HasForeignKey<CartEntity>(cart => cart.CustomerID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
