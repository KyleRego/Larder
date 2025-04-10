﻿// <auto-generated />
using Larder.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Larder.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240727151733_SquashMigrations")]
    partial class SquashMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<string>("IngredientsId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipesId")
                        .HasColumnType("TEXT");

                    b.HasKey("IngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("Larder.Models.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("FoodId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ServingsProduced")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FoodId")
                        .IsUnique();

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2",
                            FoodId = "a9007fbc-aebd-41c9-8a6c-572aeee2c305",
                            Name = "Rice Roni Low Sodium Chicken Rice",
                            ServingsProduced = 0
                        });
                });

            modelBuilder.Entity("Larder.Models.RecipeIngredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("IngredientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UnitId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UnitId");

                    b.ToTable("RecipeIngredients");

                    b.HasData(
                        new
                        {
                            Id = "80fc6b88-9232-48f4-bbbb-5dd625b19268",
                            Amount = 1.0,
                            IngredientId = "618bf9b4-1458-4d5e-bd5c-76ceba568e11",
                            RecipeId = "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2",
                            UnitId = "10e1ebcb-5680-4656-b3ea-fb2635bb7089"
                        },
                        new
                        {
                            Id = "a3fbfc92-b1b5-47f5-a615-4ac5ce23a4a8",
                            Amount = 2.5,
                            IngredientId = "3c8420f7-2767-41f9-98ab-76fc051dd6e9",
                            RecipeId = "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2",
                            UnitId = "cebfdc72-4558-4371-95e2-78ce9e49a37e"
                        },
                        new
                        {
                            Id = "925b00cf-5fed-4423-8da2-654dc90c027b",
                            Amount = 1.0,
                            IngredientId = "2fdf50f5-f0dd-4e56-8e68-b051c47f787a",
                            RecipeId = "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2"
                        });
                });

            modelBuilder.Entity("Larder.Models.Unit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Units");

                    b.HasData(
                        new
                        {
                            Id = "90237dbe-7280-47f2-94b9-358081009b49",
                            Name = "Liters",
                            Type = 1
                        },
                        new
                        {
                            Id = "5db2c906-2de0-4d80-afaf-50dd74683f96",
                            Name = "Pounds",
                            Type = 2
                        },
                        new
                        {
                            Id = "a949e8d3-bfce-4aa0-90bb-5bcf2d4f38d0",
                            Name = "Grams",
                            Type = 0
                        },
                        new
                        {
                            Id = "ceae0ccc-5cbe-411d-a54a-71f99bb8fdef",
                            Name = "Milliliters",
                            Type = 1
                        },
                        new
                        {
                            Id = "10e1ebcb-5680-4656-b3ea-fb2635bb7089",
                            Name = "Tablespoons",
                            Type = 1
                        },
                        new
                        {
                            Id = "cebfdc72-4558-4371-95e2-78ce9e49a37e",
                            Name = "Cups",
                            Type = 1
                        });
                });

            modelBuilder.Entity("Larder.Models.Food", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.Property<int>("Calories")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Servings")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Food");

                    b.HasData(
                        new
                        {
                            Id = "a9007fbc-aebd-41c9-8a6c-572aeee2c305",
                            Name = "Chicken and rice",
                            Calories = 0,
                            Servings = 0
                        });
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.Property<double>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<string>("UnitId")
                        .HasColumnType("TEXT");

                    b.HasIndex("UnitId");

                    b.HasDiscriminator().HasValue("Ingredient");

                    b.HasData(
                        new
                        {
                            Id = "618bf9b4-1458-4d5e-bd5c-76ceba568e11",
                            Name = "Butter",
                            Quantity = 0.0
                        },
                        new
                        {
                            Id = "3c8420f7-2767-41f9-98ab-76fc051dd6e9",
                            Name = "Water",
                            Quantity = 0.0
                        },
                        new
                        {
                            Id = "2fdf50f5-f0dd-4e56-8e68-b051c47f787a",
                            Name = "Rice Roni Chicken Lower Sodium box",
                            Quantity = 0.0
                        });
                });

            modelBuilder.Entity("Larder.Models.Utensil", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.HasDiscriminator().HasValue("Utensil");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("Larder.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.HasOne("Larder.Models.Food", "Food")
                        .WithOne("Recipe")
                        .HasForeignKey("Larder.Models.Recipe", "FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("Larder.Models.RecipeIngredient", b =>
                {
                    b.HasOne("Larder.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.HasOne("Larder.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("Larder.Models.Food", b =>
                {
                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
