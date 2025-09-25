// // Description :    Definition of UnitTest.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

using System;
using System.Xml.Linq;

namespace Trxlog2Html.ReportModels;

public record UnitTest {
    public string Name { get; }
    public string Storage { get; }
    public string Id { get; }

    public UnitTest(string name, string storage, string id) {
        Name = name;
        Storage = storage;
        Id = id;
    }

    public static UnitTest Create(XElement element) {
            string name = element.Attribute("name")?.Value ?? string.Empty;
            string storage = element.Attribute("storage")?.Value ?? string.Empty;
            string id = element.Attribute("id")?.Value ?? string.Empty;
            return new UnitTest(name, storage, id);
    }
}