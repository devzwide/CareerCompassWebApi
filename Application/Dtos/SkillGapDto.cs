namespace WebAPI.Application.Dtos
{
    public class SkillGapDto
    {
        public string SkillName { get; set; }

        /// <summary>
        /// True if the user has this skill on their profile.
        /// </summary>
        public bool HasSkill { get; set; }

        /// <summary>
        /// The percentage (0.0 to 1.0) of job postings that require this skill.
        /// </summary>
        public double MarketDemand { get; set; }

        /// <summary>
        /// True if this skill is part of the "core" learning roadmap.
        /// </summary>
        public bool IsCoreRequirement { get; set; }
    }
}
