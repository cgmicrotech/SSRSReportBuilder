using Grpc.Core;
using ReportServer;
using ReportServer.Generators;

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
            return request.RepotType switch
            {
                ReportType.Pdf => new RDLGenerator().GeneratePDF(request.ReportDefinition, request.ReportData),
                ReportType.Xls => throw new NotImplementedException(),
                ReportType.Html => throw new NotImplementedException(),
                _ => throw new InvalidDataException("Unable to determine a proper way to generate the chosen report type.")
            };
        }

    }
}