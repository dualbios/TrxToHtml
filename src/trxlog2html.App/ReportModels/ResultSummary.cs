// // Description :    Definition of ResultSummary.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

using System.Xml.Serialization;

namespace trxlog2html.ReportModels;

[XmlRoot("ResultSummary", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
public record ResultSummary {
    [XmlAttribute("outcome")]
    public string Outcome { get; set; }
    
    [XmlElement("Counters")]
    public CountersRecord Counters { get; set; }
}