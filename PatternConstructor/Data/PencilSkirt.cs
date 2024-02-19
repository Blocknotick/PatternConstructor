using PatternConstructor.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace PatternConstructor.Data
{
    public class PencilSkirt : Pattern
    {
        float skirtlengthFront; float waist; bool isLecal; float beltwitdth;
        float hips; float hipsHeight;
        float skirtlengthSide;
        float skirtlengthBack;
        bool hasButtons;
        string skirtType = "Pencil";

        public PencilSkirt(SkirtConstructModel skirtConstructModel)
        {
            skirtlengthFront = (float)CountLength(skirtConstructModel.SkirtCombinationModel.Length, skirtConstructModel.WaistFloorFrontLength);
            skirtlengthSide = (float)(skirtConstructModel.WaistFloorSideLength - skirtConstructModel.WaistFloorFrontLength + skirtlengthFront);
            skirtlengthBack = (float)(skirtConstructModel.WaistFloorBackLength - skirtConstructModel.WaistFloorFrontLength + skirtlengthFront);

            waist = (float)skirtConstructModel.WaistGirth;
            hips = (float)skirtConstructModel.HipsGirth;
            hipsHeight = (float)skirtConstructModel.HipHeight;

            isLecal = skirtConstructModel.SkirtCombinationModel.DoubleContour;

            double belt = 0;
            for (int i = 0; i < skirtConstructModel.SkirtCombinationModel.Belts.Length; i++)
            {
                if (skirtConstructModel.SkirtCombinationModel.Belt == skirtConstructModel.SkirtCombinationModel.Belts[i])
                    belt = i + 3;
            }
            beltwitdth = (float)belt;

            hasButtons = skirtConstructModel.SkirtCombinationModel.Clasp == "Пуговицы и молния";

            if (skirtConstructModel.SkirtCombinationModel.Type == "Прямая")
            {
                skirtType = "Pencil";
            }
            if (skirtConstructModel.SkirtCombinationModel.Type == "Тюльпан")
            {
                skirtType = "Tulip";
            }
        }

        public override string GenerateContent()
        {
            int beltadd = 0;
            if (hasButtons) beltadd = 3;

            float CH = hips / 2;
            float CW = waist / 2;
            float width = beltwitdth * 2 + 2 + hips / 2 + 1 + 2;
            float height = Math.Max(2 + 1 + skirtlengthSide, waist + 2 + beltadd +2);

            if (isLecal)
            {
                width += 5;
                height += 4;
            }

            widthcm = width * pixelsizeincm;
            heightcm = height * pixelsizeincm;

            string s = $"<svg version=\"1.1\" width = \"{(int)width * 10}mm\" height = \"{(int)height * 10}mm\" xmlns =\"http://www.w3.org/2000/svg\">";

            

            
            List<Vector2> back = new List<Vector2>();
            List<Vector2> front = new List<Vector2>();
            List<Vector2> belt = new List<Vector2>();
            belt.Add(new Vector2(0, 0));
            belt.Add(new Vector2(beltwitdth*2, 0));
            belt.Add(new Vector2(0, waist + 2 + beltadd));
            belt.Add(new Vector2(beltwitdth * 2, waist + 2 + beltadd));
            belt.Add(new Vector2(beltwitdth, 0));
            belt.Add(new Vector2(beltwitdth, waist + 2 + beltadd));

            if (hasButtons)
            {
                belt.Add(new Vector2(0, waist + 2));
                belt.Add(new Vector2(beltwitdth*2, waist + 2));
            }

            back.Add(new Vector2(0, 0)); //T3
            back.Add(new Vector2(0,hipsHeight-1)); //линия бедер Б

            front.Add(new Vector2(CH+2, back[1].Y)); //2 - припуск на свободу облегания

            back.Add(new Vector2((CH+2)/2-1, back[1].Y)); //делим на переднее и заднее полотнище
            front.Add(new Vector2(back[2].X, back[2].Y));

            if (skirtType == "Pencil")
            {
                back.Add(new Vector2(back[2].X,skirtlengthSide));
                front.Add(new Vector2(back[2].X, skirtlengthSide));
            }
            else if (skirtType == "Tulip")
            {
                back.Add(new Vector2(back[2].X-1.5f, skirtlengthSide));
                front.Add(new Vector2(back[2].X + 1.5f, skirtlengthSide));
            }

            back[0] = new Vector2(0, skirtlengthSide - skirtlengthBack); // середина спинки, верх
            front.Add(new Vector2(front[0].X, skirtlengthSide - skirtlengthFront)); // середина переда, верх

            float b = CH + 2 - (CW + 1); // раствор вытачек

            back.Add(new Vector2(back[2].X-b/4,0));
            front.Add(new Vector2(back[2].X + b / 4, 0)); // половина раствора в боковой шов

            back.Add(new Vector2(back[2].X, back[2].Y - 2)); //Б3
            front.Add(new Vector2(back[2].X, back[2].Y - 2)); //Б3

            back.Add(new Vector2(0.4f*back[2].X, back[2].Y)); //Б4

            back.Add(Intersec(back[0], back[4], back[6], new Vector2(back[6].X, back[6].Y + 1))); //Т7
            back.Add(new Vector2(back[7].X-b/6,back[7].Y)); //Т71
            back.Add(new Vector2(back[7].X + b / 6, back[7].Y)); //Т72
            back.Add(new Vector2(back[7].X,back[7].Y+14)); //Б6

            float b1b2 = front[0].X-front[1].X;
            front.Add(new Vector2(front[0].X - 0.4f * b1b2, front[0].Y)); //Б5
            front.Add(Intersec(front[4],front[3],front[6], new Vector2(front[6].X,front[6].Y+1))); //Т8
            front.Add(new Vector2(front[7].X-b/12,front[7].Y)); //Т81
            front.Add(new Vector2(front[7].X + b / 12, front[7].Y)); //Т82
            front.Add(new Vector2(front[7].X, front[7].Y + 14)); //Б7

            back.Add(new Vector2(0, back[3].Y));
            front.Add(new Vector2(front[0].X, back[3].Y));

            Vector2 plus11 = new Vector2(1, 1);
            Vector2 plus10 = new Vector2(1, 0);
            for (int i = 0;i<belt.Count; i++)
            {
                belt[i] += plus11;
                belt[i] = (float)pixelsizeincm * belt[i];
            }
            Vector2 beltoffset = new Vector2(belt[3].X, 0);
            for (int i = 0; i < back.Count; i++)
            {
                back[i] += plus11;
                front[i] += plus11;
                back[i] = (float)pixelsizeincm * back[i];
                back[i] += beltoffset;
                front[i] = (float)pixelsizeincm * front[i];
                front[i] += beltoffset;
            }


            if (isLecal)
            {
                // сдвинуть белт на 1 1 (умноженное на пиксель в сантиметрах)
                // сдвинуть бэк на 3 1
                // сдвинуть фронт на 5 1
            }
            else
            {
                //belt
                s += @$"
                    <path d=""M {(int)belt[0].X} {(int)belt[0].Y} L {(int)belt[1].X} {(int)belt[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)belt[0].X} {(int)belt[0].Y} L {(int)belt[2].X} {(int)belt[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)belt[1].X} {(int)belt[1].Y} L {(int)belt[3].X} {(int)belt[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)belt[2].X} {(int)belt[2].Y} L {(int)belt[3].X} {(int)belt[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)belt[4].X} {(int)belt[4].Y} L {(int)belt[5].X} {(int)belt[5].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
                if (hasButtons)
                    s += $@"
                    <path d=""M {(int)belt[6].X} {(int)belt[6].Y} h {(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)belt[7].X} {(int)belt[7].Y} h {-(int)pixelsizeincm}""  stroke-width=""1"" stroke=""black""/>";
                //back
                s += @$"
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} L {(int)back[11].X} {(int)back[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[11].X} {(int)back[11].Y} L {(int)back[3].X} {(int)back[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[3].X} {(int)back[3].Y} L {(int)back[2].X} {(int)back[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[2].X} {(int)back[2].Y} L {(int)back[5].X} {(int)back[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[5].X} {(int)back[5].Y} L {(int)back[4].X} {(int)back[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[4].X} {(int)back[4].Y} L {(int)back[9].X} {(int)back[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[9].X} {(int)back[9].Y} L {(int)back[10].X} {(int)back[10].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[10].X} {(int)back[10].Y} L {(int)back[8].X} {(int)back[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[8].X} {(int)back[8].Y} L {(int)back[0].X} {(int)back[0].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
                // front
                s += @$"
                    <path d=""M {(int)front[1].X} {(int)front[1].Y} L {(int)front[5].X} {(int)front[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[5].X} {(int)front[5].Y} L {(int)front[4].X} {(int)front[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[4].X} {(int)front[4].Y} L {(int)front[8].X} {(int)front[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[8].X} {(int)front[8].Y} L {(int)front[10].X} {(int)front[10].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[10].X} {(int)front[10].Y} L {(int)front[9].X} {(int)front[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[9].X} {(int)front[9].Y} L {(int)front[3].X} {(int)front[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[3].X} {(int)front[3].Y} L {(int)front[11].X} {(int)front[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[11].X} {(int)front[11].Y} L {(int)front[2].X} {(int)front[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[2].X} {(int)front[2].Y} L {(int)front[1].X} {(int)front[1].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
            }

            return s;
        }
    }
}
