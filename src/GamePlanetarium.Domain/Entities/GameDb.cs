using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Entities.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GamePlanetarium.Domain.Entities;

public class GameDb : DbContext
{
    public DbSet<AnswerEntity> Answers => Set<AnswerEntity>();
    public DbSet<QuestionImageEntity> QuestionImages => Set<QuestionImageEntity>();
    public DbSet<QuestionEntity> Questions => Set<QuestionEntity>();
    public DbSet<QuestionStatisticsDataEntity> QuestionStatistics => Set<QuestionStatisticsDataEntity>();
    public DbSet<GameStatisticsDataEntity> GameStatistics => Set<GameStatisticsDataEntity>();

    private readonly string? _connectionString;

    public GameDb(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        _connectionString = connectionString;
    }
    public GameDb(DbContextOptions<GameDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<QuestionImageEntity>()
        //             .Ignore(p => p.Question);
        // modelBuilder.Entity<QuestionEntity>()
        //             .Ignore(p => p.QuestionImage);
        modelBuilder.Entity<GameStatisticsDataEntity>()
                    .Property(e => e.DateStamp)
                    .HasConversion(
                        // Convert DateOnly to DateTime when saving to the database.
                        v => v.ToDateTime(TimeOnly.Parse("00:00 AM")),
                        v => DateOnly.FromDateTime(v));
        modelBuilder.Entity<QuestionEntity>()
                    .HasOne(q => q.QuestionImage).WithOne(qi => qi.Question)
                    .HasForeignKey<QuestionImageEntity>(qi => qi.Id);
        modelBuilder.Entity<QuestionStatisticsDataEntity>()
                    .HasOne(q => q.Game).WithMany(g => g.QuestionsStatistics);
        
        base.OnModelCreating(modelBuilder);
        //Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!_connectionString.IsNullOrEmpty())
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        optionsBuilder.LogTo(Console.WriteLine);
    }
}