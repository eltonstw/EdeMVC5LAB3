using System;
using System.Linq;
using System.Collections.Generic;

namespace LAB.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public 客戶銀行資訊 Find(int? id)
        {
            return id.HasValue ? All().FirstOrDefault(c => c.Id == id) : null;
        }

        public override IQueryable<客戶銀行資訊> All()
        {
            return this.All(includeDeleted: false);
        }

        public IQueryable<客戶銀行資訊> All(bool includeDeleted = false)
        {
            return includeDeleted
                ? base.All()
                : base.All().Where(c => !c.IsDeleted);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.IsDeleted = true;
        }
    }

    public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {
        客戶銀行資訊 Find(int? id);

    }
}