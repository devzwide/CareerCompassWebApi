namespace WebAPI.Application.Dtos
{
    public class AlumniDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public int? GraduatingYear { get; set; } // Optional, if you had this field
    }
}
