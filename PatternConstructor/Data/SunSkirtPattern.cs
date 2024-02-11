namespace PatternConstructor.Data
{
    public class SunSkirtPattern
    {
        const double pixelsizeincm = 37.795275591;
        double skirtlength; double waist; bool isLecal; double beltwitdth; int degrees;
        public double width { get; set; }
        public double height { get; set; }
        public SunSkirtPattern(double _skirtlength, double _waist, double _beltwitdth, bool _isLecal, int _degrees)
        {
            skirtlength = _skirtlength;
            waist = _waist;
            isLecal = _isLecal;
            beltwitdth = _beltwitdth;
            degrees = _degrees; // 180 - полусолнце, 360 - солнце
        }
        public SunSkirtPattern()
        {
            skirtlength = 0;
            waist = 0;
            isLecal = false;
            beltwitdth = 0;
            degrees = 0; // 180 - полусолнце, 360 - солнце
        }
        private string generateSize()
        {

            return "";
        }
        public string GenerateContent()
        {
            beltwitdth = beltwitdth * 2;
            double smallradius = waist * 180 / (degrees * Math.PI); // малый радиус юбки
            double bigradius = smallradius + skirtlength;
            width = Math.Max( 5 + waist, bigradius + 2); // ширина - это обхват талии + 2 на отступ от края + 3 на застежку 

            height = 3 + beltwitdth + bigradius;
            if (isLecal)
            {
                width += 2;
                height += 4;
            }

            
            string s = $"<svg version=\"1.1\" width = \"{(int)width*10}mm\" height = \"{(int)height*10}mm\" xmlns =\"http://www.w3.org/2000/svg\">";
            width = width * pixelsizeincm;
            height = height * pixelsizeincm;
            s += @$" <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} v {(int)(pixelsizeincm*beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm * (waist + 4))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} h {(int)(pixelsizeincm * (waist + 3))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (beltwitdth+1))} h {(int)(pixelsizeincm * (waist + 3))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm*(1+ beltwitdth/2))} h {(int)(pixelsizeincm * (waist + 3))} ""  stroke-width=""3"" stroke=""black"" stroke-dasharray=""4""/>

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm*(1+beltwitdth+1+smallradius))} v {(int)(pixelsizeincm*skirtlength)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm*(1+smallradius))} {(int)(pixelsizeincm * ( 1 + beltwitdth + 1))} h {(int)(pixelsizeincm * skirtlength)} ""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm *( 1 + beltwitdth + 1 + smallradius))} A {(int)(pixelsizeincm*smallradius)} {(int)(pixelsizeincm * smallradius)} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm *( 1 + beltwitdth + 1))}"" fill-opacity=""0"" stroke-width=""3"" stroke=""black""/>
                    <path d = ""M {(int)pixelsizeincm} {(int)(pixelsizeincm *( 1 + beltwitdth + 1 + bigradius))} A {(int)(pixelsizeincm * bigradius)} {(int)(pixelsizeincm * bigradius)} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius))} {(int)(pixelsizeincm *( 1 + beltwitdth + 1))}"" fill-opacity = ""0"" stroke-width = ""3"" stroke = ""black"" />
                           ";
            s += "</svg>";
            return s;
        }
    }
}
