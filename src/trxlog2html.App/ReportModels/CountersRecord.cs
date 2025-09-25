using System.Xml.Serialization;

namespace trxlog2html.ReportModels;

public record CountersRecord {
    [property: XmlAttribute("total")] 
    public int Total { get; set; }
    [property: XmlAttribute("executed")] 
    public int Executed { get; set; }
    [property: XmlAttribute("passed")] 
    public int Passed { get; set; }
    [property: XmlAttribute("failed")] 
    public int Failed { get; set; }
    [property: XmlAttribute("error")] 
    public int Error { get; set; }
    [property: XmlAttribute("timeout")] 
    public int Timeout { get; set; }
    [property: XmlAttribute("aborted")] 
    public int Aborted { get; set; }
    [property: XmlAttribute("inconclusive")]
    public int Inconclusive { get; set; }
    [property: XmlAttribute("passedButRunAborted")]
    public int PassedButRunAborted { get; set; }
    [property: XmlAttribute("notRunnable")]
    public int NotRunnable { get; set; }
    [property: XmlAttribute("notExecuted")]
    public int NotExecuted { get; set; }
    [property: XmlAttribute("disconnected")]
    public int Disconnected { get; set; }
    [property: XmlAttribute("warning")] 
    public int Warning { get; set; }
    [property: XmlAttribute("completed")] 
    public int Completed { get; set; }
    [property: XmlAttribute("inProgress")] 
    public int InProgress { get; set; }
    [property: XmlAttribute("pending")] 
    public int Pending { get; set; }
}