using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Manager.Interfaces
{
    public interface IRegistrationsManager
    {
        Task<PartnerSubject> Create(PartnerSubject model);
        Task<PartnerSubject> ReadById(int Id);
        Task<List<PartnerSubject>> Read();
        Task<PartnerSubject> Update(int Id, PartnerSubject model);
        Task<bool> Delete(int Id);
        Task<List<Subject>> GetSubjects();
    }
}
