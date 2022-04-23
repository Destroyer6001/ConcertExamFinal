using System.ComponentModel.DataAnnotations;

namespace ConcertExam.Models
{
    public class EditTicketViewModel
    {
        [Display(Name = "Numero ticket")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Fecha y hora")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Estado del ticket")]
        public bool WasUsed { get; set; }
    }
}
