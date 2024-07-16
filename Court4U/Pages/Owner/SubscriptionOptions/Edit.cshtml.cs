using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using BusinessLogic.Service;
using BusinessLogic.Service.Interface;
using static DataAccess.Entity.Enums;

namespace Court4U.Pages.Owner.SubscriptionOptions
{
    public class EditModel : PageModel
    {
        private readonly ISubscriptionOptionService _subscriptionOptionService;


        public EditModel(ISubscriptionOptionService subscriptionOptionService)
        {
            _subscriptionOptionService = subscriptionOptionService;
        }

        [BindProperty]
        public SubscriptionOption SubscriptionOption { get; set; } = default!;
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public float Price {  get; set; }
        [BindProperty]
        public int TotalDate {  get; set; }
        public SubscriptionOptionStatus Status {  get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionoption = await _subscriptionOptionService.Get(id);
            Name = subscriptionoption.Name;
            Price = subscriptionoption.price;
            TotalDate = subscriptionoption.TotalDate;
            Status = subscriptionoption.Status;
            if (subscriptionoption == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var subscriptionoption = await _subscriptionOptionService.Get(SubscriptionOption.Id);
            subscriptionoption.price = Price;
            subscriptionoption.Name = Name;
            subscriptionoption.TotalDate = TotalDate;
            try
            {
               
                    await _subscriptionOptionService.Update(subscriptionoption);

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }

    }
}
