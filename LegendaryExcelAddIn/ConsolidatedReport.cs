using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryExcelAddIn
{
    public enum ConsolidatedReportType
    {
        IncomeStatement = 1,
        BalanceSheet = 2
    }

    public class ConsolidatedReport
    {
        private ConsolidatedReport() { }

        public ConsolidatedReport(string fundFullName, string reportTitle, DateTime reportDate, ConsolidatedReportType reportType,
                                  List<ConsolidatedAccount> consolidatedAccounts, List<ConsolidatedReportEntity> reportEntities)
        {
            FundFullName = fundFullName;
            ReportTitle = reportTitle;
            ReportDate = reportDate;
            ReportType = reportType;
            ReportEntities = reportEntities;
            ConsolidatedAccounts = consolidatedAccounts;
        }

        public string FundFullName { get; }
        public string ReportTitle { get; }
        public DateTime ReportDate { get; }
        public ConsolidatedReportType ReportType { get; }
        public List<ConsolidatedReportEntity> ReportEntities { get; }
        public List<ConsolidatedAccount> ConsolidatedAccounts { get; }

        static public List<ConsolidatedReportEntity> GetConsolidatedReportEntities(DateTime reportDate,
                                                                                   string consolidatedType,
                                                                                   string fund,
                                                                                   List<EntityData> entities,
                                                                                   List<ConsolidatedAccount> consolidatedAccounts,
                                                                                   List<LedgerAccount> ledgerAccounts,
                                                                                   List<AccountData> accountData)
        {
            var reportEntities = new List<ConsolidatedReportEntity>();
            var reportConsolidatedAccounts = ConsolidatedAccount.GetByConsolidatedType(consolidatedType, consolidatedAccounts);
            foreach (var entity in entities)
                if (string.Compare(entity.Fund, fund, true) == 0)
                    reportEntities.Add(GetConsolidatedReportEntity(reportDate, entity, reportConsolidatedAccounts, ledgerAccounts, accountData));
            return reportEntities;       
        }

        static public ConsolidatedReportEntity GetConsolidatedReportEntity(DateTime reportDate, 
                                                                           EntityData entity, 
                                                                           List<ConsolidatedAccount> consolidatedAccounts,
                                                                           List<LedgerAccount> ledgerAccounts,
                                                                           List<AccountData> accountData )
        {
            var reportItems = new List<ConsolidatedReportItem>();
            foreach (var consolidatedAccount in consolidatedAccounts)
            {
                List<AccountData> accountAmounts = GetAccountAmounts(entity, consolidatedAccount, ledgerAccounts, accountData);
                var reportItem = new ConsolidatedReportItem(consolidatedAccount, accountAmounts);
                reportItems.Add(reportItem);
            }
            var consolidatedReportEntity = new ConsolidatedReportEntity(entity, reportItems);
            return consolidatedReportEntity;
        }

        static public List<AccountData> GetAccountAmounts(EntityData entity, ConsolidatedAccount consolidated, List<LedgerAccount> ledgerAccounts, List<AccountData> accountData)
        {
            List<AccountData> accountAmounts = new List<AccountData>();
            foreach (var data in accountData)
                if (data.Entity_Id == entity.Entity_Id)
                    foreach (var ledgerAccount in ledgerAccounts)
                        if ((ledgerAccount.Ledger_Account_Id == data.Ledger_Account_Id) && (ledgerAccount.Consolidated_Account_Id == consolidated.Consolidated_Account_Id))
                            accountAmounts.Add(data);
            return accountAmounts;
        }

        static public void WriteConsolidatedSumToAccountData(LedgerAccount ledgerAccount, DateTime valueDate, 
                                                             List<ConsolidatedReportEntity> reportEntities, bool invertSum = false)
        {
            foreach (var reportEntity in reportEntities)
            {
                decimal sum = reportEntity.GetReportSum();
                var accountData = new AccountData(0, ledgerAccount.Ledger_Account_Id, reportEntity.Entity.Entity_Id, valueDate, 
                                                  (sum * (invertSum ? (decimal)-1.0 : (decimal)1.0)), valueDate.Year, valueDate.Month, valueDate.Day);
                AccountData.InsertOrUpdateOneAccountData(accountData);
            }
        }

    }
}
