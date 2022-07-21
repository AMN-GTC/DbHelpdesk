using Helpdesk.Core;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class ExcelReportService : IExcelReportService
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public ExcelReportService(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public async Task<byte[]> GenerateExcelReportDetail(ExcelPackage excelPackage, int year, int month, CancellationToken cancellationToken = default)
        {
            FileInfo newfile = new FileInfo(@"E:\ReportAMN.xlsx");
            if (newfile.Exists)
            {
                newfile.Delete();
                newfile = new FileInfo(@"E:\ReportAMN.xlsx");
            }

            var spec = new VwExcelReportDetailFilterSpecification();
            spec.YearEqual = year;
            spec.MonthEqual = month;
            var listdata = await _helpdeskUnitOfWork.VwExcelReportDetail.GetList(spec.Build(), cancellationToken);

            var spesification = new VwExcelReportTicketDetailAllSpecification();
            spesification.YearEqual = year;
            var listalldata = await _helpdeskUnitOfWork.VwExcelReportTicketDetailAll.GetList(spesification.Build(), cancellationToken);

            var spesifi = new VwExcelMonthlySpecification();
            var listmonthly = await _helpdeskUnitOfWork.VwExcelMonthly.GetList(spesifi.Build(), cancellationToken);


            using (ExcelPackage package = new ExcelPackage(newfile))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add("Monthly");

                excelWorksheet.Cells[1, 1].Value = "Ticket Id";
                excelWorksheet.Cells[1, 2].Value = "Project_Name";
                excelWorksheet.Cells[1, 3].Value = "Problem_description";
                excelWorksheet.Cells[1, 4].Value = "Reported_by";
                excelWorksheet.Cells[1, 5].Value = "Submission_date";
                excelWorksheet.Cells[1, 6].Value = "Status_Ticket";
                excelWorksheet.Cells[1, 7].Value = "Start_time";
                excelWorksheet.Cells[1, 8].Value = "End_time";
                excelWorksheet.Column(1).AutoFit();
                excelWorksheet.Column(2).AutoFit();
                excelWorksheet.Column(3).AutoFit();
                excelWorksheet.Column(4).AutoFit();
                excelWorksheet.Column(5).AutoFit();
                excelWorksheet.Column(6).AutoFit();
                excelWorksheet.Column(7).AutoFit();
                excelWorksheet.Column(8).AutoFit();


                for (int i = 0; i < listdata.Count; i++, excelWorksheet.Column(i).Width *= 1.06)
                {

                    excelWorksheet.Cells[i + 2, 1].Value = listdata[i].TicketId;
                    excelWorksheet.Cells[i + 2, 2].Value = listdata[i].Project_Name;
                    excelWorksheet.Cells[i + 2, 3].Value = listdata[i].Problem_description;
                    excelWorksheet.Cells[i + 2, 4].Value = listdata[i].Reported_by;
                    excelWorksheet.Cells[i + 2, 5].Value = listdata[i].Submission_date;
                    excelWorksheet.Cells[i + 2, 6].Value = listdata[i].Status_Ticket;
                    excelWorksheet.Cells[i + 2, 7].Value = listdata[i].Start_time;
                    excelWorksheet.Cells[i + 2, 8].Value = listdata[i].End_time;
                }

                ExcelWorksheet excelWorksheetyear = package.Workbook.Worksheets.Add("Yearly");

                excelWorksheetyear.Cells[1, 1].Value = "TicketId";
                excelWorksheetyear.Cells[1, 2].Value = "Project_Name";
                excelWorksheetyear.Cells[1, 3].Value = "Problem_description";
                excelWorksheetyear.Cells[1, 4].Value = "Reported_by";
                excelWorksheetyear.Cells[1, 5].Value = "Submission_date";
                excelWorksheetyear.Cells[1, 6].Value = "Start_date";
                excelWorksheetyear.Cells[1, 7].Value = "Start_time";
                excelWorksheetyear.Cells[1, 8].Value = "End_date";
                excelWorksheetyear.Cells[1, 9].Value = "End_time";
                excelWorksheetyear.Cells[1, 10].Value = "Status_ticket";
                excelWorksheetyear.Cells[1, 11].Value = "Jam";
                excelWorksheetyear.Cells[1, 12].Value = "Menit_total";
                excelWorksheetyear.Column(1).AutoFit();
                excelWorksheetyear.Column(2).AutoFit();
                excelWorksheetyear.Column(3).AutoFit();
                excelWorksheetyear.Column(4).AutoFit();
                excelWorksheetyear.Column(5).AutoFit();
                excelWorksheetyear.Column(6).AutoFit();
                excelWorksheetyear.Column(7).AutoFit();
                excelWorksheetyear.Column(8).AutoFit();
                excelWorksheetyear.Column(9).AutoFit();
                excelWorksheetyear.Column(10).AutoFit();
                excelWorksheetyear.Column(11).AutoFit();
                excelWorksheetyear.Column(12).AutoFit();

                for (int i = 0; i < listalldata.Count; i++)
                {


                    excelWorksheetyear.Cells[i + 2, 1].Value = listalldata[i].TicketId;
                    excelWorksheetyear.Cells[i + 2, 2].Value = listalldata[i].Project_Name;
                    excelWorksheetyear.Cells[i + 2, 3].Value = listalldata[i].Problem_description;
                    excelWorksheetyear.Cells[i + 2, 4].Value = listalldata[i].Reported_by;
                    excelWorksheetyear.Cells[i + 2, 5].Value = listalldata[i].Submission_date;
                    excelWorksheetyear.Cells[i + 2, 6].Value = listalldata[i].Start_date;
                    excelWorksheetyear.Cells[i + 2, 7].Value = listalldata[i].Start_time;
                    excelWorksheetyear.Cells[i + 2, 8].Value = listalldata[i].End_date;
                    excelWorksheetyear.Cells[i + 2, 9].Value = listalldata[i].End_time;
                    excelWorksheetyear.Cells[i + 2, 10].Value = listalldata[i].Status_Ticket;
                    excelWorksheetyear.Cells[i + 2, 11].Value = listalldata[i].Jam;
                    excelWorksheetyear.Cells[i + 2, 12].Value = listalldata[i].Menit_total;
                }

                ExcelWorksheet excelsummary = package.Workbook.Worksheets.Add("Summary2021");
                excelsummary.Cells[1, 1].Value = "Support Project";
                excelsummary.Cells[1, 2].Value = "Total Minute";
                excelsummary.Cells[1, 3].Value = "Total Hour";
                excelsummary.Cells[1, 4].Value = "Mandays";
                excelsummary.Column(1).AutoFit();
                excelsummary.Column(2).AutoFit();
                excelsummary.Column(3).AutoFit();
                excelsummary.Column(4).AutoFit();
                Dictionary<string, int> listDistinct = new Dictionary<string, int>();


                for (int i = 0; i < listalldata.Count; i++)
                {
                    if (listDistinct.ContainsKey(listalldata[i].Project_Name))
                    {
                        listDistinct[listalldata[i].Project_Name] = listDistinct[listalldata[i].Project_Name] + listalldata[i].Menit_total;
                    }
                    else
                    {
                        listDistinct.Add(listalldata[i].Project_Name, listalldata[i].Menit_total);
                    }
                }
                int x = 0;
                foreach (string key in listDistinct.Keys)
                {
                    excelsummary.Cells[x + 2, 1].Value = key;
                    excelsummary.Cells[x + 2, 2].Value = listDistinct[key];
                    excelsummary.Cells[x + 2, 3].Value = listDistinct[key] / 60;
                    excelsummary.Cells[x + 2, 4].Value = listDistinct[key] / 480;
                    x++;
                }
                ExcelPieChart pieChart = excelsummary.Drawings.AddChart("pieChart", eChartType.Pie) as ExcelPieChart;
                pieChart.SetPosition(0, 0, 6, 0);
                pieChart.SetSize(400, 400);
                pieChart.Series.Add("D2:D11", "A2:A11");
                pieChart.Title.Text = "Usage Per Project";
                pieChart.DataLabel.ShowLeaderLines = true;
                pieChart.DataLabel.ShowPercent = true;

                pieChart.Legend.Add();


                ExcelWorksheet excelPerBulan = package.Workbook.Worksheets.Add("Summary Per Bulan");
                excelPerBulan.Cells[1, 1].Value = "Bulan";
                excelPerBulan.Cells[1, 2].Value = "Total Minute";
                excelPerBulan.Cells[1, 3].Value = "Total Hour";
                excelPerBulan.Cells[1, 4].Value = "Mandays";
                //excelPerBulan.Cells[1, 5].Value = "Usage";
                //excelPerBulan.Cells[1, 6].Value = "Limit";
                excelPerBulan.Column(1).AutoFit();
                excelPerBulan.Column(2).AutoFit();
                excelPerBulan.Column(3).AutoFit();
                excelPerBulan.Column(4).AutoFit();
                //excelPerBulan.Column(5).AutoFit();
                //excelPerBulan.Column(6).AutoFit();

                Dictionary<int, int> listBulan = new Dictionary<int, int>();


                for (int i = 0; i < listalldata.Count; i++)
                {
                    if (listBulan.ContainsKey(listalldata[i].Start_time.Month))
                    {
                        listBulan[listalldata[i].Start_time.Month] += listalldata[i].Menit_total;
                    }
                    else
                    {
                        listBulan.Add(listalldata[i].Start_time.Month, listalldata[i].Menit_total);
                    }

                }
                int b = 0;
                foreach (int bln in listBulan.Keys)
                {
                    excelPerBulan.Cells[b + 2, 1].Value = bln;
                    excelPerBulan.Cells[b + 2, 2].Value = listBulan[bln];
                    excelPerBulan.Cells[b + 2, 3].Value = listBulan[bln] / 60;
                    excelPerBulan.Cells[b + 2, 4].Value = listBulan[bln] / 480;
                    b++;
                }
                ExcelBarChart barChart = excelPerBulan.Drawings.AddChart("barchart", eChartType.ColumnClustered) as ExcelBarChart;
                barChart.SetPosition(0, 0, 6, 0);
                barChart.SetSize(400, 400);
                barChart.Series.Add("D2:D3", "A2:A3");
                barChart.Series.Add("C2:C3", "A2:A3");



                barChart.Title.Text = "Mandays Usage (Accumulative)";
                barChart.DataLabel.ShowLeaderLines = true;
                barChart.DataLabel.ShowCategory = true;
                barChart.DataLabel.ShowPercent = true;

                pieChart.Legend.Add();

                ExcelWorksheet excelBalance = package.Workbook.Worksheets.Add("Balance");
                excelBalance.Cells[1, 1].Value = "Balance";
                excelBalance.Cells[1, 2].Value = "Quota Mandays";
                excelBalance.Cells[1, 3].Value = "Mandays Usage";
                excelBalance.Column(1).AutoFit();
                excelBalance.Column(2).AutoFit();
                excelBalance.Column(3).AutoFit();


                Dictionary<string, int> listBalance = new Dictionary<string, int>();
                for (int i = 0; i < listalldata.Count; i++)
                {

                    if (listBalance.ContainsKey(listalldata[i].Project_Name))
                    {
                        listBalance[listalldata[i].Project_Name] = listalldata[i].Quota;

                    }
                    else
                    {
                        listBalance.Add(listalldata[i].Project_Name, listalldata[i].Quota);
                    }


                }
                int y = 0;
                foreach (string key in listBalance.Keys)
                {
                    excelBalance.Cells[y + 2, 1].Value = key;
                    excelBalance.Cells[y + 2, 2].Value = listBalance[key];
                    excelBalance.Cells["A13"].Value = "Begining Balance";
                    excelBalance.Cells["A14"].Value = "Quota Usage";
                    excelBalance.Cells["A15"].Value = "Last Balance";
                    excelBalance.Cells["C2:C11"].Formula = "=(Summary2021!D2:D11)";
                    excelBalance.Cells["B13"].Formula = "=SUM(B2:B11)";
                    excelBalance.Cells["B14"].Formula = "=SUM(Summary2021!D2:D11)";
                    excelBalance.Cells["B15"].Formula = "=(B13-B14)";
                    y++;
                }



                ExcelWorksheet excelmonthly = package.Workbook.Worksheets.Add("Monthly Trend");

                excelmonthly.Cells[1, 1].Value = "Support Project";
                excelmonthly.Cells[1, 2].Value = "Januari";
                excelmonthly.Cells[1, 3].Value = "Februari";
                excelmonthly.Cells[1, 4].Value = "Maret";
                excelmonthly.Cells[1, 5].Value = "April";
                excelmonthly.Cells[1, 6].Value = "Mei";
                excelmonthly.Cells[1, 7].Value = "Juni";
                excelmonthly.Cells[1, 8].Value = "Juli";
                excelmonthly.Cells[1, 9].Value = "Agustus";
                excelmonthly.Cells[1, 10].Value = "September";
                excelmonthly.Cells[1, 11].Value = "Oktober";
                excelmonthly.Cells[1, 12].Value = "November";
                excelmonthly.Cells[1, 13].Value = "Desember";
                excelmonthly.Column(1).AutoFit();
                excelmonthly.Column(2).AutoFit();
                excelmonthly.Column(3).AutoFit();
                excelmonthly.Column(4).AutoFit();
                excelmonthly.Column(5).AutoFit();
                excelmonthly.Column(6).AutoFit();
                excelmonthly.Column(7).AutoFit();
                excelmonthly.Column(8).AutoFit();
                excelmonthly.Column(9).AutoFit();
                excelmonthly.Column(10).AutoFit();
                excelmonthly.Column(11).AutoFit();
                excelmonthly.Column(12).AutoFit();
                excelmonthly.Column(13).AutoFit();

                for (int i = 0; i < listmonthly.Count; i++)
                {


                    excelmonthly.Cells[i + 2, 1].Value = listmonthly[i].Project_Name;
                    excelmonthly.Cells[i + 2, 2].Value = listmonthly[i].Januari;
                    excelmonthly.Cells[i + 2, 3].Value = listmonthly[i].Februari;
                    excelmonthly.Cells[i + 2, 4].Value = listmonthly[i].Maret;
                    excelmonthly.Cells[i + 2, 5].Value = listmonthly[i].April;
                    excelmonthly.Cells[i + 2, 6].Value = listmonthly[i].Mei;
                    excelmonthly.Cells[i + 2, 7].Value = listmonthly[i].Juni;
                    excelmonthly.Cells[i + 2, 8].Value = listmonthly[i].Juli;
                    excelmonthly.Cells[i + 2, 9].Value = listmonthly[i].Agustus;
                    excelmonthly.Cells[i + 2, 10].Value = listmonthly[i].September;
                    excelmonthly.Cells[i + 2, 11].Value = listmonthly[i].Oktober;
                    excelmonthly.Cells[i + 2, 12].Value = listmonthly[i].November;
                    excelmonthly.Cells[i + 2, 13].Value = listmonthly[i].Desember;
                }

                ExcelLineChart lineChart = excelmonthly.Drawings.AddChart("lineChart", eChartType.Line) as ExcelLineChart;
                lineChart.SetPosition(12, 12, 1, 14);
                lineChart.SetSize(800, 400);
                //for (int i = 0; i < listmonthly.Count; i++)
                //{
                //    lineChart.Series.Add(excelmonthly.Cells[i + 2, 2], excelmonthly.Cells[i + 2, 13]);
                //}


                lineChart.Series.Add("B2:M2", "B1:M1");
                lineChart.Series.Add("B3:M3", "B1:M1");
                lineChart.Series.Add("B4:M4", "B1:M1");
                lineChart.Series.Add("B5:M5", "B1:M1");
                lineChart.Series.Add("B6:M6", "B1:M1");
                lineChart.Series.Add("B7:M7", "B1:M1");
                lineChart.Series.Add("B8:M8", "B1:M1");
                lineChart.Series.Add("B9:M9", "B1:M1");
                lineChart.Series.Add("B10:M10", "B1:M1");
                lineChart.Series.Add("B11:M11", "B1:M1");


                for (int i = 0; i < listmonthly.Count; i++)
                {
                    lineChart.Series[i].Header = excelmonthly.Cells[i + 2, 1].Value.ToString();
                    lineChart.Title.Text = "Monthly Trend";
                    lineChart.Legend.Add();
                }


                excelPackage.Workbook.Calculate();
                package.SaveAs(new FileInfo(@"E:\ReportAMN.xlsx"));
                return package.GetAsByteArray();
            }


        }
        public async Task<byte[]> GenerateExcelReport(int year, int month, CancellationToken cancellationToken = default)
        {
            ExcelPackage package = new ExcelPackage();
            var excell = await GenerateExcelReportDetail(package, year, month, cancellationToken);
            return excell;
        }

    }
}
