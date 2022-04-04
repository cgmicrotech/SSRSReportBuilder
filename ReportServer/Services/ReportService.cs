using Grpc.Core;
using SSRSReportGeneratorServer;
using static SSRSReportGeneratorServer.ReportServices;

namespace ReportServer.Services
{
    public class ReportService : ReportServicesBase
    {
        private readonly ILogger<ReportService> _logger;
        public ReportService(ILogger<ReportService> logger)
        {
            _logger = logger;
        }

        public override async Task<ReportResult> GenerateReport(ReportGenerationRequest request, ServerCallContext context)
        {
            var result = new ReportResult();

            result = await new RDLGenerator().GeneratePDF(request.ReportDefinition, request.ReportData);
            return result;
        }

    }
}