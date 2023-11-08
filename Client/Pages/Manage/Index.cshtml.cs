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
using System.Text.RegularExpressions;

namespace Client.Pages.Manage
{
    public class IndexModel : ClientAbstract
    {
        public IndexModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public IList<TattooSticker> TattooSticker { get;set; } = default!;
        [BindProperty]
        public string searchString { get; set; }
        [BindProperty]
        public DateTime importDate { get; set; }

        public async Task OnGetAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync("api/manage");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TattooSticker = JsonConvert.DeserializeObject<List<TattooSticker>>(content);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/manage/search?searchString={searchString}&searchDate={importDate}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TattooSticker = JsonConvert.DeserializeObject<List<TattooSticker>>(content);
                return Page();
            }
            ViewData["Message"] = "TattooSticker don't exits!";
            await OnGetAsync();
            return Page();
     
          
        }
        public async Task OnPostResetAsync()
        {
            await  OnGetAsync();
        }
    }
}
