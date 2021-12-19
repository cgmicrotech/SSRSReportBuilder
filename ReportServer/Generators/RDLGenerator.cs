using Google.Protobuf;

namespace ReportServer.Generators;

public class RDLGenerator
{
    public Task<ReportResult> GeneratePDF(ByteString rdlDefinition, DataSet data)
    {
        return new Task<ReportResult>(() => new ReportResult()
        {
            ReportType = ReportType.Pdf
        });
    }
}