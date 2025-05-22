using CW11.Models;
using Microsoft.EntityFrameworkCore;

namespace CW11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() {IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com"},
            new Doctor() {IdDoctor = 2, FirstName = "John2", LastName = "Doe2", Email = "john@doe.com2"},
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient(){IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = DateTime.Parse("1990-01-01")},
            new Patient(){IdPatient = 2, FirstName = "Jan2", LastName = "Kowalski2", Birthdate = DateTime.Parse("1995-01-01")}
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() {IdMedicament = 1, Name = "Medicament 1", Description = "Description 1", Type="Medicament 1"},
            new Medicament() {IdMedicament = 2, Name = "Medicament 2", Description = "Description 2", Type="Medicament 2"},
        });
    }
    
}