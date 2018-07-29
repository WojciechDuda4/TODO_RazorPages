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
    public class DeletedModel : PageModel
    {
        private readonly ListPositionContext _context;
        public IList<DeletedPosition> deletedPositions { get; set; }
        public string deletionDateHeader { get { return "Deletion Date"; } }
        public DeletedModel(ListPositionContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            deletedPositions = await _context.Deleted.ToListAsync();
        }
    }
}