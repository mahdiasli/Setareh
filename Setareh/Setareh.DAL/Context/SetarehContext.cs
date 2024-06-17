﻿
using Microsoft.EntityFrameworkCore;
using Setareh.DAL.Entities.ContacUs;
using Setareh.DAL.Entities.User;

namespace Setareh.DAL.Context
{
    public class SetarehContext:DbContext
    {
        #region Constractor

        public SetarehContext(DbContextOptions<SetarehContext> options):base(options)
        {
            
        }

        #endregion


        #region DbSets

        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<User> User { get; set; }

        

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    CreateDate = DateTime.Now,
                    Email = "mahdiamiridev@gmail.com",
                    FirstName = "مهدی",
                    LastName = "امیری",
                    Id = 1,
                    IsActive = true,
                    Mobile = "09337132998",
                    Password = "2C-21-6B-1B-A5-E3-3A-27-EB-6D-3D-F7-DE-7F-8C-36"
                });

            modelBuilder.Entity<ContactUs>()
                .HasData(new ContactUs
                {
                    Answer = "ندارد",
                    CreateDate = DateTime.Now,
                    Description = "سلام عالی",
                    Email = "mahdiamiridev@gmail.com",
                    FirstName = "مهدی",
                    Id = 1,
                    LastName = "امیری",
                    Mobile = "09337132998",
                    Title = "تست"
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
