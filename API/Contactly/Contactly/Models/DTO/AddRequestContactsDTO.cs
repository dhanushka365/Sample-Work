namespace Contactly.Models.DTO
{
    public class AddRequestContactsDTO
    {
        public required string Name { get; set; }

        public string? Email { get; set; }

        public required string PhoneNumber { get; set; }
    }
}
