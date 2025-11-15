using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Application.Dtos;

namespace WebAPI.Application.Interfaces
{
    public interface IAlumniService
    {
        /// <summary>
        /// Finds alumni from the same university as the current user, optionally filtering by keyword.
        /// </summary>
        Task<List<AlumniDto>> FindAlumniAsync(int userId, string? keyword = null);

        /// <summary>
        /// Generates a personalized message template to send to a specific alumni.
        /// </summary>
        Task<IntroductionTemplateDto> GenerateIntroTemplateAsync(int userId, int alumniId);
    }
}
