﻿using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
  public DbSet<Post> Posts => Set<Post>();
  public DbSet<User> Users => Set<User>();
  public DbSet<Comment> Comments => Set<Comment>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=C:\\Users\\Alexa\\OneDrive\\Dokumenter\\GitHub\\DNPAssignment\\.idea\\DNPAssignment\\Server\\EfcRepositories\\app.db");
  }
}