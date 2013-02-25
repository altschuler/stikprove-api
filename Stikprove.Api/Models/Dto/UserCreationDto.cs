namespace Stikprove.Api.Models.Dto
{
    public class UserCreationDto : UserDto
    {
        public string Password { get; set; }
        public string EnergyPassword { get; set; }
    }
}