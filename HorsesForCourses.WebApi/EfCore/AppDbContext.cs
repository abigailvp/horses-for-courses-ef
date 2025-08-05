using HorsesForCourses.Core.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.WebApi;

public class AppDbContext : DbContext
{
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<Course> Courses => Set<Course>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enforce unique course titles
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.NameCourse)
            .IsUnique();


        //instellen van relatie tss coach en course: 1 course per coach 
        modelBuilder.Entity<Coach>()
            .HasMany(co => co.ListOfCoursesAssignedTo)
            .WithOne(c => c.CoachForCourse) //uit course halen
            .HasForeignKey(c => c.CoachId); //toegevoegd aan course, ef stelt deze zelf in in domein

        // valueobjects van coach
        modelBuilder.Entity<Coach>()
            .OwnsMany(c => c.ListOfCompetences, a => //a zijn de skills
                {
                    a.WithOwner().HasForeignKey("CoachId");  // optioneel: als EF niet zelf herkent
                    a.Property<int>("Id");// nodig als geen Id in het type zit; hier id maken
                    a.HasKey("Id"); //id instellen als primary key
                    a.Property(a => a.Name).HasColumnName("Name"); //bestaande property benoemen
                    a.ToTable("CoachSkills"); //tabel van maken
                })
            .Property(coach => coach.NameCoach).IsRequired();


        //valueobjects van course
        modelBuilder.Entity<Course>()
            .OwnsMany(c => c.ListOfCourseSkills, rs =>
            {
                rs.WithOwner().HasForeignKey("CourseId");
                rs.Property<int>("Id");
                rs.HasKey("Id");
                rs.Property(rs => rs.Name).HasColumnName("Name");
                rs.ToTable("CourseSkills");
            })
            .OwnsMany(c => c.CourseTimeslots, t =>
            {
                t.WithOwner().HasForeignKey("CourseId");
                t.Property<int>("Id");
                t.HasKey("Id");
                t.Property(t => t.DateTimeslot).HasColumnName("Day");
                t.Property(t => t.BeginTimeslot).HasColumnName("Begin");
                t.Property(t => t.EndTimeslot).HasColumnName("End");
                t.ToTable("CourseTimeslots");
            });

    }

}