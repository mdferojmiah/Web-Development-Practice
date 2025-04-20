using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodApp.Models;
namespace FoodApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<DishIngredient>().HasKey(di => new {
                di.DishId,
                di.IngredientId
            });
            modelBuilder.Entity<DishIngredient>().HasOne(di => di.Dish).WithMany(d => d.DishIngredients).HasForeignKey(di => di.DishId);
            modelBuilder.Entity<DishIngredient>().HasOne(di => di.Ingredient).WithMany(d => d.DishIngredients).HasForeignKey(di => di.IngredientId);

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Pizza", ImageUrl = "https://img.freepik.com/free-photo/pizza-pizza-filled-with-tomatoes-salami-olives_140725-1200.jpg?semt=ais_hybrid&w=740", Price = 9.99 },
                new Dish { Id = 2, Name = "Burger", ImageUrl = "https://img.freepik.com/free-photo/side-view-pizza-with-slices-bell-pepper-pizza-slices-flour-board-cookware_176474-3185.jpg?semt=ais_hybrid&w=740", Price = 5.99 },
                new Dish { Id = 3, Name = "Pasta", ImageUrl = "https://img.freepik.com/free-psd/top-view-delicious-pizza_23-2151868964.jpg?semt=ais_hybrid&w=740", Price = 7.99 }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tomato" },
                new Ingredient { Id = 2, Name = "Cheese" },
                new Ingredient { Id = 3, Name = "Olives" },
                new Ingredient { Id = 4, Name = "Lettuce" },
                new Ingredient { Id = 5, Name = "Beef" },
                new Ingredient { Id = 6, Name = "Chicken" }
            );

            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1, IngredientId = 1 },
                new DishIngredient { DishId = 1, IngredientId = 2 },
                new DishIngredient { DishId = 1, IngredientId = 3 },
                new DishIngredient { DishId = 2, IngredientId = 4 },
                new DishIngredient { DishId = 2, IngredientId = 5 },
                new DishIngredient { DishId = 3, IngredientId = 6 }
            );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}