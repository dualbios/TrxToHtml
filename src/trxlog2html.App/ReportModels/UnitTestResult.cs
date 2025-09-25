// // Description :    Definition of UnitTestResult.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace trxlog2html.ReportModels;

public class UnitTestResult {
    public string ExecutionId { get; private set; }
    public string TestId { get; private set; }
    public string TestName { get; private set; }
    public string ComputerName { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string TestType { get; private set; }
    public string Outcome { get; private set; }
    public string TestListId { get; private set; }
    public string RelativeResultsDirectory { get; private set; }

    public static UnitTestResult Create(XElement element) {
        UnitTestResult result = new ();
        result.ExecutionId = element.Attribute("executionId")?.Value ?? string.Empty;
        result.TestId = element.Attribute("testId")?.Value ?? string.Empty;
        result.TestName = element.Attribute("testName")?.Value ?? string.Empty;
        result.ComputerName = element.Attribute("computerName")?.Value ?? string.Empty;
        result.Duration = TimeSpan.Parse(element.Attribute("duration")?.Value ?? "00:00:00");
        result.StartTime = DateTime.Parse(element.Attribute("startTime")?.Value ?? DateTime.MinValue.ToString("o"));
        result.EndTime = DateTime.Parse(element.Attribute("endTime")?.Value ?? DateTime.MinValue.ToString("o"));
        result.TestType = element.Attribute("testType")?.Value ?? string.Empty;
        result.Outcome = element.Attribute("outcome")?.Value ?? string.Empty;
        result.TestListId = element.Attribute("testListId")?.Value ?? string.Empty;
        result.RelativeResultsDirectory = element.Attribute("relativeResultsDirectory")?.Value ?? string.Empty;

        return result;
    }
}