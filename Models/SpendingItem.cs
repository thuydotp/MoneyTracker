using System;

namespace MoneyTracker.Models
{
    public class SpendingItem
    {
        public Guid ID { get; set; }
        public SpendingType Type { get; set; }
        public decimal ChangeValue { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
        public Guid SpendingAccountID { get; set; }
    }

    public enum SpendingType
    {
        Expense = 0,
        Income = 1
    }
}
