using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryLibrary
{
    public class ConsolidatedReportItem
    {
        private ConsolidatedReportItem() { }

        public ConsolidatedReportItem(ConsolidatedAccount consolidatedAccount, List<AccountData> accountAmounts)
        {
            AccountAmounts = accountAmounts;
            ConsolidatedAccount = consolidatedAccount;
        }

        public decimal Amount
        {
            get
            {
                decimal amount = decimal.Zero;
                foreach (var accountAmount in AccountAmounts)
                    amount += accountAmount.Value;
                return amount;
            }
        }
        public List<AccountData> AccountAmounts { get; }
        public ConsolidatedAccount ConsolidatedAccount { get; }

    }
}
