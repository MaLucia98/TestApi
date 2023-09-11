using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Manager.Interfaces
{
    public interface IPartnerManager
    {
        Task<Partner> Create(Partner model);
        Task<Partner> ReadById(int Id);
        Task<List<Partner>> Read();
        Task<Partner> Update(int Id, Partner model);
        Task<bool> Delete(int Id);
        Task<List<PartnerSubject>> GetStudetsBySubject(int subjectId);
        Task<List<Partner>> GetStudets();
        Task<List<PartnerSubject>> GetSubjectsByPartner(int partnerId);
    }
}
