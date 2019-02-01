using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Tip_Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SolidColorBrush errorBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private Brush correctBrush = null;
        double percent;
        bool tipTotal;

        public MainPage()
        {
            this.InitializeComponent();
            if (correctBrush == null)
                correctBrush = txtInput.Foreground;
            percent = 18d;
            tipTotal = true;
        }
        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtInput.Foreground = correctBrush;
        }
        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            calcTip();
        }
        private void ddlTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ddl = (ComboBox)sender;
            ComboBoxItem selPercent = (ComboBoxItem)ddl.SelectedItem;
            percent = Convert.ToDouble(selPercent.Content.ToString());
            calcTip();
        }
        private void calcTip()
        {
            //First check if anything has been entered
            if (!String.IsNullOrEmpty(txtInput.Text))
            {
                double billAmount;
                double tip;
                //See if you can cnvert it to Double
                if (!Double.TryParse(txtInput.Text, out billAmount))
                {
                    txtInput.Foreground = errorBrush;
                }
                else if (billAmount < 0)//Check for negative amount
                {
                    txtInput.Foreground = errorBrush;
                }
                else
                {
                    if (tipTotal)
                    {
                        tip = TipCalc.tip(billAmount, percent);
                    }
                    else
                    {
                        tip = TipCalc.tipNoTax(billAmount, percent);
                    }
                    myStoryboard.Begin();
                    lblTipAmount.Text = "Tip: " + tip.ToString("c");
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var option = (RadioButton)sender;
            if (option.Content != null)
            {
                tipTotal = (option.Content.ToString() == "Tip Total Bill");
                calcTip();
            }
        }
    }
}
