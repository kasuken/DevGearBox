﻿# Changelog

All notable changes to DevGearbox will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2025-12-23

### ✨ Enhanced

#### File Encoding Detector - Major UI/UX Improvements
- **Redesigned Interface** - Modern two-column layout for better information presentation
  - Left panel: Dedicated file selection with larger drag-drop zone
  - Right panel: Comprehensive results display (1.2x width)
- **Enhanced Detection Results:**
  - **Detailed Information Panel** - Shows encoding name, file size, BOM status, confidence level, and analysis
  - **File Size Display** - Human-readable format (KB, MB) with exact byte count
  - **BOM Status Indicator** - Clear ✓ Yes / ✗ No visual feedback
  - **Confidence Levels** - Very High, High, or Low (Fallback) based on detection method
  - **Plain-English Analysis** - User-friendly explanation of how encoding was detected
- **Technical Details Section** (Expandable)
  - Complete file metadata (path, size, last modified timestamp)
  - Codepage numbers for precise encoding selection in code
  - BOM byte sequences displayed in hexadecimal
  - Recommendations and compatibility notes
- **New Backend Features:**
  - Added `EncodingResult` class with comprehensive detection properties
  - New `DetectEncodingDetailed()` method providing rich analysis data
  - Backward compatible - existing methods unchanged
- **User Experience Improvements:**
  - Copy to Clipboard button for quick encoding name copying
  - Better visual hierarchy with card-based sections
  - Larger, more prominent drag-and-drop zone with improved hover effects
  - Context-aware status messages with appropriate severity levels
  - Detection methods info card explaining BOM, heuristic, and fallback detection
- **Perfect for developers** needing to debug encoding issues or understand file formats

## [0.0.1] - 2025-12-22

### 🎉 Initial Release

First public release of DevGearbox - Your personal offline Swiss Army knife for developers!

### ✨ Added

#### Core Tools (15 Total)
- **Encoders & Decoders** - Base64 encoding/decoding with validation
- **Formatters** - JSON and XML formatting with pretty-print and minify
- **JSON ↔ CSV Converter** - Bidirectional conversion with auto type detection
- **JWT Analyzer** - Decode and analyze JWT tokens offline
- **JWT Debugger** - Split-panel JWT debugging interface
- **Timestamp Converter** - Unix timestamp ↔ DateTime conversion
- **Text Tools** - Multiple text transformations (case, URL, HTML, Base64, etc.)
- **Hash Generator** - MD5, SHA-1, SHA-256, SHA-512 hash generation
- **RegEx Tester** - Real-time regular expression testing
- **Color Converter** - Convert between HEX, RGB, HSL, HSB, HWB, CMYK formats
- **GUID Generator** - Generate GUIDs in all 9 C# formats
- **Number Base Converter** - Real-time conversion between Binary, Octal, Decimal, Hex
- **URL Parser** - Parse URLs and extract components and parameters
- **Cron Parser** - Parse and understand CRON expressions
- **Lorem Ipsum Generator** - Generate placeholder text using Bogus library

#### Features
- Modern dark theme UI inspired by VS Code
- Tabbed navigation for quick tool access
- Copy to clipboard functionality throughout
- Real-time parsing and conversion
- Status messages and error handling
- Professional, intuitive interface
- 100% offline operation - no data leaves your machine

#### Technical
- Built with .NET 10 and WPF
- Component-based architecture with dynamic tool loading
- Integrated NuGet packages:
  - System.IdentityModel.Tokens.Jwt (8.2.1)
  - Newtonsoft.Json (13.0.3)
  - Bogus (35.6.5)

### 🔒 Security
- All operations performed locally
- No internet connection required
- No telemetry or analytics
- No data collection
- Zero external API calls

### 📝 Documentation
- Complete README with feature descriptions
- Individual feature documentation files
- Code examples and use cases
- Installation and build instructions

### 🎨 UI/UX
- Clean, professional dark theme
- Consistent color scheme throughout
- Responsive layout
- Status bars with clear feedback
- Copy buttons for quick clipboard access
- Organized component layouts

---

## [Unreleased]

### Planned Features
- SQL Formatter
- Diff Viewer
- Certificate Decoder
- Dark/Light theme toggle
- Export to file functionality
- Keyboard shortcuts
- Cross-platform support (macOS, Linux)

---

[0.0.1]: https://github.com/yourusername/DevGearbox/releases/tag/v0.0.1

