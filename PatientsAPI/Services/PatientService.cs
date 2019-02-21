using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientsPage.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace PatientsPage.Services
{
    public class PatientService
    {
        private readonly IMongoCollection<Patients> _patients;

        public PatientService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PatientsPage"));
            var database = client.GetDatabase("PatientsPage");
            _patients = database.GetCollection<Patients>("PatientsPage");
        }

        public List<Patients> Get()
        {
            return _patients.Find(patients => true).ToList();
        }

        public Patients Get(string id)
        {
            return _patients.Find<Patients>(patients => patients.Id == id).FirstOrDefault();
        }

        public Patients Create(Patients patients)
        {
            _patients.InsertOne(patients);
            return patients;
        }

        public void Update(string id, Patients patientsIn)
        {
            _patients.ReplaceOne(patients => patients.Id == id, patientsIn);
        }

        public void Remove(Patients patientsIn)
        {
            _patients.DeleteOne(patients => patients.Id == patientsIn.Id);
        }

        public void Remove(string id)
        {
            _patients.DeleteOne(patients => patients.Id == id);
        }
    }
}