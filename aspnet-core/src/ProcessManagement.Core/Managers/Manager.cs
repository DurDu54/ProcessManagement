using Abp.Domain.Entities.Auditing;
using ProcessManagement.Authorization.Users;
using ProcessManagement.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Managers
{
    [Table("Managers")]
    public class Manager:FullAuditedEntity
    {
        public Manager()
        {
            Projects = new List<Project>();
        }
        public virtual User User { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}
