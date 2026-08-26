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

    public virtual DbSet<MOrganization> MOrganizations { get; set; }

    public virtual DbSet<MPermission> MPermissions { get; set; }

    public virtual DbSet<MProject> MProjects { get; set; }

    public virtual DbSet<MRoleHasPermission> MRoleHasPermissions { get; set; }

    public virtual DbSet<MTeam> MTeams { get; set; }

    public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHasRole> UserHasRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("m_organization_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.MOrganizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_m_organization_owner");
        });

        modelBuilder.Entity<MPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_m_permission");
        });

        modelBuilder.Entity<MProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("m_project_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Org).WithMany(p => p.MProjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_m_project_organization");
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

        modelBuilder.Entity<MTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("m_team_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Org).WithMany(p => p.MTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_m_team_organization");

            entity.HasOne(d => d.TeamLead).WithMany(p => p.MTeams).HasConstraintName("fk_m_team_lead");
        });

        modelBuilder.Entity<OrganizationMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_members_pkey");

            entity.Property(e => e.IsActive).HasDefaultValue((short)1);
            entity.Property(e => e.JoinedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Role).HasDefaultValueSql("'MEMBER'::character varying");

            entity.HasOne(d => d.Org).WithMany(p => p.OrganizationMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_organization_members_org");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_organization_members_user");
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("project_members_pkey");

            entity.Property(e => e.IsActive).HasDefaultValue((short)1);
            entity.Property(e => e.JoinedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Role).HasDefaultValueSql("'MEMBER'::character varying");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_members_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_roles");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_member_pkey");

            entity.Property(e => e.IsActive).HasDefaultValue((short)1);
            entity.Property(e => e.JoinedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_team_member_team");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_team_member_user");
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
