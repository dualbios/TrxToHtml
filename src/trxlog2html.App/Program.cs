using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommandLine;
using RazorEngine;
using RazorEngine.Templating;
using trxlog2html;
using Encoding = System.Text.Encoding;

namespace Trxlog2Html;

public class Program {

    private static async Task Main(string[] args) {
        ParserResult<Options> arguments = Parser.Default.ParseArguments<Options>(args);

        if (arguments.Errors.Any()) {
            Console.WriteLine("Invalid arguments.");

            foreach (Error error in arguments.Errors) {
                Console.WriteLine(error);
            }

            return;
        }

        string templateFilePath = string.IsNullOrEmpty(arguments.Value.TemplateFilePath)
            ? Path.Combine(GetBuiltInTemplatesDir(), "default-template.cshtml")
            : arguments.Value.TemplateFilePath;
        string template = ReadTemplate(templateFilePath);
        string outputFilePath = string.IsNullOrEmpty(arguments.Value.OutputFilePath)
            ? templateFilePath + ".html"
            : arguments.Value.OutputFilePath;

        Console.WriteLine($"Reading the file {arguments.Value.InputFilePath}...");
        await using FileStream stream = new (arguments.Value.InputFilePath, FileMode.Open, FileAccess.Read);
        XmlSerializer serializer = new (typeof(TestRun));
        Console.WriteLine("Parsing the file...");
        TestRun result = (TestRun)serializer.Deserialize(stream);

        Console.WriteLine($"Generating the report. Use template {templateFilePath}...");
        string html = Engine.Razor.RunCompile(template, "templateKey", typeof(TestRun), result);
        Console.WriteLine($"Writing results to {outputFilePath}...");
        WriteResult(outputFilePath, html);
        Console.WriteLine("Done.");
    }

    private static string GetBuiltInTemplatesDir() {
        return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Templates");
    }


    private static string ReadTemplate(string path) {
        using StreamReader reader = new (path, Encoding.UTF8);
        return reader.ReadToEnd();

    }

    private static void WriteResult(string path, string result) {
        using StreamWriter writer = new (path, false, Encoding.UTF8);
        writer.Write(result);
    }

    public class Options {
        [Option('i', "input", Required = true, HelpText = "Input file path.")]
        public string InputFilePath { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file path.")]
        public string OutputFilePath { get; set; }

        [Option('t', "template", Required = false, HelpText = "Template file path.")]
        public string TemplateFilePath { get; set; }
    }
}