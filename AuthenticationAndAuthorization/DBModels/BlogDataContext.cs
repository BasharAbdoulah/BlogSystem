using System;
using System.Collections.Generic;
using BlogSystem.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem;

public partial class BlogDataContext : DbContext
{
    public BlogDataContext()
    {
    }

    public BlogDataContext(DbContextOptions<BlogDataContext> options)
        : base(options)
    {
    }

        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(p => p.PostId);

        modelBuilder.Entity<Like>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Like>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(p => p.PostId);


    }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Like> likes { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
