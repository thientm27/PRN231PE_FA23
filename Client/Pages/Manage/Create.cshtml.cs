using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;

namespace Client.Pages.Manage
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObjects.Models.RoseTattooShop2023DBContext _context;

        public CreateModel(BusinessObjects.Models.RoseTattooShop2023DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TypeId"] = new SelectList(_context.RoseTattooTypes, "TypeId", "TypeId");
            return Page();
        }

        [BindProperty]
        public TattooSticker TattooSticker { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TattooStickers == null || TattooSticker == null)
            {
                return Page();
            }

            _context.TattooStickers.Add(TattooSticker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
