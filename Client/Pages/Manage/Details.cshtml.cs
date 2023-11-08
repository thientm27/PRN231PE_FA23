using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Client.Pages.Inheritance;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Client.Pages.Manage
{
    public class DetailsModel : ClientAbstract
    {
        public DetailsModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public TattooSticker TattooSticker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            string token = _context.HttpContext.Session.GetString("token");
            // Thêm token vào tiêu đề yêu cầu HTTP
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
                ViewData["Message"] =   await response.Content.ReadAsStringAsync();
            }
            return Page();
        }
    }
}
