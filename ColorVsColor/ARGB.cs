using System;

namespace ColorVsColor
{
    public class ARGB
    {
        //
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public ARGB(string hex)
        {
            if (hex.Length == 8)
                assignValues(hex);
        }
        private void assignValues(string value)
        {
            var a = value.Substring(0, 2);
            var r = value.Substring(2, 2);
            var g = value.Substring(4, 2);
            var b = value.Substring(6, 2);
            A = Convert.ToByte(a, 16);
            R = Convert.ToByte(r, 16);
            G = Convert.ToByte(g, 16);
            B = Convert.ToByte(b, 16);
        }
    }
}
