using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryLibrary
{
    public class ConsolidatedReportEntity
    {
        private ConsolidatedReportEntity() { }

        public ConsolidatedReportEntity(EntityData entity, List<ConsolidatedReportItem> reportItems)
        {
            Entity = entity;
            ReportItems = reportItems;
        }

        public EntityData Entity { get; }
        public List<ConsolidatedReportItem> ReportItems { get; }
        public ConsolidatedReportItem GetReportItem(int consolidated_Account_Id)
        {
            foreach (var reportItem in ReportItems)
                if (reportItem.ConsolidatedAccount.Consolidated_Account_Id == consolidated_Account_Id)
                    return reportItem;
            return null;
        }

        public decimal GetReportSum()
        {
            decimal sum = decimal.Zero;
            foreach (var reportItem in ReportItems)
                sum += reportItem.Amount;
            return sum;
        }
    }
}
