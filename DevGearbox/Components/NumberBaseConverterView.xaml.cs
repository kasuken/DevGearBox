using System;
using System.Windows;
using System.Windows.Controls;
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
                StatusText.Text = "Ready to convert numbers between bases";
                return;
            }
            OctalInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToOctal(BinaryInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToDecimal(BinaryInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToHex(BinaryInput.Text);
            StatusText.Text = "Converted from Binary";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error: {ex.Message}";
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
                StatusText.Text = "Ready to convert numbers between bases";
                return;
            }
            BinaryInput.Text = Utils.NumberBaseConverter.OctalConverter.ToBinary(OctalInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.OctalConverter.ToDecimal(OctalInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.OctalConverter.ToHex(OctalInput.Text);
            StatusText.Text = "Converted from Octal";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error: {ex.Message}";
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
                StatusText.Text = "Ready to convert numbers between bases";
                return;
            }
            BinaryInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToBinary(DecimalInput.Text);
            OctalInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToOctal(DecimalInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToHex(DecimalInput.Text);
            StatusText.Text = "Converted from Decimal";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error: {ex.Message}";
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
                StatusText.Text = "Ready to convert numbers between bases";
                return;
            }
            BinaryInput.Text = Utils.NumberBaseConverter.HexConverter.ToBinary(HexInput.Text);
            OctalInput.Text = Utils.NumberBaseConverter.HexConverter.ToOctal(HexInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.HexConverter.ToDecimal(HexInput.Text);
            StatusText.Text = "Converted from Hexadecimal";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error: {ex.Message}";
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
        StatusText.Text = "Ready to convert numbers between bases";
        _isUpdating = false;
    }
}
