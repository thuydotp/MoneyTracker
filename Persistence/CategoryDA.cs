﻿using System;
using System.Collections.Generic;

namespace MoneyTracker.Persistence
{
    public class CategoryDA
    {
        public Guid ID { get; set; }

        public string CategoryName { get; set; }

        public Guid IconID { get; set; }
        List<SpendingItemDA> SpendingItems { get; set; }
    }
}