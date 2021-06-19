using HM.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Context
{
    public class HMContext : DbContext
    {
        public HMContext(DbContextOptions<HMContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public virtual DbSet<AppAccount> AppAccounts { get; set; }
        public virtual DbSet<AppAccountDetail> AppAccountDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<Hostel> Hostels { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomFee> RoomFees { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomFee>().HasKey(c => new { c.RoomId, c.FeeId });
        }
    }
}
