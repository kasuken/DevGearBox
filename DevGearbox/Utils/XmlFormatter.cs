using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace DevGearbox.Utils;
public static class XmlFormatter
{
    public static string Format(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return string.Empty;
        try
        {
            var doc = XDocument.Parse(xml);
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = doc.Declaration == null
            };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
        catch (XmlException ex)
        {
            return $"Invalid XML: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    public static string Minify(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return string.Empty;
        try
        {
            var doc = XDocument.Parse(xml);
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = doc.Declaration == null
            };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
        catch (XmlException ex)
        {
            return $"Invalid XML: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
