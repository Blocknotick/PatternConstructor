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
            float CH = hips / 2;
            float width = beltwitdth * 2 + 2 + hips / 2 + 1 + 2;
            float height = 2 + 1 + skirtlengthSide;

            if (isLecal)
            {
                width += 5;
                height += 4;
            }

            widthcm = width * pixelsizeincm;
            heightcm = height * pixelsizeincm;

            string s = $"<svg version=\"1.1\" width = \"{(int)width * 10}mm\" height = \"{(int)height * 10}mm\" xmlns =\"http://www.w3.org/2000/svg\">";

            int beltadd = 0;
            if (hasButtons) beltadd = 3;

            
            List<Vector2> back = new List<Vector2>();
            List<Vector2> front = new List<Vector2>();
            List<Vector2> belt = new List<Vector2>();
            belt.Add(new Vector2(0, 0));
            belt.Add(new Vector2(beltwitdth*2, 0));
            belt.Add(new Vector2(0, waist + 2 + beltadd));
            belt.Add(new Vector2(beltwitdth * 2, waist + 2 + beltadd));

            back.Add(new Vector2(0, 0));
            back.Add(new Vector2(0,hipsHeight-1)); //линия бедер

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



            return s;
        }
    }
}
