using System;
using System.Linq;
using System.Collections.Generic;

namespace LAB.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public 客戶聯絡人 Find(int? id)
        {
            return id.HasValue ? All().FirstOrDefault(c => c.Id == id) : null;
        }

        public override IQueryable<客戶聯絡人> All()
        {
            return this.All(includeDeleted: false);
        }

        public IQueryable<客戶聯絡人> All(bool includeDeleted = false)
        {
            return includeDeleted
                ? base.All()
                : base.All().Where(c => !c.IsDeleted);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDeleted = true;
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {
        客戶聯絡人 Find(int? id);

    }
}