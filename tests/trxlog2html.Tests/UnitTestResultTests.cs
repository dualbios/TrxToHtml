// // Description :    Definition of UnitTestResultTests.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

using Trxlog2Html.ReportModels;

namespace trxlog2html.Tests;

public class UnitTestResultTests {
    [Fact]
    public async Task ParseUnitTestResult() {
        string xml = """
                     <Results>
                       <UnitTestResult executionId="E1" testId="T1" testName="Test A" computerName="Computer1" duration="00:00:30" startTime="2025-10-21T10:00:00Z" endTime="2025-10-21T10:00:30Z" testType="UnitTest" outcome="Passed" testListId="List1" relativeResultsDirectory="Results/Dir1" />
                       <UnitTestResult executionId="E2" testId="T2" testName="Test B" computerName="Computer2" duration="00:00:45" startTime="2025-10-21T10:05:00Z" endTime="2025-10-21T10:05:45Z" testType="UnitTest" outcome="Failed" testListId="List2" relativeResultsDirectory="Results/Dir2" />
                       <UnitTestResult executionId="E3" testId="T3" testName="Test C" computerName="Computer3" duration="00:01:00" startTime="2025-10-21T10:10:00Z" endTime="2025-10-21T10:11:00Z" testType="UnitTest" outcome="Passed" testListId="List3" relativeResultsDirectory="Results/Dir3" />
                     </Results>
                     """;

        CancellationTokenSource cts = CancellationTokenSourceFactory.GetCancellationTokenSource();
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml));
        ReportModel model = await ReportModel.Parse(stream, cts.Token);

        Assert.Equal(3, model.UnitTestResults.Length);

        // Asserting properties for first test result (Test A)
        Assert.Equal("Test A", model.UnitTestResults[0].TestName);
        Assert.Equal("Passed", model.UnitTestResults[0].Outcome);
        Assert.Equal("E1", model.UnitTestResults[0].ExecutionId);
        Assert.Equal("T1", model.UnitTestResults[0].TestId);
        Assert.Equal("Computer1", model.UnitTestResults[0].ComputerName);
        Assert.Equal(TimeSpan.Parse("00:00:30"), model.UnitTestResults[0].Duration);
        Assert.Equal(DateTime.Parse("2025-10-21T10:00:00Z"), model.UnitTestResults[0].StartTime);
        Assert.Equal(DateTime.Parse("2025-10-21T10:00:30Z"), model.UnitTestResults[0].EndTime);
        Assert.Equal("UnitTest", model.UnitTestResults[0].TestType);
        Assert.Equal("List1", model.UnitTestResults[0].TestListId);
        Assert.Equal("Results/Dir1", model.UnitTestResults[0].RelativeResultsDirectory);

        // Asserting properties for second test result (Test B)
        Assert.Equal("Test B", model.UnitTestResults[1].TestName);
        Assert.Equal("Failed", model.UnitTestResults[1].Outcome);
        Assert.Equal("E2", model.UnitTestResults[1].ExecutionId);
        Assert.Equal("T2", model.UnitTestResults[1].TestId);
        Assert.Equal("Computer2", model.UnitTestResults[1].ComputerName);
        Assert.Equal(TimeSpan.Parse("00:00:45"), model.UnitTestResults[1].Duration);
        Assert.Equal(DateTime.Parse("2025-10-21T10:05:00Z"), model.UnitTestResults[1].StartTime);
        Assert.Equal(DateTime.Parse("2025-10-21T10:05:45Z"), model.UnitTestResults[1].EndTime);
        Assert.Equal("UnitTest", model.UnitTestResults[1].TestType);
        Assert.Equal("List2", model.UnitTestResults[1].TestListId);
        Assert.Equal("Results/Dir2", model.UnitTestResults[1].RelativeResultsDirectory);

        // Asserting properties for third test result (Test C)
        Assert.Equal("Test C", model.UnitTestResults[2].TestName);
        Assert.Equal("Passed", model.UnitTestResults[2].Outcome);
        Assert.Equal("E3", model.UnitTestResults[2].ExecutionId);
        Assert.Equal("T3", model.UnitTestResults[2].TestId);
        Assert.Equal("Computer3", model.UnitTestResults[2].ComputerName);
        Assert.Equal(TimeSpan.Parse("00:01:00"), model.UnitTestResults[2].Duration);
        Assert.Equal(DateTime.Parse("2025-10-21T10:10:00Z"), model.UnitTestResults[2].StartTime);
        Assert.Equal(DateTime.Parse("2025-10-21T10:11:00Z"), model.UnitTestResults[2].EndTime);
        Assert.Equal("UnitTest", model.UnitTestResults[2].TestType);
        Assert.Equal("List3", model.UnitTestResults[2].TestListId);
        Assert.Equal("Results/Dir3", model.UnitTestResults[2].RelativeResultsDirectory);
    }
}