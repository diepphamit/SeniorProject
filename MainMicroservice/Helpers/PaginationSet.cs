﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Helpers
{
    public class PaginationSet<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
