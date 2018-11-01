﻿using System;
using System.Collections.Generic;

namespace MoneyTracker.Persistence
{
    public class SpendingAccountDA
    {
        public Guid ID { get; set; }

        public string AccountName { get; set; }

        List<SpendingItemDA> SpendingItems { get; set; }
    }
}