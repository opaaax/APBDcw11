using System.ComponentModel.DataAnnotations;
using CW11.Models;

namespace CW11.DTOs;

public class PrescriptionAddDTO
{
    public PatientDTO Patient { get; set; }
    public DoctorDTO Doctor { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class PatientDTO
{
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}

public class DoctorDTO
{
    public int IdDoctor { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
}

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}
