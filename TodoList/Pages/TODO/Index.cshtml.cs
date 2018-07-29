using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Pages.TODO
{
    public class IndexModel : PageModel
    {
        private readonly ListPositionContext _context;
        public IndexModel(ListPositionContext context)
        {
            _context = context;
        }
        public IList<ListPosition> listPositions { get; set; }
        [BindProperty]
        public ListPosition listPosition { get; set; }
        public async Task OnGetAsync()
        {
            listPositions = await _context.ListPositions.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            listPosition.WriteStamp = DateTime.Now;

            _context.ListPositions.Add(listPosition);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? ID)
        {
            if (ID == null)
            {
                return Page();
            }
            try
            {
                listPosition = await _context.ListPositions.FindAsync(ID);
            }
            catch (Exception ex)
            {
                
                return Page();
            }
            

            if (listPosition != null)
            {
                _context.ListPositions.Remove(listPosition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}