# DevGearbox v1.1.0 - File Encoding Detector Enhancement 🚀

**Your personal offline Swiss Army knife for developers**

## What's New in v1.1.0

This release brings a **major UI/UX overhaul** to the File Encoding Detector tool, making it more powerful and informative than ever!

## ✨ File Encoding Detector - Completely Redesigned

### New Features

**🎨 Modern Two-Column Layout**
- Professional split-panel interface optimized for information density
- Large, intuitive drag-drop zone on the left
- Comprehensive results panel on the right

**📊 Enhanced Detection Results**
- **Prominent Encoding Display** - Large, easy-to-read encoding name in a highlighted card
- **File Information:**
  - Human-readable file size (e.g., "1.23 KB") with exact byte count
  - BOM status with clear ✓ Yes / ✗ No visual indicators
  - Confidence levels: Very High, High, or Low (Fallback)
  - Plain-English analysis explaining the detection method
- **Technical Details** (Expandable section):
  - Complete file metadata (path, size, last modified)
  - Codepage numbers for precise encoding selection
  - BOM byte sequences in hexadecimal
  - Expert recommendations and compatibility notes

**🎯 Better User Experience**
- One-click "Copy Encoding" button for instant clipboard access
- Larger, more responsive drag-and-drop zone
- Better visual feedback with hover effects
- Context-aware status messages
- Detection methods info card
- Scrollable results panel

**⚙️ Enhanced Backend**
- New `EncodingResult` class with rich detection properties
- `DetectEncodingDetailed()` method for comprehensive analysis
- Maintains backward compatibility

### Perfect For:
✅ Debugging encoding issues in cross-platform projects  
✅ Understanding legacy file formats  
✅ Determining correct encoding for data import/export  
✅ Learning about text encodings and BOMs  

## 🛠️ All 15 Tools Still Included:
- 🔄 **Encoders** - Base64 encoding/decoding
- 🧹 **Formatters** - JSON and XML formatting
- 📊 **JSON ↔ CSV** - Bidirectional conversion
- 🔍 **JWT Analyzer** - Decode and analyze tokens
- 🐛 **JWT Debugger** - Split-panel debugging
- ⏰ **Timestamp** - Unix ↔ DateTime conversion
- ✂️ **Text Tools** - Multiple text transformations
- 🔐 **Hash** - MD5, SHA-1, SHA-256, SHA-512
- 🔎 **RegEx Tester** - Pattern testing with real-time feedback
- 🎨 **Color Converter** - HEX, RGB, HSL, HSB, HWB, CMYK
- 🎲 **GUID Generator** - All 9 C# formats
- 🔢 **Number Base** - Binary, Octal, Decimal, Hex converter
- 🔗 **URL Parser** - Extract components and parameters
- ⏲️ **Cron Parser** - Understand CRON expressions
- 📝 **Lorem Ipsum** - Placeholder text generator (Bogus library)
- 📄 **File Encoding Detector** - ⭐ **ENHANCED in v1.1.0!**

## 🔒 Privacy First

- ✅ 100% offline - no internet required
- ✅ No data collection
- ✅ No telemetry
- ✅ All processing is local

## 📦 Installation

### Windows
1. Download `DevGearbox-v1.1.0.zip`
2. Extract to any folder
3. Run `DevGearbox.exe`

### Build from Source
```bash
git clone https://github.com/yourusername/DevGearbox.git
cd DevGearbox
dotnet restore
dotnet build
dotnet run --project DevGearbox
```

## 🛠️ Requirements

- Windows 10 or later
- .NET 10.0 Runtime

## 🎯 Highlights

- **Enhanced File Encoding Detector** - Complete UI/UX overhaul with detailed analysis
- **Modern UI** - Dark theme inspired by VS Code
- **15 Tools** - Comprehensive developer toolkit
- **Zero Config** - Works out of the box
- **Open Source** - Transparent and trustworthy

## 📝 Full Release Notes

See [RELEASE_NOTES.md](RELEASE_NOTES.md) for detailed information about all features, technical details, and more.

## 🙏 Credits

Built with:
- .NET 10 & WPF
- Bogus library for Lorem Ipsum
- Newtonsoft.Json for JSON processing
- System.IdentityModel.Tokens.Jwt for JWT handling

---

**Download:** [DevGearbox-v1.1.0.zip](../../releases/download/v1.1.0/DevGearbox-v1.1.0.zip)

**Star ⭐ this repo if you find it useful!**

## 🔄 Upgrade from v0.0.1

Simply download the new version and replace your existing installation. All tools remain fully compatible.
