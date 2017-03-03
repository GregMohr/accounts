using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using accounts.Models;

namespace accounts.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("accounts.Models.Account", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("balance");

                    b.Property<DateTime>("created_at");

                    b.Property<string>("type");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("accounts.Models.Transaction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("accountId");

                    b.Property<decimal>("amount");

                    b.Property<DateTime>("created_at");

                    b.Property<DateTime>("updated_at");

                    b.HasKey("id");

                    b.HasIndex("accountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("accounts.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("first")
                        .IsRequired();

                    b.Property<string>("last")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.Property<DateTime>("updated_at");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("accounts.Models.Account", b =>
                {
                    b.HasOne("accounts.Models.User")
                        .WithMany("accounts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("accounts.Models.Transaction", b =>
                {
                    b.HasOne("accounts.Models.Account")
                        .WithMany("transactions")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
