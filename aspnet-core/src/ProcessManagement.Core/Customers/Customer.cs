using Abp.Domain.Entities.Auditing;
using ProcessManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Customers
{
    [Table("Customers")]
    public class Customer:FullAuditedEntity
    {
        public virtual User User { get; set; }
    }
}
