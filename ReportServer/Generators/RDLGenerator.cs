using Google.Protobuf;
using SSRSReportGeneratorServer;

public class RDLGenerator
{
    public Task<ReportResult> GeneratePDF(ByteString rdlDefinition, DataSet data)
    {
        return Task.Run(() =>
        {
            var rslt = new ReportResult()
            {
                ReportType = ReportType.Pdf,
                BinaryData = ByteString.CopyFrom(new byte[]{1})
            };
            return rslt;
        });
    }
}