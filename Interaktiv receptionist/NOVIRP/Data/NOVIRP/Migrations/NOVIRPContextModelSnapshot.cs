﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOVIRP.Models;

namespace NOVIRP.Migrations
{
    [DbContext(typeof(NOVIRPContext))]
    partial class NOVIRPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NOVIRP.Models.NOVI", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CVR");

                    b.Property<string>("Findvej");

                    b.Property<string>("Lokale");

                    b.Property<int>("Medarbejdere");

                    b.Property<string>("Navn");

                    b.HasKey("ID");

                    b.ToTable("NOVI");
                });
#pragma warning restore 612, 618
        }
    }
}