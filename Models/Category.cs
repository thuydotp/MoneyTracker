﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTracker.Models
{
    public class Category
    {
        public Guid ID { get; set; }

        public string CategoryName { get; set; }

        public Guid IconID { get; set; }

        public SpendingType Type { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
