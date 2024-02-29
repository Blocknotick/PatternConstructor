using PatternConstructor.Data.Enum;
using PatternConstructor.ViewModels;
using System.Numerics;

namespace PatternConstructor.Data
{
    public class DressPattern : Pattern
    {
        float csh,
            cg1,
            cg2,
            cg3,
            ct,
            cb,
            dts,
            vg,
            vpk,
            dtp,
            shs,
            shg,
            shp,
            vprz,
            tsg,
            dr,
            op,
            di;
        float pg,
            pshs,
            pshp,
            ppr,
            pt,
            pb,
            pshgor,
            pdts,
            pspr,
            pop;

        string neckType, collartype, sleevetype, clasptype, waisttype;
        public DressPattern(DressConstructModel model)
        {
            switch (model.DressCombinationModel.Silhouette)
            {
                case "Среднее":
                    pg = 4;
                    pshs = 1;
                    pshp = 0.6f;
                    ppr = 2.4f;
                    pt = 3;
                    pb = 2;
                    pdts = 0.5f;
                    pspr = 1.5f;
                    pshgor = 0.5f;
                    pop = 4;
                    break;
                case "Плотное":
                    pg = 3;
                    pshs = 0.6f;
                    pshp = 0.3f;
                    ppr = 2.1f;
                    pt = 1;
                    pb = 1.5f;
                    pdts = 0.5f;
                    pspr = 1.5f;
                    pshgor = 0.5f;
                    pop = 3;
                    break;
                case "Свободное":
                    pg = 6;
                    pshs = 1.8f;
                    pshp = 1.2f;
                    ppr = 3;
                    pt = 5;
                    pb = 5;
                    pdts = 0.5f;
                    pspr = 1.5f;
                    pshgor = 0.5f;
                    pop = 6;
                    break;
                default:
                    break;
            }

            csh = (float)model.NeckGirth/2;
            cg1 = (float)model.BustGirthUp/2;
            cg2 = (float)model.BustGirthSecond / 2;
            cg3 = (float)model.BustGirth / 2;
            ct = (float)model.WaistGirth / 2;
            cb = (float)model.HipsGirth / 2;
            dts = (float)model.BackWaistLength;
            vg = (float)model.BustHeight;
            vpk = (float)model.ShoulderHeight;
            dtp = (float)model.FrontWaistLength;
            shs = (float)model.BackWidth/2;
            shg = (float)model.BustWidth / 2;
            shp = (float)model.ShoulderToNeck;
            vprz = (float)model.BackArmholeDepth;
            tsg = (float)model.BustCenter/2;
            dr = (float)model.ShoulderToWrist;
            op = (float)model.UpperArm;
            di = (float)(dts + CountLength(model.DressCombinationModel.Length, model.WaistFloorFrontLength));

            neckType = model.DressCombinationModel.Neck;
            collartype = model.DressCombinationModel.Collar;
            sleevetype = model.DressCombinationModel.Sleeve;
            clasptype = model.DressCombinationModel.Clasp;
            waisttype = model.DressCombinationModel.Waist;
        }
        public override string GenerateContent()
        {
            List<Vector2> back = new List<Vector2>();
            List<Vector2> front = new List<Vector2>();
            List<Vector2> sleeve = new List<Vector2>();
            List<Vector2> collar = new List<Vector2>();
            List<Vector2> cuff = new List<Vector2>();

            float a1 = cg3 + pg;
            float g = vprz + pspr;
            float t = dts + pdts;
            float b = t + 0.5f * dts - 2;
            float a = shs + pshs;
            float a2 = a1 - (shg + cg2 - cg1 + pshp);

            float II1 = 2;
            float A2I = 4;
            Vector2 A1 = new Vector2(csh / 3 + pshgor, (csh / 3 + pshgor) / 3);

            back.Add(new Vector2(0, A1.Y));
            back.Add(new Vector2(0, t));
            back.Add(new Vector2(0, di));
            back.Add(new Vector2(A1.X, 0));
            Vector2[] arr = CirclesIntersec(back[3], shp + II1, back[1], vpk + pdts);
            Vector2 P1;
            if (arr[0].X > arr[1].X) P1 = arr[0];
            else P1 = arr[1];
            back.Add(P1);

            P1.X -= back[3].X;
            Vector2 I;
            I.Y = A2I * P1.Y / P1.X;
            I.X = (float)Math.Sqrt(A2I* A2I - I.Y * I.Y);

            Vector2 I1;
            I1.Y = (A2I+ II1) * P1.Y / P1.X;
            I1.X = (float)Math.Sqrt((A2I + II1)* (A2I + II1) - I1.Y * I1.Y);
            I += back[3];
            I1 += back[3];

            Vector2 I2;
            I2.X = I.X;
            I2.Y = I.Y+7;
            float d = Vector2.Distance(I1, I2);
            float dy = (I2.Y - I1.Y) * 7 / d;
            float dx = (float)Math.Sqrt(49-dy*dy);

            back.Add(I);
            back.Add(I2);
            back.Add(new Vector2(I2.X+dx, I2.Y-dy));


            float G3P2 = vprz + pspr - back[4].Y;
            float P3G3 = G3P2 / 3 + 2;
            back.Add(new Vector2(a, vprz + pspr - P3G3));

            float G3G4 = a2 - a;
            back.Add(new Vector2((a2 + a) / 2, g));

            float vG3 = 0.2f * G3G4 + 0.5f;
            vG3 /= (float)Math.Sqrt(2);
            back.Add(new Vector2(a + vG3, g - vG3));


            Vector2 G6 = new Vector2(a1 - tsg - 0.5f, g);
            Vector2 A3 = new Vector2(a1, t-dtp-pdts);
            front.Add(new Vector2(a1, A3.Y + back[3].X + 1)); //A5
            front.Add(new Vector2(a1 - back[3].X, A3.Y)); // A4
            float G7y = front[1].Y + (float)Math.Sqrt(vg*vg - (G6.X - front[1].X) * (G6.X - front[1].X));
            front.Add(new Vector2(G6.X, G7y)); //G7
            front.Add(back[9]);//G3

            float cG4 = (float)(0.2 * G3G4 / Math.Sqrt(2));
            front.Add(new Vector2(a2 - cG4, g - cG4)); //c

            float P4y = back[4].Y + 1;
            front.Add(new Vector2(a2, P4y + 2f / 3f * (g - P4y))); //P6

            arr = CirclesIntersec(front[2], vg, front[1], 2*(cg2-cg1)+2);
            Vector2 A9;
            if (arr[0].X < arr[1].X) A9 = arr[0];
            else A9 = arr[1];

            arr = CirclesIntersec(front[5], front[5].Y-P4y, A9, shp);
            Vector2 P5;
            if (arr[0].X < arr[1].X) P5 = arr[0];
            else P5 = arr[1];

            front.Add(P5); //P5

            Vector2 D = (front[6] + front[5])/2;
            float sin = (front[5].Y - front[6].Y)/ (front[5].X - front[6].X);
            float c = 1/sin;
            front.Add(new Vector2(D.X + sin, D.Y - c)); //e

            float d1 = Vector2.Distance(back[3], back[5]);
            float P5P7 = Vector2.Distance(A9, P5)-d1;

            float px = P5P7 * Math.Abs(P5.X - A9.X) / Vector2.Distance(A9, P5);
            float py = P5P7 * Math.Abs(P5.Y - A9.Y) / Vector2.Distance(A9, P5);

            front.Add(new Vector2(P5.X + px, P5.Y - py)); //P7

            arr = CirclesIntersec(front[2], Vector2.Distance(front[2], front[8]), front[1], Vector2.Distance(back[3], back[5]));
            Vector2 A8;
            if (arr[0].X < arr[1].X) A8 = arr[0];
            else A8 = arr[1];
            front.Add(A8); //A8


            Vector2 offs = new Vector2(0, -front[1].Y);
            widthcm = a1 * pixelsizeincm;
            heightcm = di * pixelsizeincm;
            for (int i = 0; i < back.Count; i++)
            {
                back[i] = (float)pixelsizeincm * (back[i]+offs);
            }
            for (int i = 0;i < front.Count;i++)
            {
                front[i] = (float)pixelsizeincm * (front[i] + offs);
            }
            string s = $"<svg version=\"1.1\" width = \"{(int)a1 * 10}mm\" height = \"{(int)di * 10}mm\" xmlns =\"http://www.w3.org/2000/svg\">";
            s += @$"
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} L {(int)back[2].X} {(int)back[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} L {(int)back[3].X} {(int)back[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[3].X} {(int)back[3].Y} L {(int)back[5].X} {(int)back[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[5].X} {(int)back[5].Y} L {(int)back[6].X} {(int)back[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[6].X} {(int)back[6].Y} L {(int)back[7].X} {(int)back[7].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[7].X} {(int)back[7].Y} L {(int)back[4].X} {(int)back[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[4].X} {(int)back[4].Y} L {(int)back[8].X} {(int)back[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[8].X} {(int)back[8].Y} L {(int)back[10].X} {(int)back[10].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[10].X} {(int)back[10].Y} L {(int)back[9].X} {(int)back[9].Y}""  stroke-width=""3"" stroke=""black""/>
                ";

            s += @$"
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} L {(int)front[1].X} {(int)front[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[1].X} {(int)front[1].Y} L {(int)front[9].X} {(int)front[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[9].X} {(int)front[9].Y} L {(int)front[2].X} {(int)front[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[2].X} {(int)front[2].Y} L {(int)front[8].X} {(int)front[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[8].X} {(int)front[8].Y} L {(int)front[6].X} {(int)front[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[6].X} {(int)front[6].Y} L {(int)front[7].X} {(int)front[7].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[7].X} {(int)front[7].Y} L {(int)front[5].X} {(int)front[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[5].X} {(int)front[5].Y} L {(int)front[4].X} {(int)front[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[4].X} {(int)front[4].Y} L {(int)front[3].X} {(int)front[3].Y}""  stroke-width=""3"" stroke=""black""/>
                ";

            return s;
        }
    }
}
