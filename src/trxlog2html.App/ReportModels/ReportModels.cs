using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using trxlog2html.ReportModels;

namespace Trxlog2Html.ReportModels {
    public class ReportModel {
        public DateTime StartTime { get; private set; }
        public DateTime FinishTime { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime Queuing { get; private set; }
        
        public TimeSpan Duration { get; private set; }

        public ResultSummary Summary { get; private set; }
        public UnitTest[] UnitTests { get; private set; }
        public UnitTestResult[] UnitTestResults { get; private set; }

        public static async Task<ReportModel> Parse(Stream stream, CancellationToken token) {
            XDocument document = await XDocument.LoadAsync(stream, LoadOptions.None, token);
            ReportModel model = new();
            
            ReadTimes(document, model);
            
            model.Summary = ParseSummary(document);
            model.UnitTests = ParseUnitTests(document);
            model.UnitTestResults = ParseUnitTestResults(document);

            return model;
        }

        private static void ReadTimes(XDocument document, ReportModel model) {
            XNamespace ns = document.Root?.GetDefaultNamespace().NamespaceName;
            XElement testTimesElement = document.Descendants(ns + "Times").FirstOrDefault();

            if (testTimesElement == null) {
                return;
            }
            
            model.StartTime = DateTime.Parse(testTimesElement.Attribute("start")?.Value ?? DateTime.MinValue.ToString("o"));
            model.FinishTime = DateTime.Parse(testTimesElement.Attribute("finish")?.Value ?? DateTime.MinValue.ToString("o"));
            model.Creation = DateTime.Parse(testTimesElement.Attribute("creation")?.Value ?? DateTime.MinValue.ToString("o"));
            model.Queuing = DateTime.Parse(testTimesElement.Attribute("queuing")?.Value ?? DateTime.MinValue.ToString("o"));
            model.Duration = model.FinishTime - model.StartTime;
        }

        private static UnitTestResult[] ParseUnitTestResults(XDocument document) {
            XNamespace ns = document.Root?.GetDefaultNamespace().NamespaceName;
            XElement testResultsElement = document.Descendants(ns + "Results").FirstOrDefault();
            
            if (testResultsElement == null) {
                return [];
            }

            return testResultsElement.Descendants(ns + "UnitTestResult")
                                     .Select(x => {
                                         try {
                                             return UnitTestResult.Create(x);
                                         }
                                         catch (Exception e) {
                                             return null;
                                         }
                                     })
                                     .Where(x => x != null).ToArray();
        }

        private static UnitTest[] ParseUnitTests(XDocument document) {
            XNamespace ns = document.Root?.GetDefaultNamespace().NamespaceName;
            XElement testDefinitionsElement = document.Descendants(ns + "TestDefinitions").FirstOrDefault();

            if (testDefinitionsElement == null) {
                return [];
            }

            return testDefinitionsElement.Descendants(ns + "UnitTest")
                                         .Select(x => {
                                             try {
                                                 return UnitTest.Create(x);
                                             }
                                             catch (Exception e) {
                                                 return null;
                                             }
                                         })
                                         .Where(x => x != null).ToArray();
        }

        private static ResultSummary ParseSummary(XDocument document) {
            XNamespace ns = document.Root?.GetDefaultNamespace().NamespaceName;
            XElement resultSummaryElement = document.Descendants(ns + "ResultSummary").FirstOrDefault();

            if (resultSummaryElement == null) {
                return null;
            }

            var serializer = new XmlSerializer(typeof(ResultSummary));
            using var reader = resultSummaryElement.CreateReader();
            return (ResultSummary)serializer.Deserialize(reader);
        }
    }

    /// <summary>
    /// </summary>
    public class ReportTestClassModel {

        public string ClassName { get; set; }

        public List<ReportTestResultModel> TestResults { get; set; }
    }

    /// <summary>
    /// report test result
    /// </summary>
    public class ReportTestResultModel {
        public string TestMethod { get; set; }

        public string DisplayName { get; set; }

        public string Duration { get; set; }

        public string Outcome { get; set; }

        public string StdOut { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorStackTrace { get; set; }

    }

    /// <summary>
    /// report summary
    /// </summary>
    public class ReportSummaryModel {
        // public string Total { get; set; }
        //
        // public string Executed { get; set; }
        //
        // public string Passed { get; set; }
        //
        // public string Failed { get; set; }
        //
        // public string Error { get; set; }
        //
        // public string Timeout { get; set; }
        //
        // public string Aborted { get; set; }
        //
        // public string Inconclusive { get; set; }
        //
        // public string PassedButRunAborted { get; set; }
        //
        // public string NotRunnable { get; set; }
        //
        // public string NotExecuted { get; set; }
        //
        // public string Disconnected { get; set; }
        //
        // public string Warning { get; set; }
        //
        // public string Completed { get; set; }
        //
        // public string InProgress { get; set; }
        //
        // public string Pending { get; set; }
    }
}