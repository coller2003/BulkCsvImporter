using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkCsvImporter.Extension;
using BulkCsvImporter.Enum;

namespace BulkCsvImporter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var columns = "Id,ApplicationNumber,OverdueDays,AveOverdueDays,OverdueLevel,OverdueMoney,OverduePeriods,HavePayPeriods,FinancingMaturity,TheHightOverduePeriods,TotalOverduePeriods,RemitDays,OverdueStatus,RiskLevel,FiveLevelClass,IsOverdueNow,CarAge,RedBookOfSecondHand,SaleActualSurplusMoney,SaleCurrentSurplusMoney,DQSYZJ,FinancialActualSurplusMoney,FinancialCurrentSurplusMoney,DisposalAssessmentAmount,DisposalAmount,CLSFSH,VehicleRecyclingNotice,ElectricityStatus,ElectricityOperate,LitigationStatus,LitigationOperators,LitigationDate,FamilyVisitState,HomeVisitsOperator,FamilyVisitApplicationDate,CollectState,CollectOperator,CollectApplicationDate,VehicleDisposalStatus,VehicleDisposalOperator,VehicleDisposalApplicationDate,ChargeOffStatus,ChargeOffApplicationDate,ChargeOffMoney,ChargeOffOperate,YQRQ".Split(',').ToList();
            var keys = new List<string>() { "ApplicationNumber" };
            var connectionString = "data source=.;initial catalog=sa;persist security info=True;user id=dht_test;password=123456;MultipleActiveResultSets=True;";

            var singleFileImportOption = new SingleFileImportOption()
                                        .BuildDatabaseConnect(DatabaseType.SQLServer, connectionString)
                                        .BuildImportTarget(true, "OverdueInfo", columns, keys)
                                        .BuildLocalFileSource(@"E:\OverdueInfo.csv");
            var importer = ImporterFactory.CreateInstance(singleFileImportOption);
            importer.Import();
        }
    }
}
