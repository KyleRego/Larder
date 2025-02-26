﻿// <auto-generated />
using System;
using Larder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Larder.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ItemRecipe", b =>
                {
                    b.Property<string>("IngredientsId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipesId")
                        .HasColumnType("TEXT");

                    b.HasKey("IngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("ItemRecipe");
                });

            modelBuilder.Entity("Larder.Models.ApplicationUser", b =>
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

            modelBuilder.Entity("Larder.Models.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Larder.Models.ItemComponents.ConsumedTime", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ConsumedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("ConsumedTime");
                });

            modelBuilder.Entity("Larder.Models.ItemComponents.Nutrition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

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

                    b.Property<string>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<double>("MilligramsCholesterol")
                        .HasColumnType("REAL");

                    b.Property<double>("MilligramsSodium")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Larder.Models.RecipeIngredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

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

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

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

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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

                    b.Property<int?>("UnitType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TargetUnitId");

                    b.HasIndex("UnitId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("Larder.Models.Utensil", b =>
                {
                    b.HasBaseType("Larder.Models.Item");

                    b.HasDiscriminator().HasValue("Utensil");
                });

            modelBuilder.Entity("ItemRecipe", b =>
                {
                    b.HasOne("Larder.Models.Item", null)
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
                    b.HasOne("Larder.Models.ApplicationUser", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Larder.Models.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<string>("ItemId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("UnitId")
                                .HasColumnType("TEXT");

                            b1.HasKey("ItemId");

                            b1.HasIndex("UnitId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");

                            b1.HasOne("Larder.Models.Unit", "Unit")
                                .WithMany()
                                .HasForeignKey("UnitId");

                            b1.Navigation("Unit");
                        });

                    b.Navigation("Quantity")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Larder.Models.ItemComponents.ConsumedTime", b =>
                {
                    b.HasOne("Larder.Models.Item", "Item")
                        .WithOne("ConsumedTime")
                        .HasForeignKey("Larder.Models.ItemComponents.ConsumedTime", "ItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Larder.Models.ItemComponents.Nutrition", b =>
                {
                    b.HasOne("Larder.Models.Item", "Item")
                        .WithOne("Nutrition")
                        .HasForeignKey("Larder.Models.ItemComponents.Nutrition", "ItemId");

                    b.OwnsOne("Larder.Models.Quantity", "ServingSize", b1 =>
                        {
                            b1.Property<string>("NutritionId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("UnitId")
                                .HasColumnType("TEXT");

                            b1.HasKey("NutritionId");

                            b1.HasIndex("UnitId");

                            b1.ToTable("Foods");

                            b1.WithOwner()
                                .HasForeignKey("NutritionId");

                            b1.HasOne("Larder.Models.Unit", "Unit")
                                .WithMany()
                                .HasForeignKey("UnitId");

                            b1.Navigation("Unit");
                        });

                    b.Navigation("Item");

                    b.Navigation("ServingSize")
                        .IsRequired();
                });

            modelBuilder.Entity("Larder.Models.Recipe", b =>
                {
                    b.HasOne("Larder.Models.ApplicationUser", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Larder.Models.RecipeIngredient", b =>
                {
                    b.HasOne("Larder.Models.Item", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Larder.Models.Quantity", "DefaultQuantity", b1 =>
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

                    b.Navigation("DefaultQuantity")
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Larder.Models.RecipeStep", b =>
                {
                    b.HasOne("Larder.Models.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Larder.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Larder.Models.Unit", b =>
                {
                    b.HasOne("Larder.Models.ApplicationUser", "User")
                        .WithMany("Units")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

                    b.HasOne("Larder.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetUnit");

                    b.Navigation("Unit");

                    b.Navigation("User");
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
                    b.HasOne("Larder.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Larder.Models.ApplicationUser", null)
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

                    b.HasOne("Larder.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Larder.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Larder.Models.ApplicationUser", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Recipes");

                    b.Navigation("Units");
                });

            modelBuilder.Entity("Larder.Models.Item", b =>
                {
                    b.Navigation("ConsumedTime");

                    b.Navigation("Nutrition");

                    b.Navigation("RecipeIngredients");
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
#pragma warning restore 612, 618
        }
    }
}
