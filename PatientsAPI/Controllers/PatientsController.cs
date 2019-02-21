using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientsPage.Models;
using PatientsPage.Services;
using Microsoft.AspNetCore.Mvc;

namespace PatientsPage.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientsController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult<List<Patients>> Get()
        {
            return _patientService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetPatient")]
        public ActionResult<Patients> Get(string id)
        {
            var patients = _patientService.Get(id);

            if (patients == null)
            {
                return NotFound();
            }

            return patients;
        }

        [HttpPost]
        public ActionResult<Patients> Create(Patients patients)
        {
            _patientService.Create(patients);

            return CreatedAtRoute("GetPatients", new { id = patients.Id.ToString() }, patients);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Patients patientsIn)
        {
            var patients = _patientService.Get(id);

            if (patients == null)
            {
                return NotFound();
            }

            _patientService.Update(id, patientsIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var patients = _patientService.Get(id);

            if (patients == null)
            {
                return NotFound();
            }

            _patientService.Remove(patients.Id);

            return NoContent();
        }
    }
}