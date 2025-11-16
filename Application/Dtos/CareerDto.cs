using System;

namespace CareerCompassWebApi.Application.Dtos;

public class CareerDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public double MarketDemandScore { get; set; }
    public int SalaryPotentialScore { get; set; }
    public double ProfileMatchScore { get; set; } 
    public double CompositeScore { get; set; }
}
