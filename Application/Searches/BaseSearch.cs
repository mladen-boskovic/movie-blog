using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public abstract class BaseSearch
    {
        public int PerPage { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;
    }
}
