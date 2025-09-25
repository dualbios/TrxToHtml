// // Description :    Definition of UnitTestTests.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

using Trxlog2Html.ReportModels;

namespace trxlog2html.Tests;

public class UnitTestTests {
    [Fact]
    public async Task ParseUnitTest() {
        string xml = """
            <TestDefinitions>
            <UnitTest name="Test1" storage="Storage1" id="1" />
            <UnitTest name="Test2" storage="Storage2" id="2" />
            <UnitTest name="Test3" storage="Storage3" id="3" />
            </TestDefinitions>
            """;

        CancellationTokenSource cts = CancellationTokenSourceFactory.GetCancellationTokenSource();
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml));
        ReportModel model = await ReportModel.Parse(stream, cts.Token);
        
        Assert.Equal(3, model.UnitTests.Length);
        Assert.Equal("Test1", model.UnitTests[0].Name);
        Assert.Equal("1", model.UnitTests[0].Id);
        Assert.Equal("Storage1", model.UnitTests[0].Storage);


        Assert.Equal("Test2", model.UnitTests[1].Name);
        Assert.Equal("2", model.UnitTests[1].Id);
        Assert.Equal("Storage2", model.UnitTests[1].Storage);

        Assert.Equal("Test3", model.UnitTests[2].Name);
        Assert.Equal("3", model.UnitTests[2].Id);
        Assert.Equal("Storage3", model.UnitTests[2].Storage);
    }
}