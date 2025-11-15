using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities
{
    /// <summary>
    /// Represents the aggregated market demand for a skill,
    /// as determined by an (external) ETL process.
    /// </summary>
    public class JobMarketSkill
    {
        [Key]
        public int Id { get; set; }

        public int CareerId { get; set; }
        public Career Career { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        /// <summary>
        /// The percentage (0.0 to 1.0) of job postings
        /// for this career that list this skill.
        /// </summary>
        public double Frequency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
