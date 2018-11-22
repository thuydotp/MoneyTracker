using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Models
{
    public class Transaction
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
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd-MM-yyyy}", ApplyFormatInEditMode=true)]
        public DateTime RecordDate { get; set; }
    }

    public enum SpendingType
    {
        Expense = 0,
        Income = 1
    }
}
