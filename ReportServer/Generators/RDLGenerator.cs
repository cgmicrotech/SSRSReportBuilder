using System.Data;
using AspNetCore.Reporting;
using Google.Protobuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSRSReportGeneratorServer;
using DataSet = SSRSReportGeneratorServer.DataSet;
using ReportResult = SSRSReportGeneratorServer.ReportResult;

public class RDLGenerator
{
    private string ReportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");

    private IEnumerable<DataTable> CreateTablesFromDataSet(IEnumerable<DataSet> data)
    {
        var result = new List<DataTable>();
        
        foreach (var set in data)
        {
            var table = new DataTable(set.SetName);
            IEnumerable<JObject> rows = JsonConvert.DeserializeObject<IEnumerable<JObject>>(set.SerializedData);
            
            
            foreach (var row in rows)
            {
                var tblRow = table.NewRow();
                foreach (var column in row.Properties())
                {
                    if (!table.Columns.Contains(column.Name))
                        table.Columns.Add(column.Name, typeof(string));

                    tblRow[column.Name] = row.GetValue(column.Name);
                }

                table.Rows.Add(tblRow);
            }
            result.Add(table);
        }

        return result;
    }

    public async Task<ReportResult> GeneratePDF(ByteString rdlDefinition, IEnumerable<DataSet> data)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var fileNameGuid = Guid.NewGuid();
        if (!Directory.Exists(ReportDirectory))
            Directory.CreateDirectory(ReportDirectory);

        var reportPath = Path.Join(ReportDirectory, $"{Guid.NewGuid()}.rdlc");
        
        await File.WriteAllBytesAsync(reportPath, rdlDefinition.ToByteArray());
        var rpt = new LocalReport(reportPath);

        var tableData = CreateTablesFromDataSet(data);

        foreach (var table in tableData)
            rpt.AddDataSource(table.TableName, table);

        var binary = rpt.Execute(RenderType.Pdf);

        var result = new ReportResult()
        {
            ReportType = ReportType.Pdf,
            BinaryData = ByteString.CopyFrom(binary.MainStream),
        };
        
        if(File.Exists(reportPath))
            File.Delete(reportPath);
        
        return result;
    }
}