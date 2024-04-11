using System.ComponentModel.DataAnnotations;

namespace PayAndPlay.Models
{
    public class QRCodeModel
    { 
        [Display(Name = "Código QR")]
        public string? QRCodeText { get; set; }
    }
}
