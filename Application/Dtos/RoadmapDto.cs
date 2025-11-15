using System.Collections.Generic;

namespace WebAPI.Application.Dtos
{
    public class RoadmapDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public List<RoadmapStepDto> Steps { get; set; } = new List<RoadmapStepDto>();
    }
}
