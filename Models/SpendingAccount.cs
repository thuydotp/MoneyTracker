using System;
using System.Collections.Generic;

namespace MoneyTracker.Models
{
    public class SpendingAccount
    {
        public Guid ID { get; set; }

        public string AccountName { get; set; }

        List<SpendingItem> SpendingItems { get; set; }
    }
}
