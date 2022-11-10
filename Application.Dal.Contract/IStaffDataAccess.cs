using System;
using Application.Entities.Base;

namespace Application.Dal.Contract
{
    public interface IStaffDataAccess<TStaff, in TPk> where TStaff : Staff
    {
        IEnumerable<TStaff> Get();
        TStaff Get(TPk id);
        TStaff Create(TStaff staff);
        TStaff Update(TPk id, TStaff staff);
        TStaff Delete(TPk id);
    }
}

