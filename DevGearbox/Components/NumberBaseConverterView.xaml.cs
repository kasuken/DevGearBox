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
        HandleInput(BinaryInput, "binary", () =>
        {
            OctalInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToOctal(BinaryInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToDecimal(BinaryInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.BinaryConverter.ToHex(BinaryInput.Text);
        }, "Converted from Binary");
    }
    

    private void OctalInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        HandleInput(OctalInput, "octal", () =>
        {
            BinaryInput.Text = Utils.NumberBaseConverter.OctalConverter.ToBinary(OctalInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.OctalConverter.ToDecimal(OctalInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.OctalConverter.ToHex(OctalInput.Text);
        }, "Converted from Octal");
    }
    

    private void DecimalInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        HandleInput(DecimalInput, "decimal", () =>
        {
            BinaryInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToBinary(DecimalInput.Text);
            OctalInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToOctal(DecimalInput.Text);
            HexInput.Text = Utils.NumberBaseConverter.DecimalConverter.ToHex(DecimalInput.Text);
        }, "Converted from Decimal");
    }
    
    private void HexInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        HandleInput(HexInput, "hex", () =>
        {
            BinaryInput.Text = Utils.NumberBaseConverter.HexConverter.ToBinary(HexInput.Text);
            OctalInput.Text = Utils.NumberBaseConverter.HexConverter.ToOctal(HexInput.Text);
            DecimalInput.Text = Utils.NumberBaseConverter.HexConverter.ToDecimal(HexInput.Text);
        }, "Converted from Hexadecimal");
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
        ClearAllExcept(string.Empty);
        ResetStatus();
        _isUpdating = false;
    }

    private void HandleInput(System.Windows.Controls.TextBox source, string exceptKey, Action conversion, string successMessage)
    {
        if (_isUpdating)
        {
            return;
        }

        try
        {
            _isUpdating = true;
            if (string.IsNullOrWhiteSpace(source.Text))
            {
                ClearAllExcept(exceptKey);
                ResetStatus();
                return;
            }

            conversion();
            UpdateStatus(successMessage, InfoBarSeverity.Success);
        }
        catch (Exception ex)
        {
            UpdateStatus($"Error: {ex.Message}", InfoBarSeverity.Error);
        }
        finally
        {
            _isUpdating = false;
        }
    }

    private void ResetStatus()
    {
        UpdateStatus("Ready to convert numbers between bases", InfoBarSeverity.Informational);
    }

    private void UpdateStatus(string title, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = title;
        StatusInfoBar.Severity = severity;
    }
}

