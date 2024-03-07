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
                    pshs = 0.25f * pg;
                    pshp = 0.15f * pg;
                    ppr = 0.6f * pg;
                    pt = 3;
                    pb = 3;
                    pdts = 0.5f;
                    pspr = 1.5f;
                    pshgor = 0.5f;
                    pop = 4;
                    break;
                case "Плотное":
                    pg = 3;
                    pshs = 0.2f*pg;
                    pshp = 0.1f * pg;
                    ppr = 0.7f * pg;
                    pt = 1;
                    pb = 2;
                    pdts = 0.5f;
                    pspr = 1.5f;
                    pshgor = 0.5f;
                    pop = 3;
                    break;
                case "Свободное":
                    pg = 7;
                    pshs = 0.3f * pg;
                    pshp = 0.2f * pg;
                    ppr = 0.5f * pg;
                    pt = 5;
                    pb = 6;
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

            List<Vector2> skirtback = new List<Vector2>();
            List<Vector2> skirtfront = new List<Vector2>();

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
            float sin = (front[5].Y - front[6].Y)/ Vector2.Distance(front[6], front[5]);
            float c = (front[5].X - front[6].X) / Vector2.Distance(front[6], front[5]);
            front.Add(new Vector2(D.X + sin, D.Y - c)); //e

            Vector2 DE = front[7] - D;
            DE += front[6];
            DE = (DE + front[7]) / 2;
            DE = (DE + front[7]) / 2;

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

            float sb = (cg3 + pg) - (ct + pt); // сумма талиевых вытачек 
            back.Add(new Vector2(back[9].X - sb / 4f, t));// T3
            front.Add(new Vector2(front[3].X + sb / 4f, t)); // T4
            skirtback.Add(back[11]); // T3
            skirtfront.Add(front[10]); // T4

            float gbdif = ((cb + pb)- (cg3 + pg)) /2;
            skirtback.Add(new Vector2(back[9].X + gbdif, b)); //Б3
            skirtfront.Add(new Vector2(front[3].X - gbdif, b)); // Б4
            skirtback.Add(new Vector2(skirtback[1].X, di)); //H3
            skirtfront.Add(new Vector2(skirtfront[1].X, di)); // H4
            skirtback.Add(new Vector2(0, di)); //H

            float FRONT_DIF = 1;
            skirtfront.Add(new Vector2(front[0].X, di+FRONT_DIF)); // H11

            skirtback.Add(back[1]); //T
            skirtfront.Add(new Vector2(front[0].X, t+FRONT_DIF)); // T11

            float T5 = (back[1].X + back[11].X) / 2;
            back.Add(new Vector2(T5 - sb / 6, t));
            back.Add(new Vector2(T5,g+3));
            back.Add(new Vector2(T5 + sb / 6, t));
            skirtback.Add(back[12]);
            skirtback.Add(new Vector2(T5, b-5));
            skirtback.Add(back[14]);

            front.Add(skirtfront[4]);
            Vector2 T6 = Intersec(front[2],G6,front[10],front[11]);
            front.Add(new Vector2(T6.X + sb / 12, T6.Y));
            front.Add(new Vector2(T6.X, front[2].Y+3));
            front.Add(new Vector2(T6.X - sb / 12, T6.Y));
            skirtfront.Add(front[12]);
            skirtfront.Add(new Vector2(T6.X, b - 5));
            skirtfront.Add(front[14]);

            
            front.Add(DE);


            if (neckType == "V-горловина")
            {
                //affect back[3], front[1], front[0]
                back[3] += (back[4] - back[3]) / Vector2.Distance(back[3], back[4]);
                front[1] += (front[9] - front[1]) / Vector2.Distance(front[9], front[1]);
                front[0] = new Vector2(front[0].X, g - 2);
            }
            else if (neckType== "Круглая")
            {
                back[3] += (back[4] - back[3]) / Vector2.Distance(back[3], back[4]) * 2;
                front[0] += new Vector2(0, 2);
                front[1] += (front[9] - front[1]) / Vector2.Distance(front[9], front[1]) * 2;
            }
            arr = CirclesIntersec(back[6], 7.3f, back[3], Vector2.Distance(back[3], back[5]));
            Vector2 I10;
            if (arr[0].X > arr[1].X) I10 = arr[0];
            else I10 = arr[1];
            back.Add(I10); //I10
            arr = CirclesIntersec(back[6], 7.3f, back[4], Vector2.Distance(back[4], back[7]));
            if (arr[0].X < arr[1].X) I10 = arr[0];
            else I10 = arr[1];
            back.Add(I10); //I11

            back.Add(new Vector2(back[8].X - 0.5f, back[8].Y)); //отметки для рукава (17)
            front.Add(new Vector2(front[5].X + 0.5f, front[5].Y)); // отметки для рукава (16)
            sin = (front[9].X - front[1].X) / Vector2.Distance(front[9], front[1]);
            c = (front[1].Y - front[9].Y) / Vector2.Distance(front[9], front[1]);
            back.Add(Intersec(back[3], new Vector2(back[3].X - c, back[3].Y + sin), back[0], A1)); //точка для рисования горловины спинки (18)
            //sleeve

            if (sleevetype!= "Без рукава")
            {
                Vector2 O = (back[4] + front[6]) / 2;
                sleeve.Add(new Vector2(O.X, O.Y + 2f)); //O2
                float shruk = (op + pop) / 2;
                //sleeve.Add(new Vector2(sleeve[0].X + shruk/2 + 1f, front[5].Y+0.5f)); //P62
                sleeve.Add(new Vector2(sleeve[0].X + shruk / 2 + 0.5f, front[5].Y + 0.5f)); //P62
                sleeve.Add(new Vector2(sleeve[0].X + shruk, back[9].Y)); //P2
                sleeve.Add(new Vector2(sleeve[0].X - shruk / 2 - 1f, back[8].Y + 0.5f)); //P31
                sleeve.Add(new Vector2(sleeve[0].X - shruk, back[9].Y)); //P1
                sleeve.Add(new Vector2(sleeve[0].X + shruk / 4 + 2, sleeve[0].Y)); // O5
                sleeve.Add(new Vector2(sleeve[0].X - shruk / 4, sleeve[0].Y)); // O6
                sleeve.Add(new Vector2((float)(sleeve[5].X - 2.25 * Math.Sqrt((1 - (sleeve[1].X - sleeve[5].X) / Vector2.Distance(sleeve[5], sleeve[1])) / 2)),
                    (float)(sleeve[5].Y + 2.25 * Math.Sqrt((1 + (sleeve[1].X - sleeve[5].X) / Vector2.Distance(sleeve[5], sleeve[1])) / 2)))); // O51
                sleeve.Add(new Vector2((float)(sleeve[6].X + 1.5 * Math.Sqrt((1 - (sleeve[6].X - sleeve[3].X) / Vector2.Distance(sleeve[3], sleeve[6])) / 2)),
                    (float)(sleeve[6].Y + 1.5 * Math.Sqrt((1 + (sleeve[6].X - sleeve[3].X) / Vector2.Distance(sleeve[3], sleeve[6])) / 2)))); // O61
                Vector2 Six = (sleeve[3] + sleeve[4]) / 2;
                sin = (float)((sleeve[4].Y - sleeve[3].Y) / Vector2.Distance(sleeve[3], sleeve[4]));
                c = (float)((-sleeve[4].X + sleeve[3].X) / Vector2.Distance(sleeve[3], sleeve[4]));
                sleeve.Add(new Vector2(Six.X + 1.2f * sin, Six.Y + 1.2f * c)); // 7
                cG4 = (float)((0.2 * G3G4 + 1) / Math.Sqrt(2));
                sleeve.Add(new Vector2(sleeve[0].X + shruk / 2 + cG4, sleeve[2].Y-cG4)); // P81
                Vector2 L = new Vector2(sleeve[0].X, sleeve[0].Y + dr / 2 + 3);
                if (sleevetype== "Короткий")
                {
                    sleeve.Add(new Vector2(sleeve[0].X, sleeve[0].Y + (L.Y - sleeve[0].Y) / 3 * 2));
                    sin = 10.25f; //это тангенс донт майнд зе нейм
                    float slevelen = sleeve[11].Y - sleeve[4].Y + 0.5f;
                    sleeve.Add(new Vector2(sleeve[4].X + slevelen / sin, sleeve[4].Y + slevelen));
                    sleeve.Add(new Vector2(sleeve[2].X - slevelen / sin, sleeve[2].Y + slevelen));
                    sleeve.Add(new Vector2(sleeve[3].X + 0.5f, sleeve[3].Y));
                    sleeve.Add(new Vector2(sleeve[1].X - 0.5f, sleeve[1].Y));
                }
                else if (sleevetype == "Епископ с резинкой")
                {
                    sleeve.Add(new Vector2(sleeve[1].X - 0.5f, sleeve[1].Y));
                    sleeve.Add(new Vector2(sleeve[3].X + 0.5f, sleeve[3].Y));
                    sleeve.Add(new Vector2(sleeve[2].X - 0.5f, sleeve[0].Y+dr));
                    sleeve.Add(new Vector2(sleeve[4].X + 0.5f, sleeve[0].Y+dr));
                    //                    x_rotated = ((x - dx) * cos(angle)) - ((dy - y) * sin(angle)) + dx
                    //                    y_rotated = dy - ((dy - y) * cos(angle)) + ((x - dx) * sin(angle))
                    dx = sleeve[0].X;
                    dy = sleeve[0].Y;
                    sin = (float)Math.Sin(0.174533);
                    c = (float)Math.Cos(0.174533);
                    //sleeve[1] = new Vector2((sleeve[1].X - dx) * c - (dy - sleeve[1].Y)*sin + dx, dy - ((dy - sleeve[1].Y) * c) + ((sleeve[1].X - dx) * sin));
                    //sleeve[2] = new Vector2((sleeve[2].X - dx) * c - (dy - sleeve[2].Y) * sin + dx, dy - ((dy - sleeve[2].Y) * c) + ((sleeve[2].X - dx) * sin));
                    //sleeve[13] = new Vector2((sleeve[13].X - dx) * c - (dy - sleeve[13].Y) * sin + dx, dy - ((dy - sleeve[13].Y) * c) + ((sleeve[13].X - dx) * sin));
                    //sleeve[11] = new Vector2((sleeve[11].X - dx) * c - (dy - sleeve[11].Y) * sin + dx, dy - ((dy - sleeve[11].Y) * c) + ((sleeve[11].X - dx) * sin));
                    //sleeve[5] = new Vector2((sleeve[5].X - dx) * c - (dy - sleeve[5].Y) * sin + dx, dy - ((dy - sleeve[5].Y) * c) + ((sleeve[5].X - dx) * sin));
                    //sleeve[10] = new Vector2((sleeve[10].X - dx) * c - (dy - sleeve[10].Y) * sin + dx, dy - ((dy - sleeve[10].Y) * c) + ((sleeve[10].X - dx) * sin));
                }
                else if (sleevetype == "Епископ с манжетой")
                {

                }
            }

            Vector2 offsetFront = new Vector2(0, Math.Max(-front[1].Y,0));
            Vector2 offsetB = new Vector2(Math.Max(gbdif*2,0), 0);
            Vector2 sleeveoff = new Vector2(0,0);
            

            widthcm = (front[0].X+offsetB.X) * pixelsizeincm;
            heightcm = (skirtfront[3].Y+offsetFront.Y) * pixelsizeincm;
            int width = (int)((front[0].X + offsetB.X) * 10);
            int height = (int)((skirtfront[3].Y + offsetFront.Y) * 10);

            if (sleevetype != "Без рукава")
            {
                sleeveoff = new Vector2(offsetB.X + front[0].X - sleeve[4].X, -(sleeve[0].Y));
                widthcm = (Math.Max(sleeve[2].X, sleeve[13].X) + sleeveoff.X) * pixelsizeincm;
                width = (int)((sleeve[2].X + sleeveoff.X) * 10);
            }

            for (int i = 0; i < back.Count; i++)
                back[i] = (float)pixelsizeincm * (back[i]+offsetFront);
            
            for (int i = 0;i < front.Count;i++)
                front[i] = (float)pixelsizeincm * (front[i] + offsetFront + offsetB);

            for (int i = 0; i < skirtback.Count; i++)
                skirtback[i] = (float)pixelsizeincm * (skirtback[i] + offsetFront);

            for (int i = 0; i < skirtfront.Count; i++)
                skirtfront[i] = (float)pixelsizeincm * (skirtfront[i] + offsetFront + offsetB);

            for (int i = 0; i < sleeve.Count; i++)
                sleeve[i] = (float)pixelsizeincm * (sleeve[i] + sleeveoff + offsetFront);


            string s = $"<svg version=\"1.1\" width = \"{width}mm\" height = \"{height}mm\" xmlns =\"http://www.w3.org/2000/svg\">";
            s += @$"
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} Q {(int)back[18].X},{(int)back[18].Y} {(int)back[3].X},{(int)back[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} L {(int)back[1].X} {(int)back[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[3].X} {(int)back[3].Y} L {(int)back[15].X} {(int)back[15].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[15].X} {(int)back[15].Y} L {(int)back[6].X} {(int)back[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[6].X} {(int)back[6].Y} L {(int)back[16].X} {(int)back[16].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[16].X} {(int)back[16].Y} L {(int)back[4].X} {(int)back[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[4].X} {(int)back[4].Y} C {(int)back[4].X},{(int)back[4].Y} {(int)back[8].X},{(int)(back[8].Y - 2*pixelsizeincm)} {(int)back[8].X},{(int)back[8].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[8].X} {(int)back[8].Y} Q {(int)back[8].X},{(int)((back[10].Y+ back[8].Y) /2)} {(int)back[10].X},{(int)back[10].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[10].X} {(int)back[10].Y} Q {(int)((back[9].X+ back[10].X) /2)},{(int)back[9].Y} {(int)back[9].X},{(int)back[9].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[9].X} {(int)back[9].Y} L {(int)back[11].X} {(int)back[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[11].X} {(int)back[11].Y} L {(int)back[14].X} {(int)back[14].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[14].X} {(int)back[14].Y} L {(int)back[13].X} {(int)back[13].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[13].X} {(int)back[13].Y} L {(int)back[12].X} {(int)back[12].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[12].X} {(int)back[12].Y} L {(int)back[1].X} {(int)back[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[17].X} {(int)back[17].Y} L {(int)back[8].X} {(int)back[8].Y}""  stroke-width=""1"" stroke=""black""/>

                ";

            s += @$"
                    <path d=""M {(int)skirtback[0].X} {(int)skirtback[0].Y} C {(int)skirtback[0].X},{(int)(skirtback[0].Y + pixelsizeincm)} {(int)skirtback[1].X},{(int)(skirtback[1].Y - pixelsizeincm * 5)} {(int)skirtback[1].X},{(int)skirtback[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtback[1].X} {(int)skirtback[1].Y} L {(int)skirtback[2].X} {(int)skirtback[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtback[2].X} {(int)skirtback[2].Y} L {(int)skirtback[3].X} {(int)skirtback[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtback[3].X} {(int)skirtback[3].Y} L {(int)skirtback[4].X} {(int)skirtback[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtback[4].X} {(int)skirtback[4].Y} L {(int)skirtback[5].X} {(int)skirtback[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtback[5].X} {(int)skirtback[5].Y} L {(int)skirtback[6].X} {(int)skirtback[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtback[6].X} {(int)skirtback[6].Y} L {(int)skirtback[7].X} {(int)skirtback[7].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtback[7].X} {(int)skirtback[7].Y} L {(int)skirtback[0].X} {(int)skirtback[0].Y}""  stroke-width=""3"" stroke=""black""/>
                ";

            if (neckType=="V-горловина")
            s += @$"
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} L {(int)front[1].X},{(int)front[1].Y}""  stroke-width=""3"" stroke=""black"" />
                ";
            else
                s += @$"
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} Q {(int)front[1].X},{(int)front[0].Y} {(int)front[1].X},{(int)front[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                ";
            s += @$"
                    <path d=""M {(int)front[1].X} {(int)front[1].Y} L {(int)front[9].X} {(int)front[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[9].X} {(int)front[9].Y} L {(int)front[2].X} {(int)front[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[2].X} {(int)front[2].Y} L {(int)front[8].X} {(int)front[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[8].X} {(int)front[8].Y} L {(int)front[6].X} {(int)front[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[6].X} {(int)front[6].Y} C {(int)front[6].X},{(int)front[6].Y} {(int)front[15].X},{(int)front[15].Y} {(int)front[7].X},{(int)front[7].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[7].X} {(int)front[7].Y} Q {(int)front[5].X},{(int)((front[7].Y+front[5].Y)/2)} {(int)front[5].X},{(int)front[5].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[5].X} {(int)front[5].Y} Q {(int)front[5].X},{(int)((front[4].Y+ front[5].Y) /2)} {(int)front[4].X},{(int)front[4].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[4].X} {(int)front[4].Y} Q {(int)((front[4].X+front[3].X)/2)},{(int)front[3].Y} {(int)front[3].X},{(int)front[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[3].X} {(int)front[3].Y} L {(int)front[10].X} {(int)front[10].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[10].X} {(int)front[10].Y} L {(int)front[14].X} {(int)front[14].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[14].X} {(int)front[14].Y} L {(int)front[13].X} {(int)front[13].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[13].X} {(int)front[13].Y} L {(int)front[12].X} {(int)front[12].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[12].X} {(int)front[12].Y} L {(int)front[11].X} {(int)front[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[11].X} {(int)front[11].Y} L {(int)front[0].X} {(int)front[0].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[5].X} {(int)front[5].Y} L {(int)front[16].X} {(int)front[16].Y}""  stroke-width=""3"" stroke=""black""/>

                ";

            s += @$"
                    <path d=""M {(int)skirtfront[0].X} {(int)skirtfront[0].Y} C {(int)skirtfront[0].X},{(int)(skirtfront[0].Y+pixelsizeincm)} {(int)skirtfront[1].X},{(int)(skirtfront[1].Y-pixelsizeincm*5)} {(int)skirtfront[1].X},{(int)skirtfront[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfront[1].X} {(int)skirtfront[1].Y} L {(int)skirtfront[2].X} {(int)skirtfront[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[2].X} {(int)skirtfront[2].Y} C {(int)skirtfront[2].X},{(int)skirtfront[2].Y} {(int)(skirtfront[3].X - pixelsizeincm*10)},{(int)skirtfront[3].Y} {(int)skirtfront[3].X},{(int)skirtfront[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfront[3].X} {(int)skirtfront[3].Y} L {(int)skirtfront[4].X} {(int)skirtfront[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[4].X} {(int)skirtfront[4].Y} L {(int)skirtfront[5].X} {(int)skirtfront[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[5].X} {(int)skirtfront[5].Y} L {(int)skirtfront[6].X} {(int)skirtfront[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[6].X} {(int)skirtfront[6].Y} L {(int)skirtfront[7].X} {(int)skirtfront[7].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[7].X} {(int)skirtfront[7].Y} L {(int)skirtfront[0].X} {(int)skirtfront[0].Y}""  stroke-width=""3"" stroke=""black""/>
                ";

            if (sleevetype=="Короткий")
            {
                //s += @$"
                //    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} L {(int)sleeve[7].X} {(int)sleeve[7].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[7].X} {(int)sleeve[7].Y} L {(int)sleeve[1].X} {(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[1].X} {(int)sleeve[1].Y} L {(int)sleeve[10].X} {(int)sleeve[10].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[10].X} {(int)sleeve[10].Y} L {(int)sleeve[2].X} {(int)sleeve[2].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[2].X} {(int)sleeve[2].Y} L {(int)sleeve[13].X} {(int)sleeve[13].Y}""  stroke-width=""3"" stroke=""black""/>

                //    <path d=""M {(int)sleeve[13].X},{(int)sleeve[13].Y} C {(int)sleeve[13].X},{(int)sleeve[13].Y} {(int)sleeve[13].X},{(int)sleeve[11].Y} {(int)sleeve[11].X},{(int)sleeve[11].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                //    <path d=""M {(int)sleeve[12].X},{(int)sleeve[12].Y} C {(int)sleeve[12].X},{(int)sleeve[12].Y} {(int)sleeve[12].X},{(int)sleeve[11].Y} {(int)sleeve[11].X},{(int)sleeve[11].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>

                //    <path d=""M {(int)sleeve[12].X} {(int)sleeve[12].Y} L {(int)sleeve[4].X} {(int)sleeve[4].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[4].X} {(int)sleeve[4].Y} L {(int)sleeve[9].X} {(int)sleeve[9].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[9].X} {(int)sleeve[9].Y} L {(int)sleeve[3].X} {(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[3].X} {(int)sleeve[3].Y} L {(int)sleeve[8].X} {(int)sleeve[8].Y}""  stroke-width=""3"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[8].X} {(int)sleeve[8].Y} L {(int)sleeve[0].X} {(int)sleeve[0].Y}""  stroke-width=""3"" stroke=""black""/>

                //    <path d=""M {(int)sleeve[3].X} {(int)sleeve[3].Y} L {(int)sleeve[14].X} {(int)sleeve[14].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[1].X} {(int)sleeve[1].Y} L {(int)sleeve[15].X} {(int)sleeve[15].Y}""  stroke-width=""1"" stroke=""black""/>

                //    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} L {(int)sleeve[6].X} {(int)sleeve[6].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[6].X} {(int)sleeve[6].Y} L {(int)sleeve[3].X} {(int)sleeve[3].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[8].X} {(int)sleeve[8].Y} L {(int)sleeve[6].X} {(int)sleeve[6].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} L {(int)sleeve[5].X} {(int)sleeve[5].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[5].X} {(int)sleeve[5].Y} L {(int)sleeve[7].X} {(int)sleeve[7].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[5].X} {(int)sleeve[5].Y} L {(int)sleeve[1].X} {(int)sleeve[1].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} L {(int)sleeve[11].X} {(int)sleeve[11].Y}""  stroke-width=""1"" stroke=""black""/>
                //    <path d=""M {(int)sleeve[4].X} {(int)sleeve[4].Y} L {(int)sleeve[2].X} {(int)sleeve[2].Y}""  stroke-width=""1"" stroke=""black""/>
                //";
                s += @$"
                    <path d=""M {(int)sleeve[2].X} {(int)sleeve[2].Y} L {(int)sleeve[13].X} {(int)sleeve[13].Y}""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)sleeve[13].X},{(int)sleeve[13].Y} C {(int)sleeve[13].X},{(int)sleeve[13].Y} {(int)sleeve[13].X},{(int)sleeve[11].Y} {(int)sleeve[11].X},{(int)sleeve[11].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[12].X},{(int)sleeve[12].Y} C {(int)sleeve[12].X},{(int)sleeve[12].Y} {(int)sleeve[12].X},{(int)sleeve[11].Y} {(int)sleeve[11].X},{(int)sleeve[11].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>

                    <path d=""M {(int)sleeve[12].X} {(int)sleeve[12].Y} L {(int)sleeve[4].X} {(int)sleeve[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} L {(int)sleeve[11].X} {(int)sleeve[11].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeve[0].X},{(int)sleeve[0].Y} Q {(int)sleeve[5].X},{(int)sleeve[5].Y} {(int)sleeve[1].X},{(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[0].X},{(int)sleeve[0].Y} Q {(int)sleeve[6].X},{(int)sleeve[6].Y} {(int)sleeve[3].X},{(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[2].X},{(int)sleeve[2].Y} Q {(int)sleeve[10].X},{(int)sleeve[10].Y} {(int)sleeve[1].X},{(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[4].X},{(int)sleeve[4].Y} Q {(int)sleeve[9].X},{(int)sleeve[9].Y} {(int)sleeve[3].X},{(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                ";
            }
            else if (sleevetype== "Епископ с резинкой")
            {
                s += @$"

                    <path d=""M {(int)sleeve[13].X} {(int)sleeve[13].Y} L {(int)sleeve[14].X} {(int)sleeve[14].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[14].X} {(int)sleeve[14].Y} L {(int)sleeve[4].X} {(int)sleeve[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[13].X} {(int)sleeve[13].Y} L {(int)sleeve[2].X} {(int)sleeve[2].Y}""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} v {(int)(pixelsizeincm/2)}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeve[1].X} {(int)sleeve[1].Y} L {(int)sleeve[11].X} {(int)sleeve[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[3].X} {(int)sleeve[3].Y} L {(int)sleeve[12].X} {(int)sleeve[12].Y}""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)sleeve[0].X},{(int)sleeve[0].Y} Q {(int)sleeve[5].X},{(int)sleeve[5].Y} {(int)sleeve[1].X},{(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[0].X},{(int)sleeve[0].Y} Q {(int)sleeve[6].X},{(int)sleeve[6].Y} {(int)sleeve[3].X},{(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[2].X},{(int)sleeve[2].Y} Q {(int)sleeve[10].X},{(int)sleeve[10].Y} {(int)sleeve[1].X},{(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[4].X},{(int)sleeve[4].Y} Q {(int)sleeve[9].X},{(int)sleeve[9].Y} {(int)sleeve[3].X},{(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                ";
            }    

            return s;
        }
    }
}
