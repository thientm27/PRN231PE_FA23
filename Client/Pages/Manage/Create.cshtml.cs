using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Client.Pages.Inheritance;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Pages.Manage
{
    public class CreateModel : ClientAbstract
    {
        public CreateModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }


        public async Task OnGet()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync("api/manage/type");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var temp = JsonConvert.DeserializeObject<List<RoseTattooType>>(content);
                ViewData["TypeId"] = new SelectList(temp, "TypeId", "RoseTattooName");
            }
        }

        [BindProperty]
        public TattooSticker TattooSticker { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = "api/manage";
            var jsonContent = JsonConvert.SerializeObject(TattooSticker);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else 
            {
                ViewData["Message"] = "Create Fail: " + await response.Content.ReadAsStringAsync();
                await OnGet();
                return Page();
            }
        }
    }
}
