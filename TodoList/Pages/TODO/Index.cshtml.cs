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
            listPositions = await _context.TODO.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            listPosition.WriteStamp = DateTime.Now;

            _context.TODO.Add(listPosition);
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
                listPosition = await _context.TODO.FindAsync(ID);
            }
            catch (Exception ex)
            {
                return Page();
            }

            DeletedPosition deletedPosition = new DeletedPosition();
            deletedPosition.Task = listPosition.Task;
            deletedPosition.DeletionDate = DateTime.Now;

            if (listPosition != null)
            {
                _context.TODO.Remove(listPosition);
                await _context.SaveChangesAsync();
                _context.Deleted.Add(deletedPosition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostAddAsync(int? ID)
        {
            if (ID == null)
            {
                return Page();
            }
            try
            {
                listPosition = await _context.TODO.FindAsync(ID);
            }
            catch (Exception ex)
            {
                return Page();
            }
            FullFilledPosition fullFilledPosition = new FullFilledPosition();
            fullFilledPosition.Task = listPosition.Task;
            fullFilledPosition.WriteStamp = DateTime.Now;

            if (listPosition != null)
            {
                _context.TODO.Remove(listPosition);
                _context.FullFilled.Add(fullFilledPosition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}