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


            widthcm = a1 * pixelsizeincm;
            heightcm = di * pixelsizeincm;
            for (int i = 0; i < back.Count; i++)
            {
                back[i] = (float)pixelsizeincm * back[i];
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

            return s;
        }
    }
}
