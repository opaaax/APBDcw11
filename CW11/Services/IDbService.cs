using CW11.DTOs;
using CW11.Models;

namespace CW11.Services;

public interface IDbService
{
    Task AddPrescription(PrescriptionAddDTO prescription);
    Task<PatientGetDTO> GetPatientData(int id);
}