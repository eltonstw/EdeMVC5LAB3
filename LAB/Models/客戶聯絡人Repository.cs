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
            return this.All(jobTitle:"", includeDeleted: false);
        }

        public IQueryable<客戶聯絡人> All(string jobTitle = "", bool includeDeleted = false)
        {
            if (string.IsNullOrEmpty(jobTitle))
            {
                return includeDeleted
                    ? base.All()
                    : base.All().Where(c => !c.IsDeleted);
            }
            else
            {
                return
                    base.Where(c => string.IsNullOrEmpty(jobTitle) || c.職稱 == jobTitle)
                        .Where(c => !includeDeleted || !c.IsDeleted);
            }
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