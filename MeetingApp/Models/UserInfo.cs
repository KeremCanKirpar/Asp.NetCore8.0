using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Soyisim Zorunludur")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Telefon  Zorunludur")]
        public string? Phone { get; set; }
        
        [Required(ErrorMessage = "Eposta adresi Zorunludur")]
        public string? Email { get; set; }
        public bool WillAttend { get; set; }
    }
}