using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Supportly.Infrastructure.Models;

public partial class SupportlyContext : DbContext
{
    public SupportlyContext()
    {
    }

    public SupportlyContext(DbContextOptions<SupportlyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TicketComment> TicketComments { get; set; }

    public virtual DbSet<TicketPriority> TicketPriorities { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-RAS4J05;Initial Catalog=Supportly;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pk");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("Ticket_pk");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserAssigned).HasColumnName("user_assigned");
            entity.Property(e => e.UserCreated).HasColumnName("user_created");

            entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("Ticket_TicketCategory");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PriorityId)
                .HasConstraintName("Ticket_Priority");

            entity.HasOne(d => d.Status).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("Ticket_Status");

            entity.HasOne(d => d.UserAssignedNavigation).WithMany(p => p.TicketUserAssignedNavigations)
                .HasForeignKey(d => d.UserAssigned)
                .HasConstraintName("FK_Ticket_User_Assigned");

            entity.HasOne(d => d.UserCreatedNavigation).WithMany(p => p.TicketUserCreatedNavigations)
                .HasForeignKey(d => d.UserCreated)
                .HasConstraintName("FK_Ticket_User_Created");
        });

        modelBuilder.Entity<TicketAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("TicketAttachment_pk");

            entity.ToTable("TicketAttachment");

            entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.UploadedAt)
                .HasColumnType("datetime")
                .HasColumnName("uploaded_at");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketAttachments)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("TicketAttachment_Ticket");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("TicketCategory_pk");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TicketComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("TicketComment_pk");

            entity.ToTable("TicketComment");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Author).WithMany(p => p.TicketComments)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("TicketComment_User");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketComments)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("TicketComment_Ticket");
        });

        modelBuilder.Entity<TicketPriority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("TicketPriority_pk");

            entity.ToTable("TicketPriority");

            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("TicketStatus_pk");

            entity.ToTable("TicketStatus");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
