using Entities.Entities;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace Logic.Manager
{
    public class PartnerManager : IPartnerManager
    {
        private readonly DbContext context;
        private readonly ILogger<PartnerManager> log;
        public PartnerManager(DbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.log = loggerFactory.CreateLogger<PartnerManager>();
        }
        public async Task<Partner> Create(Partner model)
        {
            var subject = context.Set<Partner>().Add(model);
            context.SaveChanges();
            return subject.Entity;
        }          
        public async Task<Partner> ReadById(int Id)
        {
            var subject = context.Set<Partner>()
                .AsNoTracking()
                .Include(x => x.PartnerSubject)
                .ThenInclude(x => x.Subject)
                .FirstOrDefault(x => x.Id == Id);

            return subject;
        }
        public async Task<List<Partner>> Read()
        {
            var result = context.Set<Partner>()               
                  .ToList();

            return result;
        }
        public async Task<Partner> Update(int Id, Partner model)
        { 
            if (model.Id != Id)
            {
                return null;
            }

            Partner entity2 = context.Set<Partner>().FirstOrDefault((Partner x) => x.Id.Equals(model.Id));
            EntityEntry<Partner> entityEntry = context.Entry(entity2);
            entityEntry.CurrentValues.SetValues(model);
            entityEntry.State = EntityState.Modified;
            context.SaveChanges();

            return model;
        }
        public async Task<bool> Delete(int Id)
        {           
            var val =  context.Set<Partner>()
                .FirstOrDefault(x => x.Id == Id);

            if (val == null)
            {             
                return false;
            }

            context.Set<Partner>().Remove(val);
            context.SaveChanges();
            return true;
        }       
        public async Task<List<PartnerSubject>> GetStudetsBySubject(int subjectId)
        {
            var result = context.Set<PartnerSubject>()
                   .AsNoTracking()
                  .Include(x => x.Student)
                  .Where(x => x.SubjectId == subjectId)
                  .ToList();

            return result;
        }
        public async Task<List<PartnerSubject>> GetSubjectsByPartner(int partnerId)
        {
            var result = context.Set<PartnerSubject>()
                  .AsNoTracking()
                  .Include(x => x.Subject)
                  .Where(x => x.StudentId == partnerId)
                  .ToList();

            return result;
        }
        public async Task<List<Partner>> GetStudets()
        {
            var result = context.Set<Partner>()
                  .Where(x => x.PartnerTypeId == (int)PartnerTypeEnum.Estudiante)
                  .ToList();

            return result;
        }
        
    }
}
