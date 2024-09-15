﻿// <auto-generated />
using System;
using Larder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Larder.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240915165423_OnCascadeDeleteUnitConversions")]
    partial class OnCascadeDeleteUnitConversions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

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

            modelBuilder.Entity("Larder.Models.ConsumedFood", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("CaloriesConsumed")
                        .HasColumnType("REAL");

                    b.Property<DateOnly>("DateConsumed")
                        .HasColumnType("TEXT");

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("GramsDietaryFiberConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsProteinConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsSaturatedFatConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTotalCarbsConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTotalFatConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTotalSugarsConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTransFatConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("MilligramsCholesterolConsumed")
                        .HasColumnType("REAL");

                    b.Property<double>("MilligramsSodiumConsumed")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("ConsumedFoods");
                });

            modelBuilder.Entity("Larder.Models.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ServingsProduced")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Larder.Models.RecipeIngredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("IngredientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");
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
                });

            modelBuilder.Entity("Larder.Models.UnitConversion", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("TargetUnitId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("TargetUnitsPerUnit")
                        .HasColumnType("REAL");

                    b.Property<string>("UnitId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TargetUnitId");

                    b.HasIndex("UnitId");

                    b.ToTable("UnitConversions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Larder.Models.Food", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.Property<double>("Calories")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsDietaryFiber")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsProtein")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsSaturatedFat")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTotalCarbs")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTotalFat")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTotalSugars")
                        .HasColumnType("REAL");

                    b.Property<double>("GramsTransFat")
                        .HasColumnType("REAL");

                    b.Property<double>("MilligramsCholesterol")
                        .HasColumnType("REAL");

                    b.Property<double>("MilligramsSodium")
                        .HasColumnType("REAL");

                    b.Property<double>("Servings")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("Food");
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.HasDiscriminator().HasValue("Ingredient");
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

                    b.OwnsOne("Larder.Models.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<string>("RecipeIngredientId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("UnitId")
                                .HasColumnType("TEXT");

                            b1.HasKey("RecipeIngredientId");

                            b1.HasIndex("UnitId");

                            b1.ToTable("RecipeIngredients");

                            b1.WithOwner()
                                .HasForeignKey("RecipeIngredientId");

                            b1.HasOne("Larder.Models.Unit", "Unit")
                                .WithMany()
                                .HasForeignKey("UnitId");

                            b1.Navigation("Unit");
                        });

                    b.Navigation("Ingredient");

                    b.Navigation("Quantity")
                        .IsRequired();

                    b.Navigation("Recipe");
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

            modelBuilder.Entity("Larder.Models.UnitConversion", b =>
                {
                    b.HasOne("Larder.Models.Unit", "TargetUnit")
                        .WithMany("TargetConversions")
                        .HasForeignKey("TargetUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.Unit", "Unit")
                        .WithMany("Conversions")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetUnit");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Larder.Models.Food", b =>
                {
                    b.OwnsOne("Larder.Models.Quantity", "ServingSize", b1 =>
                        {
                            b1.Property<string>("FoodId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("UnitId")
                                .HasColumnType("TEXT");

                            b1.HasKey("FoodId");

                            b1.HasIndex("UnitId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("FoodId");

                            b1.HasOne("Larder.Models.Unit", "Unit")
                                .WithMany()
                                .HasForeignKey("UnitId");

                            b1.Navigation("Unit");
                        });

                    b.Navigation("ServingSize")
                        .IsRequired();
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.OwnsOne("Larder.Models.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<string>("IngredientId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("UnitId")
                                .HasColumnType("TEXT");

                            b1.HasKey("IngredientId");

                            b1.HasIndex("UnitId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("IngredientId");

                            b1.HasOne("Larder.Models.Unit", "Unit")
                                .WithMany()
                                .HasForeignKey("UnitId");

                            b1.Navigation("Unit");
                        });

                    b.Navigation("Quantity")
                        .IsRequired();
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.Navigation("RecipeIngredients");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Larder.Models.Unit", b =>
                {
                    b.Navigation("Conversions");

                    b.Navigation("TargetConversions");
                });

            modelBuilder.Entity("Larder.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
