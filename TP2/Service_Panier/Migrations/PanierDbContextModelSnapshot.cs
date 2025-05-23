﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service_Panier;

#nullable disable

namespace Service_Panier.Migrations
{
    [DbContext(typeof(PanierDbContext))]
    partial class PanierDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Service_Panier.Models.ItemPanier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("panierId")
                        .HasColumnType("int");

                    b.Property<int>("produitId")
                        .HasColumnType("int");

                    b.Property<int>("quantite")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("panierId");

                    b.ToTable("ItemPaniers", (string)null);
                });

            modelBuilder.Entity("Service_Panier.Models.Panier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Panier", (string)null);
                });

            modelBuilder.Entity("Service_Panier.Models.ItemPanier", b =>
                {
                    b.HasOne("Service_Panier.Models.Panier", "panier")
                        .WithMany("ItemsPanier")
                        .HasForeignKey("panierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("panier");
                });

            modelBuilder.Entity("Service_Panier.Models.Panier", b =>
                {
                    b.Navigation("ItemsPanier");
                });
#pragma warning restore 612, 618
        }
    }
}
