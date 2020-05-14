﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PricingBookBsDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public List<ProductPriceBsDTO> ProductPrices { get; set; }
    }
}
