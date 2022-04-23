using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConcertExam.Models
{
    public class TicketViewModel : EditTicketViewModel
    {
        public int EntranceId { get; set; }

        public IEnumerable<SelectListItem> Entrances { get; set; }
    }
}
