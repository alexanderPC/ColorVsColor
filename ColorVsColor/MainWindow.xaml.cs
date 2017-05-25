using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;


namespace ColorVsColor
{    
    public partial class MainWindow : Window
    {
        SolidColorBrush colorBrush;
        SolidColorBrush colorBrush2;
        bool isRecHex = true;
        bool isFormHex = true;
        public MainWindow()
        {
            colorBrush = new SolidColorBrush();
            colorBrush2 = new SolidColorBrush();
            InitializeComponent();            
        }

        private void btnRecDEC_Click(object sender, RoutedEventArgs e)
        {
            DecColorChange(true);
            isRecHex = false;
        }               

        private void butFormDEC_Click(object sender, RoutedEventArgs e)
        {
            DecColorChange(false);
            isRecHex = false;
        }

        private void DecColorChange(bool isRec)
        {
            int decValue = 0;
            if (isRec)
                int.TryParse(txtRec.Text, out decValue);
            else
                int.TryParse(txtForm.Text, out decValue);
            if (decValue > 0)
            {
                var hexValue = Convert.ToString(decValue, 16);
                if (isRec)
                    Click_Slide_Rec(hexValue);
                else
                    Click_Slide_Form(hexValue);
            }
        }

        private void btnRec_Click(object sender, RoutedEventArgs e)
        {
            Click_Slide_Rec(txtRec.Text);
        }

        private void btnForm_Click(object sender, RoutedEventArgs e)
        {
            Click_Slide_Form(txtForm.Text);
        }

        private void sliderForm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (txtForm.Text != "")
            {
                if (isFormHex)
                    Click_Slide_Form(txtForm.Text);
                else
                    DecColorChange(false);
            }
        }

        private void sliderRec_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (txtRec.Text != "")
            {
                if (isRecHex)
                    Click_Slide_Rec(txtRec.Text);
                else
                    DecColorChange(true);
            }
        }

        private void Click_Slide_Rec(string str)
        {
            stringToARGB(str, true);
            ChangeRecColor();
        }

        private void Click_Slide_Form(string str)
        {
            stringToARGB(str, false);
            ChangeFormColor();
        }

        private void ChangeFormColor()
        {
            Form.Background = colorBrush2;
        }

        private void ChangeRecColor()
        {
            ColorOne.Fill = colorBrush;
        }

        private void stringToARGB(string input, bool isRec)
        {
            if (isValidHex(input))
            {
                bool isHex = true;
                string hexValue = "";
                Regex re = new Regex(@"[0][xX][0-9a-fA-F]+");
                Regex re2 = new Regex(@"[#][0-9a-fA-F]+");
                Regex re3 = new Regex(@"[0-9a-fA-F]+");

                if (!re.IsMatch(input) && !re2.IsMatch(input) && !re3.IsMatch(input))
                    isHex = false;
                else
                {
                    if (input.Length > 8)
                        input = input.Substring(input.Length - 6);
                }

                if (isHex)
                {
                    if (re3.IsMatch(input))
                    {
                        hexValue = input;
                    }
                    else if (re.IsMatch(input))
                    {
                        hexValue = input.Substring(2);
                    }
                    else if (re2.IsMatch(input))
                    {
                        hexValue = input.Substring(1);
                    }

                    if (isRec)
                        colorBrush = sixDiggitHex(hexValue, true);
                    else
                        colorBrush2 = sixDiggitHex(hexValue, false);
                }
                else
                {
                    MessageBox.Show("Please enter valid hex number up to 6 diggits!", "Wrong input!", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid hex number up to 6 diggits!", "Wrong input!", MessageBoxButton.OK);
            }
        }

        private SolidColorBrush sixDiggitHex(string hexValue, bool isRec)
        {
            if (isRec)
            {
                var sliderValue = Convert.ToString(Convert.ToInt32(sliderRec.Value), 16);
                while (hexValue.Length < 6)
                    hexValue = "0" + hexValue;
                hexValue = sliderValue + hexValue;
            }
            else
            {
                var sliderValue = Convert.ToString(Convert.ToInt32(sliderForm.Value), 16);
                while (hexValue.Length < 6)
                    hexValue = "0" + hexValue;
                hexValue = sliderValue + hexValue;
            }

            var argb = new ARGB(hexValue);
            return new SolidColorBrush(Color.FromArgb(argb.A, argb.R, argb.G, argb.B));
        }        

        private bool isValidHex(string hex)
        {
            bool isValid = true;
            foreach(var c in hex)
            {
                if (!Char.IsDigit(c) && c != 'a' && c != 'b' && c != 'c' && c != 'd' && c != 'e' && c != 'f'
                    && c != 'A' && c != 'B' && c != 'C' && c != 'D' && c != 'E' && c != 'F'
                    && c != 'x' && c != 'X' && c != '#')
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }        
    }
}
