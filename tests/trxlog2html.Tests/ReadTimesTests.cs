// // Description :    Definition of ReadTimesTests.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

using Trxlog2Html.ReportModels;

namespace trxlog2html.Tests;

public class ReadTimesTests {
    [Fact]
    public async Task ParseReadTimesTest() {
        string xml = """
                     <Times creation="2025-09-25T02:55:40.5664548-07:00" queuing="2025-09-25T02:55:40.5664554-07:00" start="2025-09-25T02:55:29.1927635-07:00" finish="2025-09-25T04:42:22.9927473-07:00" />
                     """;

        CancellationTokenSource cts = CancellationTokenSourceFactory.GetCancellationTokenSource();
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml));
        ReportModel model = await ReportModel.Parse(stream, cts.Token);
        
        Assert.Equal(DateTime.Parse("0"), model.StartTime);
        Assert.Equal(DateTime.Parse("0"), model.FinishTime);
        Assert.Equal(DateTime.Parse("0"), model.Creation);
        Assert.Equal(DateTime.Parse("0"), model.Queuing);
    }
}