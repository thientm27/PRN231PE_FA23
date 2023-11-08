using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Client.Pages.Inheritance;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Client.Pages.Manage
{
    public class DeleteModel : ClientAbstract
    {
        public DeleteModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        [BindProperty]
      public TattooSticker TattooSticker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            string token = _context.HttpContext.Session.GetString("token");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/manage/{id}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TattooSticker = JsonConvert.DeserializeObject<TattooSticker>(content);
            }
            else
            {
                ViewData["Message"] = "Error: " + await response.Content.ReadAsStringAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/manage/{TattooSticker.TattooStickerId}";
            HttpResponseMessage response = await HttpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Message"] = await response.Content.ReadAsStringAsync();
                return Page();
            }
        }
    }
}
