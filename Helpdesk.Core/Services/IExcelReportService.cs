using OfficeOpenXml;
using System.Threading;

namespace Helpdesk.Core.Services
{
    public interface IExcelReportService
    {
        ExcelPackage GenerateExcelReport(int year, int month, CancellationToken cancellationToken = default);
    }
}
