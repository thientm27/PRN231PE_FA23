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
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.RoseTattooShop2023DBContext _context;

        public DetailsModel(BusinessObjects.Models.RoseTattooShop2023DBContext context)
        {
            _context = context;
        }

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
    }
}
