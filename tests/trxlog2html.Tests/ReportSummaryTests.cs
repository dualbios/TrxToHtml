using Trxlog2Html.ReportModels;

namespace trxlog2html.Tests;

public class ReportSummaryTests {

    [Fact]
    public async Task ParseReportSummary() {
        const string xml = """
                           <?xml version="1.0" encoding="utf-8"?>
                           <TestRun id="ac971973-ea78-43b0-b445-fe89d0270864" name="Test-PC@DESKTOP-xxxxx 2025-09-25 02:55:40" runUser="DESKTOP-xxxxx\Test-PC" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010">
                             <Times creation="2025-09-25T02:55:40.5664548-07:00" queuing="2025-09-25T02:55:40.5664554-07:00" start="2025-09-25T02:55:29.1927635-07:00" finish="2025-09-25T04:42:22.9927473-07:00" />
                           <ResultSummary outcome="Failed">
                               <Counters total="168" executed="168" passed="165" failed="3" error="0" timeout="0" aborted="0" inconclusive="0" passedButRunAborted="0" notRunnable="0" notExecuted="0" disconnected="0" warning="0" completed="0" inProgress="0" pending="0" />
                             </ResultSummary>
                           </TestRun>
                           """;

        CancellationTokenSource cts = CancellationTokenSourceFactory.GetCancellationTokenSource();
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml));

        var result = await ReportModel.Parse(stream, cts.Token);
        
        Assert.Equal("Failed", result.Summary.Outcome);

        Assert.Equal(168, result.Summary.Counters.Total);
        Assert.Equal(168, result.Summary.Counters.Executed);
        Assert.Equal(165, result.Summary.Counters.Passed);
        Assert.Equal(3, result.Summary.Counters.Failed);
        Assert.Equal(0, result.Summary.Counters.Error);
        Assert.Equal(0, result.Summary.Counters.Timeout);
        Assert.Equal(0, result.Summary.Counters.Aborted);
        Assert.Equal(0, result.Summary.Counters.Inconclusive);
        Assert.Equal(0, result.Summary.Counters.PassedButRunAborted);
        Assert.Equal(0, result.Summary.Counters.NotRunnable);
        Assert.Equal(0, result.Summary.Counters.NotExecuted);
        Assert.Equal(0, result.Summary.Counters.Disconnected);
        Assert.Equal(0, result.Summary.Counters.Warning);
        Assert.Equal(0, result.Summary.Counters.Completed);
        Assert.Equal(0, result.Summary.Counters.InProgress);
        Assert.Equal(0, result.Summary.Counters.Pending);
    }


}