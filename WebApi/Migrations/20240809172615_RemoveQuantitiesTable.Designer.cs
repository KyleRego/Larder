﻿// <auto-generated />
using Larder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Larder.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240809172615_RemoveQuantitiesTable")]
    partial class RemoveQuantitiesTable
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

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UnitId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

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
                            Id = "b9a6b978-946f-4560-bfba-76ce28a1ecd3",
                            FoodId = "bb55f800-f695-4976-b4f1-2222ed015313",
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
                            Id = "231f0f9c-806a-468e-82e0-874a0c0fcdf8",
                            Amount = 1.0,
                            IngredientId = "f2e273e5-0dfe-4245-bd41-2045041d3122",
                            RecipeId = "b9a6b978-946f-4560-bfba-76ce28a1ecd3",
                            UnitId = "417b4271-3fa1-4523-a6cb-0d50710880fd"
                        },
                        new
                        {
                            Id = "23458413-1d06-403c-a08b-942a73884be4",
                            Amount = 2.5,
                            IngredientId = "e171b57d-b905-410b-bee1-f003f7a06e94",
                            RecipeId = "b9a6b978-946f-4560-bfba-76ce28a1ecd3",
                            UnitId = "3e054aa5-5e8d-48b1-9609-ce2d97ced7d8"
                        },
                        new
                        {
                            Id = "339ad74a-c6fd-4adf-870e-d870e5cc14eb",
                            Amount = 1.0,
                            IngredientId = "40c2e018-30be-4d1c-a181-79b32020e086",
                            RecipeId = "b9a6b978-946f-4560-bfba-76ce28a1ecd3"
                        });
                });

            modelBuilder.Entity("Larder.Models.RecipeStep", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
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
                            Id = "b328fab5-a585-41e0-bd2b-ad3ef71acb2a",
                            Name = "Liters",
                            Type = 1
                        },
                        new
                        {
                            Id = "0d81898d-9b13-405c-93e1-9cf761320593",
                            Name = "Pounds",
                            Type = 2
                        },
                        new
                        {
                            Id = "b4ee34a0-0384-49e6-b320-606a042ee0d7",
                            Name = "Grams",
                            Type = 0
                        },
                        new
                        {
                            Id = "05976d10-937f-4ae3-a703-838fcfa40e05",
                            Name = "Milliliters",
                            Type = 1
                        },
                        new
                        {
                            Id = "417b4271-3fa1-4523-a6cb-0d50710880fd",
                            Name = "Tablespoons",
                            Type = 1
                        },
                        new
                        {
                            Id = "3e054aa5-5e8d-48b1-9609-ce2d97ced7d8",
                            Name = "Cups",
                            Type = 1
                        });
                });

            modelBuilder.Entity("Larder.Models.Food", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.Property<double>("Calories")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("Food");

                    b.HasData(
                        new
                        {
                            Id = "bb55f800-f695-4976-b4f1-2222ed015313",
                            Amount = 0.0,
                            Name = "Chicken and rice",
                            Calories = 0.0
                        });
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.HasDiscriminator().HasValue("Ingredient");

                    b.HasData(
                        new
                        {
                            Id = "f2e273e5-0dfe-4245-bd41-2045041d3122",
                            Amount = 0.0,
                            Name = "Butter"
                        },
                        new
                        {
                            Id = "e171b57d-b905-410b-bee1-f003f7a06e94",
                            Amount = 0.0,
                            Name = "Water"
                        },
                        new
                        {
                            Id = "40c2e018-30be-4d1c-a181-79b32020e086",
                            Amount = 0.0,
                            Name = "Rice Roni Chicken Lower Sodium box"
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

            modelBuilder.Entity("Larder.Models.Item", b =>
                {
                    b.HasOne("Larder.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
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

            modelBuilder.Entity("Larder.Models.RecipeStep", b =>
                {
                    b.HasOne("Larder.Models.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.Navigation("RecipeIngredients");

                    b.Navigation("Steps");
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
