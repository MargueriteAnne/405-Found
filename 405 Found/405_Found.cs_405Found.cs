﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 25/05/2024 21:57:17
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace _405Found
{

    public partial class cs_405Found : DbContext
    {

        public cs_405Found() :
            base()
        {
            OnCreated();
        }

        public cs_405Found(DbContextOptions<cs_405Found> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(GetConnectionString("cs_405Found"));
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        private static string GetConnectionString(string connectionStringName)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            var configuration = configurationBuilder.Build();
            return configuration.GetConnectionString(connectionStringName);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<User> Users
        {
            get;
            set;
        }

        public virtual DbSet<Books> Books
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.UserMapping(modelBuilder);
            this.CustomizeUserMapping(modelBuilder);

            this.BooksMapping(modelBuilder);
            this.CustomizeBooksMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

       
        private cs_405Found dbContext;

        #region User Mapping

        private void UserMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(@"Users");
            modelBuilder.Entity<User>().Property(x => x.User_Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<User>().Property(x => x.SavedPassword).HasColumnName(@"SavedPassword").ValueGeneratedNever();
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName(@"Email").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<User>().HasKey(@"User_Id");
        }

        partial void CustomizeUserMapping(ModelBuilder modelBuilder);

        #endregion

        #region Books Mapping

        private void BooksMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().ToTable(@"Books");
            modelBuilder.Entity<Books>().Property(x => x.BookName).HasColumnName(@"BookName").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Books>().Property(x => x.Genre).HasColumnName(@"Genre").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Books>().Property<short>(@"User_Id").HasColumnName(@"User_Id").ValueGeneratedNever();
            modelBuilder.Entity<Books>().Property(x => x.Status).HasColumnName(@"Status").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Books>().HasKey(@"BookName");
        }

        partial void CustomizeBooksMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Books).WithOne(op => op.User).HasForeignKey(@"User_Id").IsRequired(true);

            modelBuilder.Entity<Books>().HasOne(x => x.User).WithMany(op => op.Books).HasForeignKey(@"User_Id").IsRequired(true);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
