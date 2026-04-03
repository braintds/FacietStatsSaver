using System;
using System.Collections.Generic;
using FacietStatsSaver.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FacietStatsSaver.Controller;

public partial class ApplicationDbContext : DbContext
{
    readonly string _connectionString;
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public ApplicationDbContext( string connectionString) => _connectionString = connectionString;
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<GameSession> Gamesessions { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("account_pkey");

            entity.ToTable("account");

            entity.HasIndex(e => e.Accountname, "account_accountname_key").IsUnique();

            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Accountname)
                .HasMaxLength(100)
                .HasColumnName("accountname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.HasKey(e => e.Sessionid).HasName("gamesession_pkey");

            entity.ToTable("gamesession");

            entity.HasIndex(e => e.Accountid, "idx_session_account");

            entity.Property(e => e.Sessionid).HasColumnName("sessionid");
            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Intervalenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("intervalenddate");
            entity.Property(e => e.Intervalstartdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("intervalstartdate");
            entity.Property(e => e.Sessionenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sessionenddate");
            entity.Property(e => e.Sessionstartdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sessionstartdate");

            entity.HasOne(d => d.Account).WithMany(p => p.Gamesessions)
                .HasForeignKey(d => d.Accountid)
                .HasConstraintName("fk_session_account");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Matchid).HasName("match_pkey");

            entity.ToTable("match");

            entity.HasIndex(e => e.Playedat, "idx_match_playedat");

            entity.HasIndex(e => e.Sessionid, "idx_match_session");

            entity.Property(e => e.Matchid).HasColumnName("matchid");
            entity.Property(e => e.Assists).HasColumnName("assists");
            entity.Property(e => e.Deaths).HasColumnName("deaths");
            entity.Property(e => e.Doublekills).HasColumnName("doublekills");
            entity.Property(e => e.Iswin).HasColumnName("iswin");
            entity.Property(e => e.Kills).HasColumnName("kills");
            entity.Property(e => e.Mapname)
                .HasMaxLength(100)
                .HasColumnName("mapname");
            entity.Property(e => e.Mvps).HasColumnName("mvps");
            entity.Property(e => e.Playedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("playedat");
            entity.Property(e => e.Quadrokills).HasColumnName("quadrokills");
            entity.Property(e => e.Sessionid).HasColumnName("sessionid");
            entity.Property(e => e.Triplekills).HasColumnName("triplekills");

            entity.HasOne(d => d.Session).WithMany(p => p.Matches)
                .HasForeignKey(d => d.Sessionid)
                .HasConstraintName("fk_match_session");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
/*
   var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test;ConnectRetryCount=0")
    .Options;

using var context = new ApplicationDbContext(contextOptions);
*/