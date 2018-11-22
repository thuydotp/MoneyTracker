using System;
using System.Collections.Generic;

namespace MoneyTracker.Persistence
{
    public class SpendingAccountDA
    {
        public Guid ID { get; set; }

        public string AccountName { get; set; }

        public virtual List<TransactionDA> Transactions { get; set; }
    }
}
