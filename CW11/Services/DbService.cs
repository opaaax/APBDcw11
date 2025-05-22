using CW11.Data;
using CW11.DTOs;
using CW11.Models;
using Microsoft.AspNetCore.Components.Sections;
using Microsoft.EntityFrameworkCore;

namespace CW11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task AddPrescription(PrescriptionAddDTO prescription)
    {
        Console.WriteLine(prescription.Date);
        Console.WriteLine(prescription.Doctor.FirstName);
        Console.WriteLine(prescription.Patient.FirstName);
        Console.WriteLine(prescription.Medicaments.Count);
        
        if (prescription.Medicaments == null || !prescription.Medicaments.Any())
            throw new Exception("Prescription must contain at least one medicament");

        
        foreach (var medicament in prescription.Medicaments)
        {
            if (await _context.Medicaments.FindAsync(medicament.IdMedicament) is null)
            {
                throw new Exception("Medicament not found");
            }    
        }
        
        if (await _context.Doctors.FindAsync(prescription.Doctor.IdDoctor) is null)
        {
            throw new Exception("Doctor not found");
        }
        
        if (prescription.Medicaments.Count > 10)
        {
            throw new Exception("Too many medicaments");
        }
        
        if (await _context.Patients.FindAsync(prescription.Patient.IdPatient) is null)
        {
            await _context.Patients.AddAsync(new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.Birthdate,
            });
            await _context.SaveChangesAsync();
        }
        
        
        if (prescription.DueDate < DateTime.Now)
        {
            throw new Exception("Due date can't be in the past");
        }

        Prescription addedPrescription = new Prescription()
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = prescription.Doctor.IdDoctor,
            IdPatient = prescription.Patient.IdPatient,
        };

        await _context.Prescriptions.AddAsync(addedPrescription);
        await _context.SaveChangesAsync();

        foreach (MedicamentDTO medicament in prescription.Medicaments)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {

                IdMedicament = medicament.IdMedicament,
                IdPrescription = addedPrescription.IdPrescription,
                Dose = medicament.Dose,
                Details = medicament.Description,
            });
        }
        await _context.SaveChangesAsync();
    }

    public async Task<PatientGetDTO> GetPatientData(int id)
    {
        var patient = await _context.Patients.Where(e => e.IdPatient.Equals(id)).Select(e => new PatientGetDTO()
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.Birthdate,
            Prescriptions = e.Prescriptions.Select(p => 
                new PrescriptionGetDTO()
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Medicaments = p.PrescriptionMedicaments.Select(m => new MedicamentGetDTO()
                    {
                        IdMedicament = m.IdMedicament,
                        Name = m.Medicament.Name,
                        Description = m.Medicament.Description,
                        Type = m.Medicament.Type
                    }).ToList(),
                    Doctor = new DoctorGetDTO()
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    }
                }).ToList()
        }).FirstOrDefaultAsync();
        return patient;
    }
}