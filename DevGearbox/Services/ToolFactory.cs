﻿﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.ObjectModel;
using DevGearbox.Components;
using DevGearbox.Models;
namespace DevGearbox.Services;
public class ToolFactory
{
    private static ToolFactory? _instance;
    public static ToolFactory Instance => _instance ??= new ToolFactory();
    private ToolFactory() { }
    public ObservableCollection<ToolItem> GetAllTools()
    {
        return new ObservableCollection<ToolItem>
        {
            new ToolItem(
                "Encoders",
                "🔄",
                "Base64 encoding and decoding",
                new Base64EncoderView()
            ),
            new ToolItem(
                "Formatters",
                "🧹",
                "JSON and XML formatting",
                new FormattersView()
            ),
            new ToolItem(
                "JSON ↔ CSV",
                "📊",
                "Convert between JSON and CSV formats",
                new JsonCsvConverterView()
            ),
            new ToolItem(
                "JWT Analyzer",
                "🔍",
                "Decode and analyze JWT tokens",
                new JwtAnalyzerView()
            ),
            new ToolItem(
                "JWT Debugger",
                "🐛",
                "Debug JWT tokens - view header, payload, and signature",
                new JwtDebuggerView()
            ),
            new ToolItem(
                "Timestamp",
                "⏰",
                "Unix timestamp conversions",
                new TimestampConverterView()
            ),
            new ToolItem(
                "Text Tools",
                "✂️",
                "Text transformation utilities",
                new TextTransformerView()
            ),
            new ToolItem(
                "Hash",
                "🔐",
                "Cryptographic hash generation",
                new HashGeneratorView()
            ),
            new ToolItem(
                "RegEx Tester",
                "🔎",
                "Test regular expressions and see matches",
                new RegexTesterView()
            ),
            new ToolItem(
                "Color Converter",
                "🎨",
                "Convert colors between HEX, RGB, HSL, HSB, HWB, CMYK formats",
                new ColorConverterView()
            ),
            new ToolItem(
                "GUID Generator",
                "🎲",
                "Generate GUIDs in all C# formats",
                new GuidGeneratorView()
            ),
            new ToolItem(
                "Number Base Converter",
                "🔢",
                "Convert numbers between Binary, Octal, Decimal, and Hexadecimal",
                new NumberBaseConverterView()
            ),
            new ToolItem(
                "URL Parser",
                "🔗",
                "Parse URLs and extract all components and query parameters",
                new UrlParserView()
            ),
            new ToolItem(
                "File Encoding",
                "📂",
                "Trascina un file e scopri rapidamente il suo encoding",
                new FileEncodingDetectorView()
            ),
            new ToolItem(
                "Cron Parser",
                "⏲️",
                "Parse and understand CRON expressions with detailed breakdown",
                new CronParserView()
            ),
            new ToolItem(
                "Lorem Ipsum Generator",
                "📝",
                "Generate placeholder text of different lengths using Bogus",
                new LoremIpsumGeneratorView()
            )
        };
    }
}
