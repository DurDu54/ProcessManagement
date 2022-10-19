using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Professions
{
    [Table("Professions")]
    public class Profession: FullAuditedEntity
    {
        public string Text { get; set; }
    }
}
