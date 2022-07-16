using OfficeOpenXml;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IExcelReportService
    {
        public Task<byte[]> GenerateExcelReportDetail(ExcelPackage excelPackage, int year, int month, CancellationToken cancellationToken = default);
        public Task<byte[]> GenerateExcelReport(int year, int month, CancellationToken cancellationToken = default);
    }
}
