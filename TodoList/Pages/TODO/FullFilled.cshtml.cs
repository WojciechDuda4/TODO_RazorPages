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
    public class FullFilledModel : PageModel
    {
        private readonly ListPositionContext _context;
        public FullFilledModel(ListPositionContext context)
        {
            _context = context;
        }
        public string filledDateHeader { get { return "Fullfill date"; } }
        public IList<FullFilledPosition> fullFilledPositions { get; set; }
        public async Task OnGetAsync()
        {
            fullFilledPositions = await _context.FullFilled.ToListAsync();
        }
    }
}