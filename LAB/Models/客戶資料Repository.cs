using System;
using System.Linq;
using System.Collections.Generic;

namespace LAB.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public 客戶資料 Find(int? id)
        {
            return id.HasValue ? All().FirstOrDefault(c => c.Id == id) : null;
        }

        public override IQueryable<客戶資料> All()
        {
            return this.All(includeDeleted: false);
        }

        public IQueryable<客戶資料> All(bool includeDeleted = false)
        {
            return includeDeleted
                ? base.All()
                : base.All().Where(c => !c.IsDeleted);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.IsDeleted = true;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {
        客戶資料 Find(int? id);

    }
}