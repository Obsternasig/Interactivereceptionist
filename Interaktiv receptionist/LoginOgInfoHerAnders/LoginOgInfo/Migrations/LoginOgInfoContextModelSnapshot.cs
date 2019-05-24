﻿// <auto-generated />
using LoginOgInfo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginOgInfo.Migrations
{
    [DbContext(typeof(LoginOgInfoContext))]
    partial class LoginOgInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoginOgInfo.Lokaler.LokalerInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Lejer");

                    b.Property<string>("Lokalenummer");

                    b.Property<string>("Medarbejderantal");

                    b.HasKey("ID");

                    b.ToTable("LokalerInfo");
                });

            modelBuilder.Entity("LoginOgInfo.Medarbejdere.MedarbejderInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Lokale");

                    b.Property<string>("Navn");

                    b.Property<string>("Stilling");

                    b.Property<string>("Virksomhed");

                    b.HasKey("ID");

                    b.ToTable("MedarbejderInfo");
                });

            modelBuilder.Entity("LoginOgInfo.Virksomheder.VirksomhederInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CVR");

                    b.Property<string>("Findvej");

                    b.Property<string>("Lokale");

                    b.Property<string>("Medarbejdere");

                    b.Property<string>("Navn");

                    b.HasKey("ID");

                    b.ToTable("VirksomhederInfo");
                });
#pragma warning restore 612, 618
        }
    }
}