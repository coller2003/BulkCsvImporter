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

            var singleFileImportOption = new SingleFileImportOption()
                                            .BuildDatabaseConnect(DatabaseType.SQLServer, "data source=114.55.43.65;initial catalog=DHT;persist security info=True;user id=dht_test;password=hj34*yu&[klkg3433;MultipleActiveResultSets=True;")
                                            .BuildImportTarget(true, "OverdueInfo", columns, keys)
                                            .BuildLocalFileSource(@"E:\OverdueInfo.csv");
            var importer = ImporterFactory.CreateInstance(singleFileImportOption);
            importer.Import();
        }
    }
}
