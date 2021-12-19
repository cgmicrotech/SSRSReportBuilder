using Grpc.Core;
using ReportServer;

namespace ReportServer.Services
{
    public class ReportService : ReportServices.ReportServicesBase
    {
        private readonly ILogger<ReportService> _logger;
        public ReportService(ILogger<ReportService> logger)
        {
            _logger = logger;
        }

        public override Task<PrintResult> PrintReport(PrintRequest request, ServerCallContext context)
        {
            return base.PrintReport(request, context);
        }

        public override Task<ReportResult> GenerateReport(ReportGenerationRequest request, ServerCallContext context)
        {
            return base.GenerateReport(request, context);
        }


    }
}