﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Core.RequestModels
{
    public class ProductRequestModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
