
using ConcertExam.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConcertExam.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync();
        Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync(IEnumerable<Entrance> filter);
    }
}
