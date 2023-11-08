using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace Client.Pages.Manage
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.RoseTattooShop2023DBContext _context;

        public DeleteModel(BusinessObjects.Models.RoseTattooShop2023DBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TattooSticker TattooSticker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TattooStickers == null)
            {
                return NotFound();
            }

            var tattoosticker = await _context.TattooStickers.FirstOrDefaultAsync(m => m.TattooStickerId == id);

            if (tattoosticker == null)
            {
                return NotFound();
            }
            else 
            {
                TattooSticker = tattoosticker;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TattooStickers == null)
            {
                return NotFound();
            }
            var tattoosticker = await _context.TattooStickers.FindAsync(id);

            if (tattoosticker != null)
            {
                TattooSticker = tattoosticker;
                _context.TattooStickers.Remove(TattooSticker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
