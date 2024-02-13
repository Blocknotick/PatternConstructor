using PatternConstructor.ViewModels;

namespace PatternConstructor.Data
{
    public class SunSkirtPattern : Pattern
    {
        double skirtlength; double waist; bool isLecal; double beltwitdth; int degrees;
        bool hasButtons;
        string skirtType = "Солнце";
        string skirtName = "";

        public SunSkirtPattern(double _skirtlength, double _waist, double _beltwitdth, bool _isLecal, int _degrees, bool _hasButtons)
        {
            skirtlength = _skirtlength;
            waist = _waist;
            isLecal = _isLecal;
            beltwitdth = _beltwitdth;
            hasButtons = _hasButtons;
            degrees = _degrees; // 180 - полусолнце, 360 - солнце
        }
        public SunSkirtPattern(SkirtConstructModel skirtConstructModel)
        {
            skirtlength = CountLength(skirtConstructModel.SkirtCombinationModel.Length, skirtConstructModel.WaistFloorFrontLength);
            waist = skirtConstructModel.WaistGirth + 2; //припуск на свободу облегания
            isLecal = skirtConstructModel.SkirtCombinationModel.DoubleContour;

            double belt = 0;
            for (int i = 0; i < skirtConstructModel.SkirtCombinationModel.Belts.Length; i++)
            {
                if (skirtConstructModel.SkirtCombinationModel.Belt == skirtConstructModel.SkirtCombinationModel.Belts[i])
                    belt = i + 3;
            }
            beltwitdth = belt;

            hasButtons = skirtConstructModel.SkirtCombinationModel.Clasp == "Пуговицы и молния";

            if (skirtConstructModel.SkirtCombinationModel.Type == "Солнце")
            {
                degrees = 360;
                skirtType = "Sun";
            }
            if (skirtConstructModel.SkirtCombinationModel.Type == "Полусолнце")
            {
                degrees = 180;
                skirtType = "HalfSun";
            }

            
        }
        public override string GenerateContent()
        {
            beltwitdth = beltwitdth * 2;
            double smallradius = waist * 180 / (degrees * Math.PI); // малый радиус юбки
            double bigradius = smallradius + skirtlength;
            double width = Math.Max( 5 + waist, bigradius + 2); // ширина - это обхват талии + 2 на отступ от края + 3 на застежку или большой радиус юбки + 2 на отступ

            double height = 3 + beltwitdth + bigradius;
            if (isLecal)
            {
                width += 2;
                height += 4;
            }

            widthcm = width * pixelsizeincm;
            heightcm = height * pixelsizeincm;

            string s = $"<svg version=\"1.1\" width = \"{(int)width*10}mm\" height = \"{(int)height*10}mm\" xmlns =\"http://www.w3.org/2000/svg\">";

            int beltadd = 0;
            if (hasButtons) beltadd = 3;
            
            if (isLecal)
            {
                s += @$"
                    <path d=""M {(int)pixelsizeincm*2} {(int)pixelsizeincm*2} v {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} v {(int)(pixelsizeincm * (beltwitdth+2))}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)(pixelsizeincm * (waist + 2 + beltadd))} {(int)pixelsizeincm*2} v  {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm * (waist + 3 + beltadd))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm * (beltwitdth+2))}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm*2} {(int)pixelsizeincm*2} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} h {(int)(pixelsizeincm * (waist + beltadd+2))} ""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (beltwitdth + 2))} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (beltwitdth + 3))} h {(int)(pixelsizeincm * (waist + beltadd+2))} ""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth / 2 +1))} h {(int)(pixelsizeincm * (waist + beltadd+2))} ""  stroke-width=""1"" stroke=""black"" stroke-dasharray=""4""/>

                    <path d=""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 2))} v {(int)(pixelsizeincm * skirtlength)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 1))} v {(int)(pixelsizeincm * (skirtlength+2))}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 1))} h {(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 3))} h {(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))} h {(int)(pixelsizeincm * (skirtlength+2))} ""  stroke-width=""3"" stroke=""black""/>


                    <path d=""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 2))} A {(int)(pixelsizeincm * smallradius)} {(int)(pixelsizeincm * smallradius)} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius+1))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 +2))}"" fill-opacity=""0"" stroke-width=""3"" stroke=""black""/>    
                    <path d=""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 1))} A {(int)(pixelsizeincm * (smallradius-1))} {(int)(pixelsizeincm * (smallradius-1))} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))}"" fill-opacity=""0"" stroke-width=""1"" stroke=""black""/>

                    <path d = ""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 2))} A {(int)(pixelsizeincm * bigradius)} {(int)(pixelsizeincm * bigradius)} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius + 1))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))}"" fill-opacity = ""0"" stroke-width = ""3"" stroke = ""black"" />
                    <path d = ""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 3))} A {(int)(pixelsizeincm * (1+bigradius))} {(int)(pixelsizeincm * (1+bigradius))} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius + 2))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 +2))}"" fill-opacity = ""0"" stroke-width = ""1"" stroke = ""black"" />


                    <text x=""{(int)(pixelsizeincm * 3)}"" y=""{(int)(pixelsizeincm * 4)}"" font-size=""32"" font-family=""Verdana"">Belt of the {skirtType} skirt,{"\n"} waist = {waist - 2}, skirt length = {Math.Round(skirtlength, 2)} x1</text>
                    <text x=""{(int)(pixelsizeincm * ((waist + beltadd) / 2 + 2))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth / 2 + 2))}"" font-size=""32"" font-family=""Verdana"">fold</text>
                    <text x=""{(int)(pixelsizeincm * (bigradius / 2 + 2))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 4))}"" font-size=""32"" font-family=""Verdana"">fold</text>
                ";

                if (degrees == 360)
                    s += @$"
<text x=""{(int)(pixelsizeincm * (2 + smallradius +1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3 + 2))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt,{"\n"} waist = {waist - 2},{"\n"} skirt length = {Math.Round(skirtlength, 2)}, {"\n"} x2</text>
                    ";

                if (degrees == 180)
                    s += @$"<text x=""{(int)(pixelsizeincm * (2 + smallradius +1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3 +2))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt,{"\n"} waist = {waist - 2},{"\n"} skirt length = {Math.Round(skirtlength, 2)}, {"\n"} x1</text>";

                if (hasButtons)
                    s += @$"<path d=""M {(int)(pixelsizeincm * (waist + 1 +1))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm*2)}""  stroke-width=""2"" stroke=""black""/>
                <path d=""M {(int)(pixelsizeincm * (waist + 1 +1))} {(int)(pixelsizeincm * (beltwitdth+1))} v  {(int)(pixelsizeincm*2)}""  stroke-width=""2"" stroke=""black""/>";
            }
            else
            {
                s += @$" <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} v {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm * (waist + 1 + beltadd))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (beltwitdth + 1))} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth / 2))} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""1"" stroke=""black"" stroke-dasharray=""4""/>

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius))} v {(int)(pixelsizeincm * skirtlength)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1))} h {(int)(pixelsizeincm * skirtlength)} ""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius))} A {(int)(pixelsizeincm * smallradius)} {(int)(pixelsizeincm * smallradius)} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1))}"" fill-opacity=""0"" stroke-width=""3"" stroke=""black""/>
                    <path d = ""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius))} A {(int)(pixelsizeincm * bigradius)} {(int)(pixelsizeincm * bigradius)} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1))}"" fill-opacity = ""0"" stroke-width = ""3"" stroke = ""black"" />
                    
                    <text x=""{(int)(pixelsizeincm * 2)}"" y=""{(int)(pixelsizeincm * 3)}"" font-size=""32"" font-family=""Verdana"">Belt of the {skirtType} skirt,{"\n"} waist = {waist - 2}, skirt length = {Math.Round(skirtlength, 2)} x1</text>
                    <text x=""{(int)(pixelsizeincm * ((waist + beltadd)/2+1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth / 2+1))}"" font-size=""32"" font-family=""Verdana"">fold</text>
                    <text x=""{(int)(pixelsizeincm * (bigradius / 2 + 1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 2))}"" font-size=""32"" font-family=""Verdana"">fold</text>
                ";

                if (degrees == 360)
                    s += @$"
<text x=""{(int)(pixelsizeincm * (2 + smallradius))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt,{"\n"} waist = {waist - 2},{"\n"} skirt length = {Math.Round(skirtlength, 2)}, {"\n"} x2</text>
                    ";

                if (degrees == 180)
                    s += @$"<text x=""{(int)(pixelsizeincm * (2 + smallradius))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt,{"\n"} waist = {waist - 2},{"\n"} skirt length = {Math.Round(skirtlength, 2)}, {"\n"} x1</text>";
                
                if (hasButtons)
                    s += @$"<path d=""M {(int)(pixelsizeincm * (waist + 1))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm)}""  stroke-width=""2"" stroke=""black""/>
                <path d=""M {(int)(pixelsizeincm * (waist + 1))} {(int)(pixelsizeincm * (beltwitdth))} v  {(int)(pixelsizeincm)}""  stroke-width=""2"" stroke=""black""/>";
            }
            
            s += "</svg>";
            return s;
        }
    }
}
