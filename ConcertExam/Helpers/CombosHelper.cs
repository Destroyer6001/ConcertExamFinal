using Concert.Data;
using ConcertExam.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConcertExam.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync()
        {
            List<SelectListItem> list = await _context.Entrances.Select(c => new SelectListItem
            {
                Text = c.Description,
                Value = c.Id.ToString()
            })
               .OrderBy(c => c.Text)
               .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione una entrada...", Value = "0" });
            return list;

        }

        public async Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync(IEnumerable<Entrance> filter)
        {
            List<Entrance> entrances = await _context.Entrances.ToListAsync();
            List<Entrance> entranceFiltered = new();
            foreach (Entrance entrance in entrances)
            {
                if (!filter.Any(c => c.Id == entrance.Id))
                {
                    entranceFiltered.Add(entrance);
                }
            }

            List<SelectListItem> list = entranceFiltered.Select(c => new SelectListItem
            {
                Text = c.Description,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "[Seleccione una categoría...", Value = "0" });
            return list;
        }
    }
}
