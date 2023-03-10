// <auto-generated />
using BlazorEcommerce.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20221230111140_Mig_AddCategory")]
    partial class Mig_AddCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlazorEcommerce.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Books",
                            Url = "books"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Movies",
                            Url = "movies"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Video Games",
                            Url = "video-games"
                        });
                });

            modelBuilder.Entity("BlazorEcommerce.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Description 1",
                            ImageUrl = "https://dkstatics-public.digikala.com/digikala-products/50c904d782011abc8cb712abac1d51df2b7a2297_1671299236.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80",
                            Price = 9.99m,
                            Title = "Product 1"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Description 2",
                            ImageUrl = "https://dkstatics-public.digikala.com/digikala-products/c849507e3a7592932236c0d0a580d913339d1fae_1661594462.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80",
                            Price = 8.99m,
                            Title = "Product 2"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Description 3",
                            ImageUrl = "https://dkstatics-public.digikala.com/digikala-products/99fff58e2393fee11af1635106176ebc34790419_1644844789.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80",
                            Price = 7.99m,
                            Title = "Product 3"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Description 4",
                            ImageUrl = "https://dkstatics-public.digikala.com/digikala-products/87b6c4f18f15c5c7878172615ad29e90da16e098_1652698789.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80",
                            Price = 6.99m,
                            Title = "Product 4"
                        });
                });

            modelBuilder.Entity("BlazorEcommerce.Shared.Product", b =>
                {
                    b.HasOne("BlazorEcommerce.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
