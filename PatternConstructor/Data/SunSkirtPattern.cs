using PatternConstructor.Data.Enum;
using PatternConstructor.ViewModels;

namespace PatternConstructor.Data
{
    public class SunSkirtPattern : Pattern
    {
        double skirtlength; 
        double waist; 
        bool isLecal; 
        double beltwitdth; 
        int degrees; 
        double waistp=2;
        bool hasButtons;
        SkirtType skirtType = SkirtType.Sun;

        public SunSkirtPattern(SunSkirtConstructModel model)
        {
            skirtlength = model.Length;
            waist = model.WaistGirth;
            isLecal = false;
            beltwitdth = 0;
            hasButtons = false;
            degrees = model.Degree; // 180 - полусолнце, 360 - солнце
            waistp = model.WaistP;
        }
        public SunSkirtPattern(SkirtConstructModel skirtConstructModel)
        {
            skirtlength = CountLength(SkirtEnum.lengthDict[skirtConstructModel.SkirtCombinationModel.Length], skirtConstructModel.WaistFloorFrontLength);

            waist = skirtConstructModel.WaistGirth + waistp; //припуск на свободу облегания
            isLecal = skirtConstructModel.SkirtCombinationModel.DoubleContour;

            beltwitdth = (int)SkirtEnum.beltTypeDict[skirtConstructModel.SkirtCombinationModel.Belt];
            hasButtons = SkirtEnum.claspDict[skirtConstructModel.SkirtCombinationModel.Clasp];
            skirtType = SkirtEnum.skirtTypeDict[skirtConstructModel.SkirtCombinationModel.Type];

            switch (skirtType)
            {
                case SkirtType.Pencil:
                    break;
                case SkirtType.Tulip:
                    break;
                case SkirtType.Sun:
                    degrees = 360;
                    break;
                case SkirtType.HalfSun:
                    degrees = 180;
                    skirtType = SkirtType.HalfSun;
                    break;
                default:
                    break;
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
                width += 3;
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
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 1))} v {(int)(pixelsizeincm * (skirtlength+2 +1))}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 1))} h {(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 3))} h {(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm } {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 4))} h {(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))} h {(int)(pixelsizeincm * (skirtlength+2 +1))} ""  stroke-width=""3"" stroke=""black""/>


                    <path d=""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 2))} A {(int)(pixelsizeincm * smallradius)} {(int)(pixelsizeincm * smallradius)} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius+1))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 +2))}"" fill-opacity=""0"" stroke-width=""3"" stroke=""black""/>    
                    <path d=""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius + 1))} A {(int)(pixelsizeincm * (smallradius-1))} {(int)(pixelsizeincm * (smallradius-1))} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))}"" fill-opacity=""0"" stroke-width=""1"" stroke=""black""/>

                    <path d = ""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 2))} A {(int)(pixelsizeincm * bigradius)} {(int)(pixelsizeincm * bigradius)} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius + 1))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))}"" fill-opacity = ""0"" stroke-width = ""3"" stroke = ""black"" />
                    <path d = ""M {(int)pixelsizeincm*2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 3))} A {(int)(pixelsizeincm * (1+bigradius))} {(int)(pixelsizeincm * (1+bigradius))} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius + 2))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 +2))}"" fill-opacity = ""0"" stroke-width = ""1"" stroke = ""black"" />

                    <path d = ""M {(int)pixelsizeincm * 2} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius + 4))} A {(int)(pixelsizeincm * (2 + bigradius))} {(int)(pixelsizeincm * (2 + bigradius))} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius + 3))} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + 2))}"" fill-opacity = ""0"" stroke-width = ""1"" stroke = ""black"" />

                    <text x=""{(int)(pixelsizeincm * 3)}"" y=""{(int)(pixelsizeincm * 4)}"" font-size=""32"" font-family=""Verdana"">Belt of the {skirtType} skirt, waist = {waist - 2}cm, skirt length = {Math.Round(skirtlength, 2)}cm x1, interfacing</text>
                    <text x=""{(int)(pixelsizeincm * ((waist + beltadd) / 2 + 2))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth / 2 + 2))}"" font-size=""32"" font-family=""Verdana"">fold</text>
                    <text x=""{(int)(pixelsizeincm * (bigradius / 2 + 2))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 4))}"" font-size=""32"" font-family=""Verdana"">fold</text>
                ";

                if (degrees == 360)
                    s += @$"
<text x=""{(int)(pixelsizeincm * (2 + smallradius +1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3 + 2))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt, waist = {waist - 2}cm, skirt length = {Math.Round(skirtlength, 2)}cm, x2</text>
                    ";

                if (degrees == 180)
                    s += @$"<text x=""{(int)(pixelsizeincm * (2 + smallradius +1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3 +2))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt,{"\n"} waist = {waist - 2},{"\n"}cm skirt length = {Math.Round(skirtlength, 2)}cm, x1</text>";

                if (hasButtons)
                    s += @$"<path d=""M {(int)(pixelsizeincm * (waist + 1 +1))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm*2)}""  stroke-width=""2"" stroke=""black""/>
                <path d=""M {(int)(pixelsizeincm * (waist + 1 +1))} {(int)(pixelsizeincm * (beltwitdth+1))} v  {(int)(pixelsizeincm*2)}""  stroke-width=""2"" stroke=""black""/>";
            }
            else
            {
                if (beltwitdth!=0)
                s += $@"<path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} v {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm * (waist + 1 + beltadd))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm * beltwitdth)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)pixelsizeincm} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (beltwitdth + 1))} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth / 2))} h {(int)(pixelsizeincm * (waist + beltadd))} ""  stroke-width=""1"" stroke=""black"" stroke-dasharray=""4""/><text x=""{(int)(pixelsizeincm * 2)}"" y=""{(int)(pixelsizeincm * 3)}"" font-size=""32"" font-family=""Verdana"">Belt of the {skirtType} skirt, waist = {waist - 2}cm, skirt length = {Math.Round(skirtlength, 2)}cm x1, interfacing</text>
                    <text x=""{(int)(pixelsizeincm * ((waist + beltadd) / 2 + 1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth / 2 + 1))}"" font-size=""32"" font-family=""Verdana"">fold</text>
          
                ";
                s += @$" 

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius))} v {(int)(pixelsizeincm * skirtlength)}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1))} h {(int)(pixelsizeincm * skirtlength)} ""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + smallradius))} A {(int)(pixelsizeincm * smallradius)} {(int)(pixelsizeincm * smallradius)} 90 0 0 {(int)(pixelsizeincm * (1 + smallradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1))}"" fill-opacity=""0"" stroke-width=""3"" stroke=""black""/>
                    <path d = ""M {(int)pixelsizeincm} {(int)(pixelsizeincm * (1 + beltwitdth + 1 + bigradius))} A {(int)(pixelsizeincm * bigradius)} {(int)(pixelsizeincm * bigradius)} 90 0 0 {(int)(pixelsizeincm * (1 + bigradius))} {(int)(pixelsizeincm * (1 + beltwitdth + 1))}"" fill-opacity = ""0"" stroke-width = ""3"" stroke = ""black"" />
                    
                ";

                if (degrees == 360)
                    s += @$"
                    <text x=""{(int)(pixelsizeincm * (2 + smallradius))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt, waist = {waist}cm, skirt length = {Math.Round(skirtlength, 2)}cm, x2</text>
                    <text x=""{(int)(pixelsizeincm * (bigradius / 2 + 1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 2))}"" font-size=""32"" font-family=""Verdana"">fold</text>      
                    ";

                if (degrees == 180)
                    s += @$"
                    <text x=""{(int)(pixelsizeincm * (2 + smallradius))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 3))}"" font-size=""32"" font-family=""Verdana"">Width of the {skirtType} skirt,{"\n"} waist = {waist - 2}cm, skirt length = {Math.Round(skirtlength, 2)}cm, x1</text>
                    <text x=""{(int)(pixelsizeincm * (bigradius / 2 + 1))}"" y=""{(int)(pixelsizeincm * (1 + beltwitdth + 2))}"" font-size=""32"" font-family=""Verdana"">fold</text>      
                    ";

                
                if (hasButtons)
                    s += @$"<path d=""M {(int)(pixelsizeincm * (waist + 1))} {(int)pixelsizeincm} v  {(int)(pixelsizeincm)}""  stroke-width=""2"" stroke=""black""/>
                <path d=""M {(int)(pixelsizeincm * (waist + 1))} {(int)(pixelsizeincm * (beltwitdth))} v  {(int)(pixelsizeincm)}""  stroke-width=""2"" stroke=""black""/>";
            }
            
            s += "</svg>";
            return s;
        }
    }
}
