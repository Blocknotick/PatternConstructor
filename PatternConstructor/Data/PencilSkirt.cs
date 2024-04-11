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
            float height = Math.Max(2 + 1 + Math.Max(skirtlengthSide,skirtlengthBack), waist + 2 + beltadd +2);

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
            List<Vector2> backD = new List<Vector2>();
            List<Vector2> frontD = new List<Vector2>();
            List<Vector2> beltD = new List<Vector2>();
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

            if (isLecal)
            {
                beltD.Add(belt[0] - Vector2.One);
                beltD.Add(belt[1] - Vector2.UnitY + Vector2.UnitX);
                beltD.Add(belt[2] + Vector2.UnitY - Vector2.UnitX);
                beltD.Add(belt[3] + Vector2.One);

                backD.Add(back[0] - Vector2.One);//0
                backD.Add(back[11] + Vector2.UnitY - Vector2.UnitX);//1
                backD.Add(Normal(back[0], back[8], false) + back[0]);//2
                backD.Add(Normal(back[0], back[8], false) + back[8]);//3
                backD[3] = Intersec(backD[2], backD[3], back[8], back[10]);
                backD[0] = Intersec(backD[0], backD[1], backD[2], backD[3]);
                backD.Add(Normal(back[9], back[4], false) + back[9]);//4
                backD.Add(Normal(back[9], back[4], false) + back[4]);//5
                backD[4] = Intersec(backD[4], backD[5], back[9], back[10]);
                backD.Add(Intersec(back[10], back[7], backD[4], backD[5]));//6
                backD.Add(Normal(back[4], back[5], false) + back[4]);//7
                backD.Add(Normal(back[4], back[5], false) + back[5]);//8
                backD[7] = Intersec(backD[4], backD[5], backD[7], backD[8]);
                backD.Add(back[5] + Vector2.UnitX);//9
                backD.Add(back[2] + Vector2.UnitX);//10
                backD[8] = Intersec(backD[9], backD[10], backD[7], backD[8]);
                if (skirtType == "Pencil")
                    backD.Add(back[3] + Vector2.One);//11
                else if (skirtType == "Tulip")
                {
                    backD.Add(Normal(back[3], back[2], true) + back[3]);//11
                    backD[10] = Normal(back[3], back[2], true) + back[2];
                    backD[11] = Intersec(backD[10], backD[11], backD[1], backD[1] + Vector2.UnitX);
                    backD[10] = Intersec(backD[10], backD[11], backD[9], backD[9] + Vector2.UnitY);
                }

                frontD.Add(front[3] - Vector2.UnitY);//0
                frontD.Add(front[11] + Vector2.UnitY);//1
                frontD.Add(Normal(front[3], front[9], false) + front[3]);//2
                frontD.Add(Normal(front[3], front[9], false) + front[9]);//3
                frontD[3] = Intersec(frontD[3], frontD[2], front[9], front[10]);
                frontD[0] = Intersec(frontD[0], frontD[1], frontD[2], frontD[3]);
                frontD.Add(Normal(front[8], front[4], false) + front[8]);//4
                frontD.Add(Normal(front[8], front[4], false) + front[4]);//5
                frontD[4] = Intersec(frontD[4], frontD[5], front[8], front[10]);
                frontD.Add(Intersec(front[10], front[7], frontD[4], frontD[5]));//6
                frontD.Add(Normal(front[4], front[5], false) + front[4]);//7
                frontD.Add(Normal(front[4], front[5], false) + front[5]);//8
                frontD[7] = Intersec(frontD[4], frontD[5], frontD[7], frontD[8]);
                frontD.Add(front[5] - Vector2.UnitX);//9
                frontD.Add(front[1] - Vector2.UnitX);//10
                frontD[8] = Intersec(frontD[9], frontD[10], frontD[7], frontD[8]);
                if (skirtType == "Pencil")
                    frontD.Add(front[2] - Vector2.UnitX + Vector2.UnitY);//11
                else if (skirtType == "Tulip")
                {
                    frontD.Add(Normal(front[1], front[2], true) + front[2]);//11
                    frontD[10] = Normal(front[1], front[2], true) + front[1];
                    frontD[11] = Intersec(frontD[10], frontD[11], frontD[1], frontD[1] + Vector2.UnitX);
                    frontD[10] = Intersec(frontD[10], frontD[11], frontD[9], frontD[9] + Vector2.UnitY);
                }
            }
            

            Vector2 backoffset = Vector2.Zero;
            if (back[0].Y<0)
            {
                backoffset = new Vector2(0,-(back[0].Y));
            }

            for (int i = 0;i<belt.Count; i++)
            {
                belt[i] += Vector2.One;
                belt[i] = (float)pixelsizeincm * belt[i];
            }
            
            Vector2 beltoffset = new Vector2(belt[3].X, 0);
            for (int i = 0; i < back.Count; i++)
            {
                back[i] += backoffset;
                front[i] += backoffset;
                back[i] += Vector2.One;
                front[i] += Vector2.One;
                back[i] = (float)pixelsizeincm * back[i];
                back[i] += beltoffset;
                front[i] = (float)pixelsizeincm * front[i];
                front[i] += beltoffset;
            }

            if (isLecal)
            {
                for (int i = 0; i < beltD.Count; i++)
                {
                    beltD[i] += Vector2.One;
                    beltD[i] = (float)pixelsizeincm * beltD[i];
                }
                for (int i = 0; i < backD.Count; i++)
                {
                    backD[i] += backoffset;
                    frontD[i] += backoffset;
                    backD[i] += Vector2.One;
                    frontD[i] += Vector2.One;
                    backD[i] = (float)pixelsizeincm * backD[i];
                    backD[i] += beltoffset;
                    frontD[i] = (float)pixelsizeincm * frontD[i];
                    frontD[i] += beltoffset;
                }
            }
            

            if (isLecal)
            {
                // сдвинуть белт на 1 1 (умноженное на пиксель в сантиметрах)
                for (int i=0;i<belt.Count;i++)
                    belt[i] += Vector2.One * (float)pixelsizeincm;
                for (int i = 0; i < beltD.Count; i++)
                    beltD[i] += Vector2.One * (float)pixelsizeincm;
                // сдвинуть бэк на 3 1
                for (int i = 0; i < back.Count; i++)
                    back[i] += Vector2.UnitY * (float)pixelsizeincm + Vector2.UnitX * 3 * (float)pixelsizeincm;
                for (int i = 0; i < backD.Count; i++)
                    backD[i] += Vector2.UnitY * (float)pixelsizeincm + Vector2.UnitX * 3 * (float)pixelsizeincm;
                // сдвинуть фронт на 5 1
                for (int i = 0; i < front.Count; i++)
                    front[i] += Vector2.UnitY * (float)pixelsizeincm + Vector2.UnitX * 5 * (float)pixelsizeincm;
                for (int i = 0; i < frontD.Count; i++)
                    frontD[i] += Vector2.UnitY * (float)pixelsizeincm + Vector2.UnitX * 5 * (float)pixelsizeincm;
                //beltD
                s += @$"
                    <path d=""M {(int)beltD[0].X} {(int)beltD[0].Y} L {(int)beltD[1].X} {(int)beltD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)beltD[0].X} {(int)beltD[0].Y} L {(int)beltD[2].X} {(int)beltD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)beltD[1].X} {(int)beltD[1].Y} L {(int)beltD[3].X} {(int)beltD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)beltD[2].X} {(int)beltD[2].Y} L {(int)beltD[3].X} {(int)beltD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
                //backD
                s += @$"
                    <path d=""M {(int)backD[0].X} {(int)backD[0].Y} L {(int)backD[1].X} {(int)backD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[1].X} {(int)backD[1].Y} L {(int)backD[11].X} {(int)backD[11].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[11].X} {(int)backD[11].Y} L {(int)backD[10].X} {(int)backD[10].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[10].X} {(int)backD[10].Y} L {(int)backD[9].X} {(int)backD[9].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[9].X} {(int)backD[9].Y} L {(int)backD[8].X} {(int)backD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[8].X} {(int)backD[8].Y} L {(int)backD[7].X} {(int)backD[7].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[7].X} {(int)backD[7].Y} L {(int)backD[5].X} {(int)backD[5].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[5].X} {(int)backD[5].Y} L {(int)backD[4].X} {(int)backD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[4].X} {(int)backD[4].Y} L {(int)backD[6].X} {(int)backD[6].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[6].X} {(int)backD[6].Y} L {(int)backD[3].X} {(int)backD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[3].X} {(int)backD[3].Y} L {(int)backD[0].X} {(int)backD[0].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
                //frontD
                s += @$"
                    <path d=""M {(int)frontD[0].X} {(int)frontD[0].Y} L {(int)frontD[1].X} {(int)frontD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[1].X} {(int)frontD[1].Y} L {(int)frontD[11].X} {(int)frontD[11].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[11].X} {(int)frontD[11].Y} L {(int)frontD[10].X} {(int)frontD[10].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[10].X} {(int)frontD[10].Y} L {(int)frontD[9].X} {(int)frontD[9].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[9].X} {(int)frontD[9].Y} L {(int)frontD[8].X} {(int)frontD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[8].X} {(int)frontD[8].Y} L {(int)frontD[7].X} {(int)frontD[7].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[7].X} {(int)frontD[7].Y} L {(int)frontD[5].X} {(int)frontD[5].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[5].X} {(int)frontD[5].Y} L {(int)frontD[4].X} {(int)frontD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[4].X} {(int)frontD[4].Y} L {(int)frontD[6].X} {(int)frontD[6].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[6].X} {(int)frontD[6].Y} L {(int)frontD[3].X} {(int)frontD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[3].X} {(int)frontD[3].Y} L {(int)frontD[0].X} {(int)frontD[0].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
            }

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

            s += $@"
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((belt[2].X + belt[4].X) / 2)}, {(int)((belt[2].Y + belt[0].Y) / 2)}) rotate(270)"" >Belt of the {skirtType} skirt, x1, interfacing</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((belt[4].X) - 1)}, {(int)((belt[2].Y + belt[0].Y) / 2)}) rotate(270)"">Fold</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((back[0].X + back[2].X) / 2)}, {(int)((back[0].Y + back[11].Y) / 2)})"">Back of the {skirtType} skirt, x2</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((front[3].X + front[2].X) / 2)}, {(int)((front[3].Y + front[11].Y) / 2)})"">Front of the {skirtType} skirt, x1</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)(front[11].X-1)}, {(int)((front[11].Y + front[3].Y) / 2)}) rotate(270)"" >Fold</text>
            ";
            return s;
        }
    }
}
