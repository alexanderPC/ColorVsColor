using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorVsColor
{

    public class VM : INotifyPropertyChanged
    {

        public string hexValue;
        public SolidColorBrush currentBrush;
        public Color currentColor;

        public VM()
        {
            HexValue = "ffffffff";
            assingColor();
            currentColor = currentBrush.Color;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private SolidColorBrush CurrentBrush
        {
            get
            {
                return currentBrush;
            }
            set
            {
                currentBrush = value;
                NotifyPropertyChanged();
            }
        }

        private string HexValue
        {
            get
            {
                return hexValue;
            }
            set
            {
                hexValue = value;
                NotifyPropertyChanged();
            }
        }      
        
        private Color CurrentColor
        {
            get
            {
                return currentColor;
            }
            set
            {
                currentColor = value;
                NotifyPropertyChanged();
            }
        }

        private void assingColor()
        {
            currentBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(HexValue);
        }
    }
}