
using ConcertExam.Data.Entities;

namespace Concert.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTicketsAsync();
            await CheckEntrenceAsync();
        }

        private async Task CheckTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                for (int i = 1; i <= 5000; i++)
                {
                    _context.Tickets.Add(new Ticket
                    {
                        Document = null,
                        Entrance = null,
                        DateTime = DateTime.Now,
                        Name = null,
                        WasUsed = false,
                    });
                }

                _context.Tickets.Add(new Ticket
                {

                    WasUsed = true,
                    Document = "1234",
                    Name = "Danny",
                    Entrance = null,
                    DateTime = DateTime.Now,

                });


            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckEntrenceAsync()
        {
            if (!_context.Entrances.Any())
            {
                _context.Entrances.Add(new Entrance { Description = "Norte" });
                _context.Entrances.Add(new Entrance { Description = "Sur" });
                _context.Entrances.Add(new Entrance { Description = "Oriental" });
                _context.Entrances.Add(new Entrance { Description = "Occidental" });
            }
            await _context.SaveChangesAsync();
        }
    }
}
