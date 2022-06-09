using Helpdesk.Core.Services;
using OfficeOpenXml;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class ExcelReportService : IExcelReportService
    {
        public ExcelPackage GenerateExcelReport(int year, int month, CancellationToken cancellationToken = default)
        {


            throw new System.NotImplementedException();
        }
        public void GenerateExcelReportDetail(ExcelPackage excelPackage, int year, int month)
        {
            ExcelPackage excel = new ExcelPackage();
        }
    }
}
