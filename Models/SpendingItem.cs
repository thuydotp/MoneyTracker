using System;

namespace MoneyTracker.Models
{
    public class SpendingItem
    {
        public Guid ID { get; set; }
        
        public SpendingType Type { get; set; }

        public string SpendingTypeName
        {
            get
            {
                switch (this.Type)
                {
                    case SpendingType.Expense:
                        return "Expense";
                    case SpendingType.Income:
                        return "Income";
                    default:
                        return "Transaction";
                }
            }
        }
        public decimal ChangeValue { get; set; }

        public string Description { get; set; }

        public Guid CategoryID { get; set; }

        public string CategoryName { get; set; }

        public Guid SpendingAccountID { get; set; }

        public string SpendingAccountName { get; set; }
    }

    public enum SpendingType
    {
        Expense = 0,
        Income = 1
    }
}
