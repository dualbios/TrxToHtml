using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace trxlog2html;

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
[XmlRoot(Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
public class TestRun {
    public TestRunTimes Times { get; set; }

    public TestRunTestSettings TestSettings { get; set; }

    [XmlArrayItem("UnitTestResult", IsNullable = false)]
    public TestRunUnitTestResult[] Results { get; set; }

    [XmlArrayItem("UnitTest", IsNullable = false)]
    public TestRunUnitTest[] TestDefinitions { get; set; }

    [XmlArrayItem("TestEntry", IsNullable = false)]
    public TestRunTestEntry[] TestEntries { get; set; }

    [XmlArrayItem("TestList", IsNullable = false)]
    public TestRunTestList[] TestLists { get; set; }

    public TestRunResultSummary ResultSummary { get; set; }

    [XmlAttribute("id")]
    public string Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("runUser")]
    public string RunUser { get; set; }

    [XmlIgnore]
    public IEnumerable<ResultDefinition> ResultDefinitions => Results.Select(x => new ResultDefinition
    {
        UnitTestResult = x,
        Definition = TestDefinitions.FirstOrDefault(d => d.Id == x.TestId)
    });
}

public class ResultDefinition {
    public TestRunUnitTestResult UnitTestResult { get; set; }
    public TestRunUnitTest Definition { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunTimes {
    [XmlAttribute("creation")]
    public DateTime Creation { get; set; }

    [XmlAttribute("queuing")]
    public DateTime Queuing { get; set; }

    [XmlAttribute("start")]
    public DateTime Start { get; set; }

    [XmlAttribute("finish")]
    public DateTime Finish { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunTestSettings {
    public TestRunTestSettingsDeployment Deployment { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("id")]
    public string Id { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunTestSettingsDeployment {
    [XmlAttribute("runDeploymentRoot")]
    public string RunDeploymentRoot { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunUnitTestResult {
    public TestRunUnitTestResultOutput Output { get; set; }

    [XmlAttribute("executionId")]
    public string ExecutionId { get; set; }

    [XmlAttribute("testId")]
    public string TestId { get; set; }

    [XmlAttribute("testName")]
    public string TestName { get; set; }

    [XmlAttribute("computerName")]
    public string ComputerName { get; set; }


    [XmlIgnore]
    public TimeSpan Duration { get; set; }

    [XmlAttribute("duration")]
    public string DurationString {
        get => Duration.ToString();
        set => Duration = string.IsNullOrEmpty(value) ? TimeSpan.Zero : TimeSpan.Parse(value);
    }

    [XmlAttribute("startTime")]
    public DateTime StartTime { get; set; }

    [XmlAttribute("endTime")]
    public DateTime EndTime { get; set; }

    [XmlAttribute("testType")]
    public string TestType { get; set; }

    [XmlAttribute("outcome")]
    public string Outcome { get; set; }

    [XmlAttribute("testListId")]
    public string TestListId { get; set; }

    [XmlAttribute("relativeResultsDirectory")]
    public string RelativeResultsDirectory { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunUnitTestResultOutput {
    public string StdOut { get; set; }

    public TestRunUnitTestResultOutputErrorInfo ErrorInfo { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunUnitTestResultOutputErrorInfo {
    public string Message { get; set; }

    public string StackTrace { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunUnitTest {
    public TestRunUnitTestExecution Execution { get; set; }

    public TestRunUnitTestTestMethod TestMethod { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("storage")]
    public string Storage { get; set; }

    [XmlAttribute("id")]
    public string Id { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunUnitTestExecution {
    [XmlAttribute("id")]
    public string Id { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunUnitTestTestMethod {
    [XmlAttribute("codeBase")]
    public string CodeBase { get; set; }

    [XmlAttribute("adapterTypeName")]
    public string AdapterTypeName { get; set; }

    [XmlAttribute("className")]
    public string ClassName { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunTestEntry {
    [XmlAttribute("testId")]
    public string TestId { get; set; }

    [XmlAttribute("executionId")]
    public string ExecutionId { get; set; }

    [XmlAttribute("testListId")]
    public string TestListId { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunTestList {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("id")]
    public string Id { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunResultSummary {
    public TestRunResultSummaryCounters Counters { get; set; }

    public TestRunResultSummaryOutput Output { get; set; }

    [XmlArrayItem("RunInfo", IsNullable = false)]
    public TestRunResultSummaryRunInfo[] RunInfos { get; set; }

    [XmlAttribute("outcome")]
    public string Outcome { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunResultSummaryCounters {
    [XmlAttribute("total")]
    public byte Total { get; set; }

    [XmlAttribute("executed")]
    public byte Executed { get; set; }

    [XmlAttribute("passed")]
    public byte Passed { get; set; }

    [XmlAttribute("failed")]
    public byte Failed { get; set; }

    [XmlAttribute("error")]
    public byte Error { get; set; }

    [XmlAttribute("timeout")]
    public byte Timeout { get; set; }

    [XmlAttribute("aborted")]
    public byte Aborted { get; set; }

    [XmlAttribute("inconclusive")]
    public byte Inconclusive { get; set; }

    [XmlAttribute("passedButRunAborted")]
    public byte PassedButRunAborted { get; set; }

    [XmlAttribute("notRunnable")]
    public byte NotRunnable { get; set; }

    [XmlAttribute("notExecuted")]
    public byte NotExecuted { get; set; }

    [XmlAttribute("disconnected")]
    public byte Disconnected { get; set; }

    [XmlAttribute("warning")]
    public byte Warning { get; set; }

    [XmlAttribute("completed")]
    public byte Completed { get; set; }

    [XmlAttribute("inProgress")]
    public byte InProgress { get; set; }

    [XmlAttribute("pending")]
    public byte Pending { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunResultSummaryOutput {
    public string StdOut { get; set; }
}

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public class TestRunResultSummaryRunInfo {
    public string Text { get; set; }

    [XmlAttribute("computerName")]
    public string ComputerName { get; set; }

    [XmlAttribute("outcome")]
    public string Outcome { get; set; }

    [XmlAttribute("timestamp")]
    public DateTime Timestamp { get; set; }
}