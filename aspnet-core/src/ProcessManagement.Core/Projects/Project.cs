using Abp.Domain.Entities.Auditing;
using ProcessManagement.Customers;
using ProcessManagement.Developers;
using ProcessManagement.Enums;
using ProcessManagement.Managers;
using ProcessManagement.Missions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Projects
{
    [Table("Projects")]
    public class Project : FullAuditedEntity
    {
        public Project()
        {
            Developers = new List<Developer>();
            Missions= new List<Mission>();
        }
        public string Name { get; set; }
        public StatusProject Status{ get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer{ get; set; }
        public int? ManagerId { get; set; }
        [ForeignKey(nameof(ManagerId))]
        public virtual Manager Manager { get; set; }
        public virtual ICollection<Developer> Developers { get; set; }
        public virtual ICollection<Mission> Missions{ get; set; }

    }
}
