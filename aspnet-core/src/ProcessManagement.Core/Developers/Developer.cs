﻿using Abp.Domain.Entities.Auditing;
using ProcessManagement.Authorization.Users;
using ProcessManagement.Missions;
using ProcessManagement.Professions;
using ProcessManagement.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Developers
{ 
    [Table("Developers")]
    public class Developer:FullAuditedEntity
    {
        public Developer()
        {
            Projects = new List<Project>();
            Missions = new List<Mission>();
        }
        public virtual User User { get; set; }
        public int ProfessionId { get; set; }
        [ForeignKey(nameof(ProfessionId))]
        public virtual Profession Profession { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Mission> Missions{ get; set; }


    }
}
