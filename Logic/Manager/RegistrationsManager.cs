using Entities.Entities;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml.Linq;

namespace Logic.Manager
{
    public class RegistrationsManager : IRegistrationsManager
    {
        private readonly DbContext context;
        private readonly ILogger<RegistrationsManager> log;
        public RegistrationsManager(DbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.log = loggerFactory.CreateLogger<RegistrationsManager>();
        }

        public Task<PartnerSubject> Create(PartnerSubject model)
        {
            var partnerSubjectExist = context.Set<PartnerSubject>()
                .FirstOrDefault(x => x.StudentId == model.StudentId &&
                x.SubjectId == model.SubjectId);

            if (partnerSubjectExist != null)
            {
                throw new InvalidOperationException("La relación entre el estudiante y la materia ya existe.");
            }

            var partnerSubjects = context.Set<PartnerSubject>()
                .Include(x => x.Subject)
                .Where(x => x.StudentId == model.StudentId)
                .ToList();

            if (partnerSubjects.Count() == 3)
            {
                throw new InvalidOperationException("El estudiante ya tiene 3 materias inscritas.");
            }

            var subjetNew = context.Set<Subject>()
                .FirstOrDefault(x => x.Id == model.SubjectId);

            var subjects = partnerSubjects.Select(x => x.Subject).ToList();

            if (subjects.Any(x => x.TeacherId == subjetNew.TeacherId))
            {
                throw new InvalidOperationException("El estudiante no puede tener dos clases con el mismo profesor.");
            }

            var subject = context.Set<PartnerSubject>().Add(model);
            context.SaveChanges();

            return Task.FromResult(subject.Entity);
        }       
        public async Task<PartnerSubject> ReadById(int Id)
        {
            var subject = context.Set<PartnerSubject>()
                .FirstOrDefault(x => x.Id == Id);

            return subject;
        }
        public async Task<List<PartnerSubject>> Read()
        {
            var result = context.Set<PartnerSubject>()                 
                    .ToList();

            return result;
        }
        public async Task<PartnerSubject> Update(int Id, PartnerSubject model)
        { 
            if (model.Id != Id)
            {
                throw new InvalidOperationException("Modelo Invalido");
            }

            var partnerSubjectExist = context.Set<PartnerSubject>()
            .FirstOrDefault(x => x.StudentId == model.StudentId &&
            x.SubjectId == model.SubjectId && x.Id != model.Id);

            if (partnerSubjectExist != null)
            {
                throw new InvalidOperationException("La relación entre el estudiante y la materia ya existe.");
            }
           
            var subjetNew = context.Set<Subject>()
                .FirstOrDefault(x => x.Id == model.SubjectId);

            var subjects = context.Set<PartnerSubject>()
                .Include(x => x.Subject)
                .Where(x => x.StudentId == model.StudentId)
                .Select(x => x.Subject).ToList();

            if (subjects.Any(x => x.TeacherId == subjetNew.TeacherId))
            {
                throw new InvalidOperationException("El estudiante no puede tener dos clases con el mismo profesor.");
            }       

            PartnerSubject entity2 = context.Set<PartnerSubject>().FirstOrDefault((PartnerSubject x) => x.Id.Equals(model.Id));
            EntityEntry<PartnerSubject> entityEntry = context.Entry(entity2);
            entityEntry.CurrentValues.SetValues(model);
            entityEntry.State = EntityState.Modified;
            context.SaveChanges();

            return model;
        }
        public async Task<bool> Delete(int Id)
        {           
            var val =  context.Set<PartnerSubject>()
                .FirstOrDefault(x => x.Id == Id);

            if (val == null)
            {             
                return false;
            }
            context.Set<PartnerSubject>().Remove(val);
            context.SaveChanges();
            return true;
        }
        public async Task<List<Subject>> GetSubjects()
        {
            var subject = context.Set<Subject>()
                .Include(x => x.StudentsSubject)
                .ThenInclude(x => x.Student)
                .ToList();

            return subject;
        }
    }
}
