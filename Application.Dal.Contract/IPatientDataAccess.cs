using System;
using Application.Entities.Base;

namespace Application.Dal.Contract
{
    public interface IPatientDataAccess<TPatient, in TPk> where TPatient:Patient
    {
        IEnumerable<TPatient> Get();
        TPatient Get(TPk id);
        TPatient Create(TPatient patient);
        TPatient Update(TPk id, TPatient patient);
        TPatient Delete(TPk id);
    }

}

