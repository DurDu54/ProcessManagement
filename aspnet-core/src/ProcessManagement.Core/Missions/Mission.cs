using Abp.Domain.Entities.Auditing;
using ProcessManagement.Developers;
using ProcessManagement.Enums;
using ProcessManagement.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessManagement.Missions
{
    [Table("Missions")]
    public class Mission : FullAuditedEntity
    {
        public Mission()
        {
            Commits = new List<Commit>();
        }

        public string Text { get; set; }
        public StatusMission Status { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual ICollection<Commit> Commits { get; set; }

        public int? DeveloperId { get; set; }
        [ForeignKey(nameof(DeveloperId))]
        public virtual Developer Developers { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public virtual Project Projects { get; set; }
    }
}
