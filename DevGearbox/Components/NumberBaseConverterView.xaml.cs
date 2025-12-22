using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace DevGearbox.Components;

public partial class NumberBaseConverterView : UserControl
{
    private bool _isUpdating = false;
    
    public NumberBaseConverterView()
    {
        InitializeComponent();
    }
    
    private void BinaryInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdating) return;
        try
        {
            _isUpdating = true;
            if (string.IsNullOrWhiteSpace(BinaryInput.Text))
            {
                ClearAllExcept("binary");
                StatusInfoBar.Title = "Ready to convert numbers between bases";
                StatusInfoBar.Severity = InfoBarSeverity.Informational;
                return;
            }
            OctalInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToOctal(BinaryInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToDecimal(BinaryInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToHex(BinaryInput.Text);
            StatusInfoBar.Title = "Converted from Binary";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
        catch (Exception ex)
        {
            StatusInfoBar.Title = $"Error: {ex.Message}";
            StatusInfoBar.Severity = InfoBarSeverity.Error;
        }
        finally
        {
            _isUpdating = false;
        }
    }
    
    private void OctalInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdating) return;
        try
        {
            _isUpdating = true;
            if (string.IsNullOrWhiteSpace(OctalInput.Text))
            {
                ClearAllExcept("octal");
                StatusInfoBar.Title = "Ready to convert numbers between bases";
                StatusInfoBar.Severity = InfoBarSeverity.Informational;
                return;
            }
            BinaryInput.Text = Utils.NumberBaseConverter.OctalConverter.ToBinary(OctalInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.OctalConverter.ToDecimal(OctalInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.OctalConverter.ToHex(OctalInput.Text);
            StatusInfoBar.Title = "Converted from Octal";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
        catch (Exception ex)
        {
            StatusInfoBar.Title = $"Error: {ex.Message}";
            StatusInfoBar.Severity = InfoBarSeverity.Error;
        }
        finally
        {
            _isUpdating = false;
        }
    }
    
    private void DecimalInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdating) return;
        try
        {
            _isUpdating = true;
            if (string.IsNullOrWhiteSpace(DecimalInput.Text))
            {
                ClearAllExcept("decimal");
                StatusInfoBar.Title = "Ready to convert numbers between bases";
                StatusInfoBar.Severity = InfoBarSeverity.Informational;
                return;
            }
            BinaryInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToBinary(DecimalInput.Text);
            OctalInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToOctal(DecimalInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToHex(DecimalInput.Text);
            StatusInfoBar.Title = "Converted from Decimal";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
        catch (Exception ex)
        {
            StatusInfoBar.Title = $"Error: {ex.Message}";
            StatusInfoBar.Severity = InfoBarSeverity.Error;
        }
        finally
        {
            _isUpdating = false;
        }
    }
    
    private void HexInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdating) return;
        try
        {
            _isUpdating = true;
            if (string.IsNullOrWhiteSpace(HexInput.Text))
            {
                ClearAllExcept("hex");
                StatusInfoBar.Title = "Ready to convert numbers between bases";
                StatusInfoBar.Severity = InfoBarSeverity.Informational;
                return;
            }
            BinaryInput.Text = Utils.NumberBaseConverter.HexConverter.ToBinary(HexInput.Text);
            OctalInput.Text = Utils.NumberBaseConverter.HexConverter.ToOctal(HexInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.HexConverter.ToDecimal(HexInput.Text);
            StatusInfoBar.Title = "Converted from Hexadecimal";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
        catch (Exception ex)
        {
            StatusInfoBar.Title = $"Error: {ex.Message}";
            StatusInfoBar.Severity = InfoBarSeverity.Error;
        }
        finally
        {
            _isUpdating = false;
        }
    }
    
    private void ClearAllExcept(string except)
    {
        if (except != "binary") BinaryInput.Text = "";
        if (except != "octal") OctalInput.Text = "";
        if (except != "decimal") DecimalInput.Text = "";
        if (except != "hex") HexInput.Text = "";
    }
    
    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        _isUpdating = true;
        BinaryInput.Text = "";
        OctalInput.Text = "";
        DecimalInput.Text = "";
        HexInput.Text = "";
        StatusInfoBar.Title = "Ready to convert numbers between bases";
        StatusInfoBar.Severity = InfoBarSeverity.Informational;
        _isUpdating = false;
    }
}
