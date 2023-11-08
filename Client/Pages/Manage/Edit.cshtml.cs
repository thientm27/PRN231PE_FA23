using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Client.Pages.Inheritance;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Pages.Manage
{
    public class EditModel : ClientAbstract
    {
        public EditModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
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
            // LOAD ADDITION
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync("api/manage/type");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var temp = JsonConvert.DeserializeObject<List<RoseTattooType>>(content);
                ViewData["TypeId"] = new SelectList(temp, "TypeId", "RoseTattooName");
            }

            // LOAD ENTITY
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/Manage/{id}";
            response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TattooSticker = JsonConvert.DeserializeObject<TattooSticker>(content);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = "api/manage";
            var jsonContent = JsonConvert.SerializeObject(TattooSticker);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Message"] = "Update Fail: " + await response.Content.ReadAsStringAsync();
                await OnGetAsync(TattooSticker.TattooStickerId);
                return Page();
            }
        }
    }
}
