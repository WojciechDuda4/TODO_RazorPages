using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.ViewModels
{
    public interface IArchiveViewModel
    {
        string Description { get; set; }

        DateTime WriteStamp { get; set; }
    }
}
