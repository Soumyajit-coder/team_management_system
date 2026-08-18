using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;

namespace team_management_system.DAL;

public partial class teamManagementSystemDBContext : DbContext
{
    public teamManagementSystemDBContext()
    {
    }

    public teamManagementSystemDBContext(DbContextOptions<teamManagementSystemDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MPermission> MPermissions { get; set; }

    public virtual DbSet<MRoleHasPermission> MRoleHasPermissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHasRole> UserHasRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_m_permission");
        });

        modelBuilder.Entity<MRoleHasPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("m_role_has_permission_id_pk");

            entity.HasOne(d => d.Permission).WithMany(p => p.MRoleHasPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_m_role_has_permission_permission_id");

            entity.HasOne(d => d.Role).WithMany(p => p.MRoleHasPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_m_role_has_permission_role_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_roles");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_users_id");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MobileNo).IsFixedLength();
        });

        modelBuilder.Entity<UserHasRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_id_pk");

            entity.HasOne(d => d.Role).WithMany(p => p.UserHasRoles).HasConstraintName("fk_user_has_role_role_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserHasRoles).HasConstraintName("fk_user_has_role_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
