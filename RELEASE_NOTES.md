# DevGearbox - Release Notes

## 🚀 v1.2.0 - Windows Cleaner Tool

**Release Date:** January 5, 2026

### 🧼 Windows Cleaner - NEW TOOL

We're excited to introduce a powerful new system maintenance tool that helps you reclaim disk space by safely cleaning temporary files and caches from your Windows machine.

#### ✨ What's New in v1.2.0

**Intelligent System Scanning**
- Automatically scans multiple safe locations for cleanable files:
  - **Windows Temp Folders** - User and system temporary directories
  - **Prefetch Files** - Windows performance optimization cache
  - **Browser Caches** - Chrome, Edge, and Firefox caches (including Code Cache and GPU Cache)
  - **Recycle Bin** - Metadata and orphaned files
- Deep recursive scanning finds files in nested directories
- Smart file detection identifies safe-to-delete items

**Customizable Scan Settings**
- **Settings Expander** with individual checkboxes for each scan location
- Toggle on/off: Windows Temp, Prefetch, Recycle Bin, Browser Caches
- Safe defaults (Recycle Bin disabled by default for user safety)
- Settings persist during your session for consistent scanning

**Interactive Results Grid**
- **Sortable Columns** - Click any column header (Category, Path, Size, Status) to sort
  - Visual sort indicators (▲/▼) show current sort order
  - Smart numeric sorting for file sizes
- **Multi-Select Support** - Choose exactly which items to clean
- **Select All/Deselect All** buttons for quick selection management
- **Real-time Status** - Shows which files can be deleted vs locked/in-use
- **Comprehensive Display:**
  - Category-based organization
  - Full file paths
  - Human-readable file sizes (B, KB, MB, GB, TB)
  - Status indicators ("Ready" or specific errors)

**Safe & Smart Deletion**
- **File Validation** - Only deletes files not currently in use or locked by the system
- **Confirmation Dialog** - Shows item count and total size before cleanup
- **Detailed Results** - Post-cleanup summary with success and error counts
- **Error Handling** - Clear messages for files that couldn't be deleted
- **Zero Risk** - System files and critical data are never touched

**Visual Feedback & Statistics**
- **Summary Panel** displaying:
  - Total files found
  - Total size of cleanable items
  - Selected items count and size
- **Progress Indicators** during scan and cleanup operations
- **Status Messages** guide you through each step
- **Async Operations** prevent UI freezing during long scans
- **Empty State Messages** provide helpful guidance

**Perfect For:**
- 🗂️ Freeing up disk space quickly
- 🧹 Regular system maintenance
- 💻 Cleaning up after development sessions
- 🌐 Removing browser cache buildup
- ⚡ Speeding up your machine

#### 🎯 Key Features

✨ **Smart Scanning** - Finds temporary files across multiple safe locations  
✨ **Customizable** - Control exactly what gets scanned  
✨ **Sortable Grid** - Organize results by category, path, size, or status  
✨ **Multi-Select** - Clean only what you want  
✨ **Safe Deletion** - Skips locked files automatically  
✨ **100% Offline** - All operations performed locally  

---

## 🚀 v1.1.0 - File Encoding Detector Enhancement

**Release Date:** December 23, 2025

### 📄 File Encoding Detector - Complete UI/UX Overhaul

We've completely redesigned the File Encoding Detector with a focus on providing developers with comprehensive, actionable encoding information.

#### ✨ What's New in v1.1.0

**Modern Two-Column Layout**
- Professional split-panel interface optimized for information density
- Left: Large, intuitive drag-drop zone with file selection
- Right: Detailed results panel showing all encoding metadata

**Enhanced Detection Results**
- **Prominent Encoding Display** - Large, easy-to-read encoding name in highlighted card
- **File Information Section:**
  - Human-readable file size (e.g., "1.23 KB") with exact byte count
  - BOM status with clear ✓ Yes / ✗ No visual indicators
  - Confidence levels: Very High, High, or Low (Fallback)
  - Plain-English analysis explaining the detection method
- **Expandable Technical Details:**
  - Complete file metadata (full path, last modified timestamp)
  - Codepage numbers for precise encoding selection
  - BOM byte sequences in hexadecimal format
  - Expert recommendations and compatibility notes

**Improved User Experience**
- One-click "Copy Encoding" button for instant clipboard access
- Larger, more responsive drag-and-drop zone
- Better visual feedback with hover effects
- Context-aware status messages
- Detection methods info card explaining the three detection approaches
- Scrollable results panel for comprehensive information

**Enhanced Backend**
- New `EncodingResult` class with rich detection properties
- `DetectEncodingDetailed()` method provides comprehensive analysis
- Maintains backward compatibility with existing code

**Perfect For:**
- Debugging encoding issues in cross-platform projects
- Understanding legacy file formats
- Determining correct encoding for data import/export
- Learning about text encodings and BOMs

#### 🎯 Key Improvements

✨ **Better UX** - Intuitive two-column layout with comprehensive results  
✨ **More Information** - BOM status, confidence levels, and detailed analysis  
✨ **Technical Details** - Codepage numbers and recommendations for developers  
✨ **One-Click Copy** - Quick clipboard access for encoding names  
✨ **Professional UI** - Modern card-based design with clear visual hierarchy  

---

# DevGearbox v0.0.1 - Initial Release 🎉

**Release Date:** December 22, 2025

## 🎊 Welcome to DevGearbox!

We're excited to announce the **first release** of DevGearbox - your personal offline Swiss Army knife for developers! This initial release includes **15 powerful developer tools**, all running completely offline with no data leaving your machine.

---

## ✨ What's New in v0.0.1

### 🚀 15 Developer Tools Included

DevGearbox v0.0.1 ships with a comprehensive suite of developer utilities:

#### 1. 🔄 **Encoders & Decoders**
- Base64 encoding and decoding
- Support for text to Base64 and Base64 to text conversion
- Error handling for invalid Base64 input

#### 2. 🧹 **Formatters**
- **JSON Formatter:** Pretty-print and minify JSON with validation
- **XML Formatter:** Format and minify XML with validation
- Syntax highlighting and error messages

#### 3. 📊 **JSON ↔ CSV Converter**
- Bidirectional conversion between JSON and CSV formats
- Automatic header extraction and type detection
- Proper CSV escaping for special characters
- Split-panel interface with copy functionality

#### 4. 🔍 **JWT Analyzer**
- Decode JWT tokens without validation
- Display header and payload information
- Show token metadata (issuer, audience, expiration)
- Check if token is expired
- Completely offline - no external API calls

#### 5. 🐛 **JWT Debugger**
- Advanced JWT debugging with split-panel interface
- Separate views for header, payload, and signature
- Syntax-highlighted JSON output
- Copy individual sections to clipboard

#### 6. ⏰ **Timestamp Converter**
- Convert between Unix timestamps and human-readable dates
- Support for both seconds and milliseconds
- Current timestamp generation
- Bidirectional conversion

#### 7. ✂️ **Text Tools**
- **Case Conversions:** UPPERCASE, lowercase, Title Case, camelCase, PascalCase, snake_case, kebab-case
- **URL Encoding/Decoding**
- **HTML Encoding/Decoding**
- **Base64 Encoding/Decoding**
- **Trim & Remove Extra Spaces**
- **Reverse Text**
- Character and word count

#### 8. 🔐 **Hash Generator**
- Generate cryptographic hashes:
  - MD5
  - SHA-1
  - SHA-256
  - SHA-512
- Quick copy buttons for each hash
- Real-time generation as you type

#### 9. 🔎 **RegEx Tester**
- Test regular expressions with real-time feedback
- Pattern matching and validation
- Display all matches with highlighting
- Support for common regex patterns

#### 10. 🎨 **Color Converter**
- Convert between multiple color formats:
  - **HEX** (with alpha)
  - **RGB** and **RGBA**
  - **HSL** and **HSLA**
  - **HSB/HSV**
  - **HWB**
  - **CMYK**
- Visual color picker
- Quick preset colors
- Real-time preview

#### 11. 🎲 **GUID Generator**
- Generate GUIDs in all 9 C# formats:
  - Format N (32 digits, no hyphens)
  - Format D (hyphens, default)
  - Format B (braces)
  - Format P (parentheses)
  - Format X (hexadecimal)
  - Default format
  - Base64 string
  - Uppercase N and D variants
- Individual copy button for each format
- One-click generation of all formats from same GUID

#### 12. 🔢 **Number Base Converter**
- Real-time conversion between:
  - **Binary** (Base 2)
  - **Octal** (Base 8)
  - **Decimal** (Base 10)
  - **Hexadecimal** (Base 16)
- Type in any field, others update automatically
- Support for large numbers (64-bit)
- Hex prefix support (0x)

#### 13. 🔗 **URL Parser**
- Parse URLs and extract all components:
  - Scheme (http, https, ftp, etc.)
  - Host (domain or IP)
  - Port number
  - Path
  - Query string
  - Fragment/anchor
- **Improved UI:**
  - Left panel: Individual textboxes for each component
  - Right panel: Dynamic list of query parameters
  - Dedicated copy button for each parameter value
- Automatic URL decoding
- Real-time parsing

#### 14. ⏲️ **Cron Parser**
- Parse and understand CRON expressions
- Detailed breakdown of all 5 CRON fields:
  - Minute (0-59)
  - Hour (0-23)
  - Day of Month (1-31)
  - Month (1-12)
  - Day of Week (0-6)
- Human-readable description generation
- Next run time estimates
- Example CRON patterns with quick-load buttons
- Support for special characters (*, ?, /, -, ,)

#### 15. 📝 **Lorem Ipsum Generator** ⭐ NEW
- Generate placeholder text using **Bogus library**
- Predefined length options:
  - Short (1 paragraph)
  - Medium (3 paragraphs)
  - Long (5 paragraphs)
  - Very Long (10 paragraphs)
  - Custom (user-specified count)
- Dropdown selection for easy length choice
- One-click generation and copy
- Professional output formatting

---

## 🎨 User Interface

- **Modern Dark Theme** - Easy on the eyes with VS Code-inspired color scheme
- **Tabbed Navigation** - Quick access to all 15 tools
- **Status Feedback** - Clear messages for all operations
- **Copy to Clipboard** - Quick copy buttons throughout
- **Responsive Layout** - Adapts to different window sizes
- **Clean Design** - Professional and intuitive interface

---

## 🔒 Privacy & Security

**All operations are performed locally. No data leaves your machine.**

- ✅ No internet connection required
- ✅ No telemetry or analytics
- ✅ No data collection
- ✅ No external API calls
- ✅ 100% offline functionality
- ✅ Open source and transparent

---

## 🛠️ Technical Details

### Built With
- **.NET 10** - Latest .NET framework
- **WPF** - Windows Presentation Foundation
- **C#** - Modern C# language features
- **XAML** - Declarative UI design

### Dependencies
- **System.IdentityModel.Tokens.Jwt** (v8.2.1) - JWT token handling
- **Newtonsoft.Json** (v13.0.3) - JSON processing
- **Bogus** (v35.6.5) - Lorem Ipsum generation

### System Requirements
- **OS:** Windows 10 or later
- **Framework:** .NET 10.0 Runtime
- **Architecture:** x64 or x86

---

## 📦 Installation

### Option 1: Download Release
1. Download `DevGearbox-v0.0.1.zip` from the [Releases](../../releases) page
2. Extract to your preferred location
3. Run `DevGearbox.exe`

### Option 2: Build from Source
```bash
git clone https://github.com/yourusername/DevGearbox.git
cd DevGearbox
dotnet restore
dotnet build
dotnet run --project DevGearbox
```

---

## 🚀 Getting Started

1. **Launch DevGearbox** - Run `DevGearbox.exe`
2. **Select a Tool** - Click on any tool from the left sidebar
3. **Use the Tool** - Each tool has an intuitive interface with examples
4. **Copy Results** - Use copy buttons to quickly copy outputs

### Quick Examples

**JSON Formatter:**
1. Paste JSON → Click "Format" → Get pretty-printed JSON

**URL Parser:**
1. Enter URL → See components and parameters automatically parsed

**GUID Generator:**
1. Click "Generate New GUID" → Get GUID in all 9 formats → Copy any format

**Lorem Ipsum Generator:**
1. Select length → Click "Generate" → Copy placeholder text

---

## 📊 Statistics

- **15 Tools** - Comprehensive developer toolkit
- **0 Network Calls** - Completely offline
- **100% Privacy** - No data collection
- **Open Source** - Transparent and trustworthy

---

## 🐛 Known Issues

### Minor Issues
- Nullable warnings in build output (cosmetic, doesn't affect functionality)
- URL Parser: Shows nullable warnings for Uri.TryCreate (functionality works correctly)

### Limitations
- CRON Parser: Next run time calculation is simplified (shows estimates for common patterns)
- Number Base Converter: Limited to 64-bit integer range

---

## 🔮 Future Plans

We're already planning exciting features for future releases:

### Under Consideration
- **More Tools:**
  - SQL Formatter
  - Diff Viewer
  - Image Tools (resize, convert)
  - Certificate Decoder
  - API Tester
  
- **Enhancements:**
  - Dark/Light theme toggle
  - Export to file functionality
  - Tool favorites/pinning
  - Recent items history
  - Keyboard shortcuts

- **Platform:**
  - macOS and Linux support via .NET MAUI
  - Cross-platform compatibility

---

## 🤝 Contributing

We welcome contributions! Here's how you can help:

1. **Report Bugs** - Open an issue with details
2. **Suggest Features** - Share your ideas
3. **Submit PRs** - Code contributions welcome
4. **Improve Docs** - Help us document better

### Development Setup
```bash
git clone https://github.com/yourusername/DevGearbox.git
cd DevGearbox
dotnet restore
dotnet build
```

---

## 📄 License

DevGearbox is open source software.

---

## 🙏 Acknowledgments

### Libraries Used
- **Bogus** by Brian Chavez - Lorem Ipsum generation
- **Newtonsoft.Json** - JSON handling
- **System.IdentityModel.Tokens.Jwt** - JWT processing

### Inspiration
- VS Code - UI/UX inspiration
- DevToys - Tool concept inspiration
- Community feedback and suggestions

---

## 📞 Support

- **Issues:** [GitHub Issues](../../issues)
- **Discussions:** [GitHub Discussions](../../discussions)
- **Documentation:** [Wiki](../../wiki)

---

## 🎯 Release Highlights

### What Makes v0.0.1 Special?

✨ **Complete Privacy** - First developer toolkit that's 100% offline  
✨ **15 Tools** - Comprehensive suite in one application  
✨ **Production Ready** - Stable and tested  
✨ **Open Source** - Transparent and trustworthy  
✨ **Modern UI** - Clean, professional interface  
✨ **Zero Config** - Works out of the box  

---

## 📈 Download & Try

Ready to boost your productivity? Download DevGearbox v0.0.1 now!

**[⬇️ Download v0.0.1](../../releases/tag/v0.0.1)**

---

## 🎊 Thank You!

Thank you for trying DevGearbox! We're committed to building the best offline developer toolkit. Your feedback helps us improve.

**Star ⭐ this repo if you find DevGearbox useful!**

---

**Version:** 0.0.1  
**Release Date:** December 22, 2025  
**Build:** Stable  
**Status:** ✅ Production Ready

