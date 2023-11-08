using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace Client.Pages.Manage
{
    public class EditModel : PageModel
    {
        private readonly BusinessObjects.Models.RoseTattooShop2023DBContext _context;

        public EditModel(BusinessObjects.Models.RoseTattooShop2023DBContext context)
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

            var tattoosticker =  await _context.TattooStickers.FirstOrDefaultAsync(m => m.TattooStickerId == id);
            if (tattoosticker == null)
            {
                return NotFound();
            }
            TattooSticker = tattoosticker;
           ViewData["TypeId"] = new SelectList(_context.RoseTattooTypes, "TypeId", "TypeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TattooSticker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TattooStickerExists(TattooSticker.TattooStickerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TattooStickerExists(int id)
        {
          return (_context.TattooStickers?.Any(e => e.TattooStickerId == id)).GetValueOrDefault();
        }
    }
}
