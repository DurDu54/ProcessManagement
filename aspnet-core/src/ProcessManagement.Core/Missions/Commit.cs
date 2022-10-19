using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessManagement.Missions
{
    [Table("Commits")]
    public class Commit : FullAuditedEntity
    {
        public string Text { get; set; }
        public int MissionId { get; set; }
        [ForeignKey(nameof(MissionId))]
        public virtual Mission Missons { get; set; }
    }
}
