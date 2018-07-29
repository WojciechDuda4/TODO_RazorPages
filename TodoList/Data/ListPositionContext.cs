using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Data
{
    public class ListPositionContext : DbContext
    {
        public ListPositionContext(DbContextOptions<ListPositionContext> options) : base(options)
        {

        }

        public DbSet<ListPosition> ListPositions { get; set; }
    }
}
