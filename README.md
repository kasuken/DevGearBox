# 🛠️ DevGearbox

<p align="center"> <img src="https://img.shields.io/badge/Platform-Windows-blue" alt="Platform: Windows" /> <img src="https://img.shields.io/badge/Built%20With-Windows%20Forms-lightgrey" alt="Built With: Windows Forms" /> <img src="https://img.shields.io/badge/License-MIT-green" alt="License: MIT" /> <img src="https://img.shields.io/badge/Status-Alpha-orange" alt="Status: Alpha" /> <img src="https://img.shields.io/badge/Offline-Yes-success" alt="Offline: Yes" /> <img src="https://img.shields.io/badge/Contributions-Welcome-brightgreen" alt="Contributions Welcome" /> </p>

**Your personal offline Swiss Army knife for developers**

All offline. No data leaves your machine.

## ✨ Features

- 🧹 Format JSON, XML, and other data types
- 📊 Convert between JSON and CSV formats bidirectionally
- 🔢 Convert numbers between Binary, Octal, Decimal, and Hexadecimal with real-time updates
- ⏲️ Parse and understand CRON expressions with detailed breakdown
- 🔄 Convert between popular developer formats (e.g., Base64 ↔ String, Unix Timestamp ↔ DateTime)
- 🔍 Analyze authentication tokens like JWTs
- 🐛 Debug JWT tokens with split-panel view (header, payload, signature)
- 🎲 Generate GUIDs in all C# formats with one click
- 📝 Generate Lorem Ipsum placeholder text of different lengths using Bogus
- ✂️ Transform and manipulate text quickly (case conversions, URL encoding, etc.)
- 🔐 Generate cryptographic hashes (MD5, SHA-1, SHA-256, SHA-512)
- 🔎 Test and debug regular expressions with real-time feedback
- 🎨 Convert colors between HEX, RGB, HSL, HSB, HWB, and CMYK formats
- 🔒 All offline. No data leaves your machine
- ⚡ Lightweight and blazing fast

## Detailed Features

### 🔄 Encoders & Decoders
- **Base64 Encoder/Decoder**: Convert text to and from Base64 encoding
  - Encode any text to Base64
  - Decode Base64 strings back to plain text
  - Error handling for invalid Base64 input

### 🧹 Formatters
- **JSON Formatter**
  - Format (pretty-print) JSON with proper indentation
  - Minify JSON to remove all whitespace
  - Validation with clear error messages

- **XML Formatter**
  - Format (pretty-print) XML with proper indentation
  - Minify XML to remove all whitespace
  - Validation with clear error messages

### 📊 JSON ↔ CSV Converter
- **Bidirectional Conversion** - Convert in both directions
  - **JSON to CSV**: Convert JSON arrays to CSV format
    - Automatically extracts headers from JSON properties
    - Handles nested objects (flattened)
    - Proper CSV escaping (commas, quotes, newlines)
    - Alphabetically sorted columns
  - **CSV to JSON**: Convert CSV with headers to JSON array
    - First row used as property names
    - Automatic type detection (numbers, booleans, strings)
    - Pretty-printed JSON output
    - Handles quoted CSV values
  - **Split-panel interface** - Input on left, output on right
  - **Swap button** - Quickly reverse conversion
  - **Copy output** - One-click clipboard copy
  - **Clear error messages** - Helpful validation feedback

### 🔍 JWT Analyzer
- **JWT Token Analyzer**
  - Decode JWT tokens without validation
  - Display header information
  - Display payload/claims
  - Show token metadata (issuer, audience, expiration)
  - Check if token is expired
  - No network required - completely offline

### 🐛 JWT Debugger
- **JWT Token Debugger** - Split-panel debugging interface
  - **Left Panel:** Paste JWT token
  - **Right Panel:** See decoded parts
    - **Header:** Algorithm, token type, and metadata
    - **Payload:** Claims and user data
    - **Signature:** Base64Url encoded signature
  - Real-time decoding as you type
  - Copy individual sections to clipboard
  - Clear error messages for invalid tokens
  - Perfect for debugging authentication issues

### ⏰ Timestamp Converter
- **Unix Timestamp to DateTime**
  - Supports both seconds and milliseconds
  - Shows both UTC and local time
  - Automatic detection of timestamp format

- **DateTime to Unix Timestamp**
  - Interactive date and time picker
  - Returns both seconds and milliseconds
  - Easy-to-use interface

### ✂️ Text Transformer
Multiple text transformation tools:
- **UPPER CASE** - Convert text to uppercase
- **lower case** - Convert text to lowercase
- **PascalCase** - Convert to PascalCase (e.g., "hello world" → "HelloWorld")
- **camelCase** - Convert to camelCase (e.g., "hello world" → "helloWorld")
- **snake_case** - Convert to snake_case (e.g., "Hello World" → "hello_world")
- **kebab-case** - Convert to kebab-case (e.g., "Hello World" → "hello-world")
- **Reverse** - Reverse the text
- **Remove Whitespace** - Remove all whitespace characters
- **URL Encode** - Encode text for URLs
- **URL Decode** - Decode URL-encoded text

### 🔐 Hash Generator
Generate cryptographic hashes from text:
- **MD5** - Generate MD5 hash (not recommended for security)
- **SHA-1** - Generate SHA-1 hash (not recommended for security)
- **SHA-256** - Generate SHA-256 hash (secure)
- **SHA-512** - Generate SHA-512 hash (secure)
- One-click hash generation
- Lowercase hex output

### 🎨 Color Converter
Convert colors between multiple formats:
- **Color Picker** - RGB sliders with alpha channel control
- **HEX Input** - Enter HEX color codes directly (#RGB, #RRGGBB, #AARRGGBB)
- **Visual Preview** - See your color in real-time
- **Format Outputs:**
  - **HEX** - Standard hex format (#RRGGBB)
  - **HEX Alpha** - Hex with alpha channel (#AARRGGBB)
  - **RGB** - Red, Green, Blue (rgb(r, g, b))
  - **RGBA** - RGB with alpha (rgba(r, g, b, a))
  - **HSL** - Hue, Saturation, Lightness (hsl(h, s%, l%))
  - **HSLA** - HSL with alpha (hsla(h, s%, l%, a))
  - **HSB/HSV** - Hue, Saturation, Brightness (hsb(h, s%, b%))
  - **HWB** - Hue, Whiteness, Blackness (hwb(h, w%, b%))
  - **CMYK** - Cyan, Magenta, Yellow, Black (cmyk(c%, m%, y%, k%))
- **Quick Presets** - Common colors (red, green, blue, etc.)
- **Copy to Clipboard** - One-click copy for any format

### 🎲 GUID Generator
Generate GUIDs in all C# formats:
- **One-Click Generation** - Generate all formats with a single button click
- **All C# GUID Formats:**
  - **Format N** - 32 digits (no hyphens): `00000000000000000000000000000000`
  - **Format D** - Hyphens (default): `00000000-0000-0000-0000-000000000000`
  - **Format B** - Braces: `{00000000-0000-0000-0000-000000000000}`
  - **Format P** - Parentheses: `(00000000-0000-0000-0000-000000000000)`
  - **Format X** - Hexadecimal: `{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}`
  - **Base64** - Base64 encoded format
  - **Uppercase variants** - N and D in uppercase
- **Quick Copy Buttons** - Generate and copy specific format instantly
- **Copy All** - Copy all formats to clipboard at once
- **Format Labels** - Each GUID shows its format type name

### 📝 Lorem Ipsum Generator
Generate placeholder text using the Bogus library:
- **Predefined Lengths:**
  - **Short** - 1 paragraph
  - **Medium** - 3 paragraphs
  - **Long** - 5 paragraphs
  - **Very Long** - 10 paragraphs
  - **Custom** - Specify any number of paragraphs
- **Dropdown Selection** - Easy length selection from dropdown
- **Custom Count** - Enter custom paragraph count when "Custom" is selected
- **One-Click Generation** - Generate with a single button click
- **Copy to Clipboard** - Quick copy generated text
- **Clean Output** - Professional Lorem Ipsum text using Bogus library
- **Perfect for:**
  - Design mockups
  - Wireframes
  - Layout testing
  - Content placeholders

### 🔢 Number Base Converter
Real-time number base conversion:
- **4 Number Systems:**
  - **Base 2 (Binary)** - Binary representation (0s and 1s)
  - **Base 8 (Octal)** - Octal representation (0-7)
  - **Base 10 (Decimal)** - Standard decimal numbers
  - **Base 16 (Hexadecimal)** - Hex representation (0-9, A-F)
- **Real-Time Conversion** - Type in any field, others update automatically
- **Bidirectional** - Convert from any base to all others instantly
- **Error Handling** - Clear validation messages for invalid input
- **Large Numbers** - Supports up to 64-bit integers
- **Hex Prefix Support** - Accepts "0x" prefix for hexadecimal

### 🔗 URL Parser
Parse and analyze URLs with detailed breakdown:
- **Split-Panel Interface** - URL input on top, parsed results below
  - **Left Panel:** URL Components (scheme, host, port, path, query, fragment)
  - **Right Panel:** Query Parameters (key-value pairs)
- **Automatic Parsing** - Real-time parsing as you type
- **URL Components Extracted:**
  - **Scheme** - Protocol (http, https, ftp, etc.)
  - **Host** - Domain name or IP address
  - **Port** - Port number
  - **Path** - URL path
  - **Query String** - Full query string
  - **Fragment** - URL fragment/anchor
- **Query Parameter Extraction** - All parameters listed separately with keys and values
- **URL Decoding** - Automatically decodes URL-encoded characters
- **Copy Functions** - Copy components or parameters separately
- **Error Handling** - Clear messages for invalid URLs

### ⏲️ Cron Parser
Parse and understand CRON expressions:
- **CRON Field Breakdown:**
  - **Minute** (0-59) - With explanation
  - **Hour** (0-23) - With explanation
  - **Day of Month** (1-31) - With explanation
  - **Month** (1-12) - With explanation
  - **Day of Week** (0-6, Sunday=0) - With explanation
- **Human-Readable Description** - Convert CRON to plain English
- **Next Run Times** - See upcoming scheduled executions (estimated)
- **Real-Time Parsing** - Auto-parse as you type
- **Example Buttons** - Quick examples (Daily, Every 15 min, Weekdays 9AM, Monthly)
- **Special Characters Support:**
  - `*` (asterisk) - Every value
  - `?` (question mark) - No specific value
  - `/` (slash) - Step values (e.g., */15 = every 15)
  - `-` (dash) - Ranges (e.g., 1-5 = Monday to Friday)
  - `,` (comma) - Multiple values (e.g., 1,3,5)

## 🔒 Privacy & Security

**All operations are performed locally. No data leaves your machine.**

- No internet connection required
- No telemetry or analytics
- No data collection
- No external API calls
- 100% offline functionality

## ⚡ Technical Details

- **Platform**: Windows WPF (.NET 10.0)
- **Language**: C#
- **Dependencies**:
  - Newtonsoft.Json (for JSON parsing)
  - System.IdentityModel.Tokens.Jwt (for JWT decoding)

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK or runtime
- Windows OS

### Building from Source
```bash
dotnet restore
dotnet build
```

### Running
```bash
dotnet run --project DevGearbox
```

Or simply run the executable from:
```
DevGearbox\bin\Debug\net10.0-windows\DevGearbox.exe
```

## 📝 Usage Examples

### JSON Formatting
1. Go to the "🧹 Formatters" tab
2. Paste your JSON in the input box
3. Click "Format (Pretty)" for formatted output or "Minify" for compact output

### JWT Analysis
1. Go to the "🔍 JWT Analyzer" tab
2. Paste your JWT token
3. Click "Analyze JWT"
4. View the decoded header, payload, and token information

### Text Transformations
1. Go to the "✂️ Text Tools" tab
2. Enter your text
3. Click any transformation button to see the result instantly

## 🎨 UI/UX

- Modern dark theme (VS Code inspired)
- Clean, intuitive interface
- Tab-based navigation
- Monospace font (Consolas) for code/data display
- Responsive layout

## 📄 License

This project is open source and available for personal and commercial use.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

## 🌟 Acknowledgments

Built with ❤️ for developers who value privacy and offline tools.

---

**Remember:** 🔒 All operations are performed locally. No data leaves your machine.

