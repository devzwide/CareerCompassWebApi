using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for a ranked career recommendation.
    /// </summary>
    public class CareerRecommendationDto
    {
        public int CareerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double MarketDemandScore { get; set; }
        public int SalaryPotentialScore { get; set; }
        public double ProfileMatchScore { get; set; } 
        public double CompositeScore { get; set; }
    }
}
