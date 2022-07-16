using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelReportController : ControllerBase
    {
        private readonly IExcelReportService _excelReportService;
        public ExcelReportController(IExcelReportService excelReportService)
        {
            _excelReportService = excelReportService;
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] Dictionary<string, string> filter, int year, int month, CancellationToken cancellationToken = default)
        {
            VwExcelReportDetailFilter filter1 = new VwExcelReportDetailFilter(filter);
            var vwExcelReportDetail = await _excelReportService.GenerateExcelReport(year, month, cancellationToken);
            return Ok(vwExcelReportDetail);
        }
    }
}
