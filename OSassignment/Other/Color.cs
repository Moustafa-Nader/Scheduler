namespace OSassignment
{
   public class Color
    {
        private byte[] rgb = new byte[3];

        public Color(byte r, byte g, byte b)
        {
            rgb[0] = r;
            rgb[1] = g;
            rgb[2] = b;
        }

        public byte this[int i]
        {
            get { return rgb[i]; }
            set { rgb[i] = value; }
        }

        public void SetRGB(byte r, byte g, byte b)
        {
            rgb = new byte[] { r, g, b };
        }

        public byte[] GetRGB()
        {
            return rgb;
        }
    }
}
