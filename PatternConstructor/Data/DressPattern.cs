using iTextSharp.text.pdf.parser;
using PatternConstructor.Data.Enum;
using PatternConstructor.ViewModels;
using System.Numerics;

namespace PatternConstructor.Data
{
    public class DressPattern : Pattern
    {
        float boardwidth = 1.5f;
        float standcollarwidth = 3;
        string s;
        List<Vector2> back = new List<Vector2>();
        List<Vector2> front = new List<Vector2>();

        List<Vector2> skirtback = new List<Vector2>();
        List<Vector2> skirtfront = new List<Vector2>();

        List<Vector2> sleeve = new List<Vector2>();
        List<Vector2> collar = new List<Vector2>();
        List<Vector2> cuff = new List<Vector2>();

        List<Vector2> backD = new List<Vector2>();
        List<Vector2> frontD = new List<Vector2>();

        List<Vector2> skirtbackD = new List<Vector2>();
        List<Vector2> skirtfrontD = new List<Vector2>();

        List<Vector2> sleeveD = new List<Vector2>();
        List<Vector2> collarD = new List<Vector2>();
        List<Vector2> cuffD = new List<Vector2>();
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
            di,
            oz;
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
        bool isLecal;
        public DressPattern(BasicDressConstructModel model)
        {
            s = "";
            pg = (float)model.pg;
            pshs = (float)model.pshs;
            pshp = (float)model.pshp;
            ppr = pg-pshs-pshp;
            pt = (float)model.pt;
            pb = (float)model.pb;
            pdts = (float)model.pdts;
            pspr = (float)model.pspr;
            pshgor = (float)model.pshgor;
            pop = 0;
                
            csh = (float)model.NeckGirth / 2;
            cg1 = (float)model.BustGirthUp / 2;
            cg2 = (float)model.BustGirthSecond / 2;
            cg3 = (float)model.BustGirth / 2;
            ct = (float)model.WaistGirth / 2;
            cb = (float)model.HipsGirth / 2;
            dts = (float)model.BackWaistLength;
            vg = (float)model.BustHeight;
            vpk = (float)model.ShoulderHeight;
            dtp = (float)model.FrontWaistLength;
            shs = (float)model.BackWidth / 2;
            shg = (float)model.BustWidth / 2;
            shp = (float)model.ShoulderToNeck;
            vprz = (float)model.BackArmholeDepth;
            tsg = (float)model.BustCenter / 2;
            dr = 0;
            op = 0;
            di = (float)model.di;
            oz = 0;

           
            neckType = "Стандартная";
            collartype = "Без воротника";
            sleevetype = "Без рукава";
            clasptype = "Без застежки";
            waisttype = "Неотрезное по талии";
            isLecal = false;
        }
        public DressPattern(DressConstructModel model)
        {
            s = "";
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
            oz = (float)model.WristGirth;

            neckType = model.DressCombinationModel.Neck;
            collartype = model.DressCombinationModel.Collar;
            sleevetype = model.DressCombinationModel.Sleeve;
            clasptype = model.DressCombinationModel.Clasp;
            waisttype = model.DressCombinationModel.Waist;
            isLecal = model.DressCombinationModel.DoubleContour;
        }
        private void BackFront(float a, float a1, float a2, Vector2 A1)
        {
            float g = vprz + pspr;
            float t = dts + pdts;
            float b = t + 0.5f * dts - 2;

            float II1 = 2;
            float A2I = 4;
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
            I.X = (float)Math.Sqrt(A2I * A2I - I.Y * I.Y);

            Vector2 I1;
            I1.Y = (A2I + II1) * P1.Y / P1.X;
            I1.X = (float)Math.Sqrt((A2I + II1) * (A2I + II1) - I1.Y * I1.Y);
            I += back[3];
            I1 += back[3];

            Vector2 I2;
            I2.X = I.X;
            I2.Y = I.Y + 7;
            float d = Vector2.Distance(I1, I2);
            float dy = (I2.Y - I1.Y) * 7 / d;
            float dx = (float)Math.Sqrt(49 - dy * dy);

            back.Add(I);
            back.Add(I2);
            back.Add(new Vector2(I2.X + dx, I2.Y - dy));


            float G3P2 = vprz + pspr - back[4].Y;
            float P3G3 = G3P2 / 3 + 2;
            back.Add(new Vector2(a, vprz + pspr - P3G3));

            float G3G4 = a2 - a;
            back.Add(new Vector2((a2 + a) / 2, g));

            float vG3 = 0.2f * G3G4 + 0.5f;
            vG3 /= (float)Math.Sqrt(2);
            back.Add(new Vector2(a + vG3, g - vG3));


            Vector2 G6 = new Vector2(a1 - tsg - 0.5f, g);
            Vector2 A3 = new Vector2(a1, t - dtp - pdts);
            front.Add(new Vector2(a1, A3.Y + back[3].X + 1)); //A5
            front.Add(new Vector2(a1 - back[3].X, A3.Y)); // A4
            float G7y = front[1].Y + (float)Math.Sqrt(vg * vg - (G6.X - front[1].X) * (G6.X - front[1].X));
            front.Add(new Vector2(G6.X, G7y)); //G7
            front.Add(back[9]);//G3

            float cG4 = (float)(0.2 * G3G4 / Math.Sqrt(2));
            front.Add(new Vector2(a2 - cG4, g - cG4)); //c

            float P4y = back[4].Y + 1;
            front.Add(new Vector2(a2, P4y + 2f / 3f * (g - P4y))); //P6

            arr = CirclesIntersec(front[2], vg, front[1], 2 * (cg2 - cg1) + 2);
            Vector2 A9;
            if (arr[0].X < arr[1].X) A9 = arr[0];
            else A9 = arr[1];

            arr = CirclesIntersec(front[5], front[5].Y - P4y, A9, shp);
            Vector2 P5;
            if (arr[0].X < arr[1].X) P5 = arr[0];
            else P5 = arr[1];

            front.Add(P5); //P5

            Vector2 D = (front[6] + front[5]) / 2;
            float sin = (front[5].Y - front[6].Y) / Vector2.Distance(front[6], front[5]);
            float c = (front[5].X - front[6].X) / Vector2.Distance(front[6], front[5]);
            front.Add(new Vector2(D.X + sin, D.Y - c)); //e

            Vector2 DE = front[7] - D;
            DE += front[6];
            DE = (DE + front[7]) / 2;
            DE = (DE + front[7]) / 2;

            float d1 = Vector2.Distance(back[3], back[5]);
            float P5P7 = Vector2.Distance(A9, P5) - d1;

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

            float gbdif = ((cb + pb) - (cg3 + pg)) / 2;
            skirtback.Add(new Vector2(back[9].X + gbdif, b)); //Б3
            skirtfront.Add(new Vector2(front[3].X - gbdif, b)); // Б4
            skirtback.Add(new Vector2(skirtback[1].X, di)); //H3
            skirtfront.Add(new Vector2(skirtfront[1].X, di)); // H4
            skirtback.Add(new Vector2(0, di)); //H

            float FRONT_DIF = 1;
            skirtfront.Add(new Vector2(front[0].X, di + FRONT_DIF)); // H11

            skirtback.Add(back[1]); //T
            skirtfront.Add(new Vector2(front[0].X, t + FRONT_DIF)); // T11

            float T5 = (back[1].X + back[11].X) / 2;
            back.Add(new Vector2(T5 - sb / 6, t));
            back.Add(new Vector2(T5, g + 3));
            back.Add(new Vector2(T5 + sb / 6, t));
            skirtback.Add(back[12]);
            skirtback.Add(new Vector2(T5, b - 5));
            skirtback.Add(back[14]);

            front.Add(skirtfront[4]);
            Vector2 T6 = Intersec(front[2], G6, front[10], front[11]);
            front.Add(new Vector2(T6.X + sb / 12, T6.Y));
            front.Add(new Vector2(T6.X, front[2].Y + 3));
            front.Add(new Vector2(T6.X - sb / 12, T6.Y));
            skirtfront.Add(front[12]);
            skirtfront.Add(new Vector2(T6.X, b - 5));
            skirtfront.Add(front[14]);


            front.Add(DE);


            if (neckType == "V-горловина")
            {
                back[3] += (back[4] - back[3]) / Vector2.Distance(back[3], back[4]);
                front[1] += (front[9] - front[1]) / Vector2.Distance(front[9], front[1]);
                front[0] = new Vector2(front[0].X, g - 2);
            }
            else if (neckType == "Круглая")
            {
                back[3] += (back[4] - back[3]) / Vector2.Distance(back[3], back[4]) * 2;
                front[0] += 2 * Vector2.UnitY;
                front[1] += (front[9] - front[1]) / Vector2.Distance(front[9], front[1]) * 2;
            }
            else if (neckType == "Стандартная" && (collartype == "Отложной с круглыми концами" || collartype == "Отложной с прямыми углами"))
            {
                back[3] += (back[4] - back[3]) / Vector2.Distance(back[3], back[4]);
                back[0] += 0.5f*Vector2.UnitY;
                front[1] += (front[9] - front[1]) / Vector2.Distance(front[9], front[1]);
                front[0] += 1.5f * Vector2.UnitY;
            }
            else if (collartype == "Стойка с застежкой")
            {
                back[3] += (back[4] - back[3]) / Vector2.Distance(back[3], back[4]);
                front[1] += (front[9] - front[1]) / Vector2.Distance(front[9], front[1]);
                front[0] +=  Vector2.UnitY;
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

            if (clasptype== "Застежка на пуговицы до талии")
            {
                front.Add(front[0]);//17
                front.Add(front[11]);//18

                front.Add(front[0] + Vector2.UnitX * boardwidth);//19
                front.Add(front[11] + Vector2.UnitX * boardwidth);//20
                front[0] += Vector2.UnitX*boardwidth*3;
                front[11] += Vector2.UnitX*boardwidth*3;

            }
            if (clasptype == "Застежка на пуговицы до низа")
            {
                front.Add(front[0]); //19
                front.Add(front[11]); //20
                front.Add(front[0] + Vector2.UnitX * boardwidth);//21
                front.Add(front[11] + Vector2.UnitX * boardwidth);//22

                front[0] += Vector2.UnitX * boardwidth * 3;
                front[11] += Vector2.UnitX * boardwidth * 3;
                skirtfront.Add(skirtfront[4]);//8
                skirtfront.Add(skirtfront[3]);//9
                skirtfront.Add(skirtfront[4] + Vector2.UnitX * boardwidth);//10
                skirtfront.Add(skirtfront[3] + Vector2.UnitX * boardwidth);//11
                skirtfront[4] += Vector2.UnitX * boardwidth * 3;
                skirtfront[3] += Vector2.UnitX * boardwidth * 3;
            }
        }
        private void Sleeve(float a2,float a)
        {
            Vector2 O = (back[4] + front[6]) / 2;
            sleeve.Add(new Vector2(O.X, O.Y + 2f)); //O2 0
            float shruk = (op + pop) / 2;
            //sleeve.Add(new Vector2(sleeve[0].X + shruk/2 + 1f, front[5].Y+0.5f)); //P62
            sleeve.Add(new Vector2(sleeve[0].X + shruk / 2 + 0.5f, front[5].Y + 0.5f)); //P62 1 
            sleeve.Add(new Vector2(sleeve[0].X + shruk, back[9].Y)); //P2 2
            sleeve.Add(new Vector2(sleeve[0].X - shruk / 2 - 1f, back[8].Y + 0.5f)); //P31 3
            sleeve.Add(new Vector2(sleeve[0].X - shruk, back[9].Y)); //P1 4
            sleeve.Add(new Vector2(sleeve[0].X + shruk / 4 + 2, sleeve[0].Y)); // O5 5
            sleeve.Add(new Vector2(sleeve[0].X - shruk / 4, sleeve[0].Y)); // O6 6
            sleeve.Add(new Vector2((float)(sleeve[5].X - 2.25 * Math.Sqrt((1 - (sleeve[1].X - sleeve[5].X) / Vector2.Distance(sleeve[5], sleeve[1])) / 2)),
                (float)(sleeve[5].Y + 2.25 * Math.Sqrt((1 + (sleeve[1].X - sleeve[5].X) / Vector2.Distance(sleeve[5], sleeve[1])) / 2)))); // O51 7
            sleeve.Add(new Vector2((float)(sleeve[6].X + 1.5 * Math.Sqrt((1 - (sleeve[6].X - sleeve[3].X) / Vector2.Distance(sleeve[3], sleeve[6])) / 2)),
                (float)(sleeve[6].Y + 1.5 * Math.Sqrt((1 + (sleeve[6].X - sleeve[3].X) / Vector2.Distance(sleeve[3], sleeve[6])) / 2)))); // O61 8
            Vector2 Six = (sleeve[3] + sleeve[4]) / 2;
            float sin = (float)((sleeve[4].Y - sleeve[3].Y) / Vector2.Distance(sleeve[3], sleeve[4]));
            float c = (float)((-sleeve[4].X + sleeve[3].X) / Vector2.Distance(sleeve[3], sleeve[4]));
            sleeve.Add(new Vector2(Six.X + 1.2f * sin, Six.Y + 1.2f * c)); // 7 9
            float G3G4 = a2 - a;
            float cG4 = (float)((0.2 * G3G4 + 1) / Math.Sqrt(2));
            sleeve.Add(new Vector2(sleeve[0].X + shruk / 2 + cG4, sleeve[2].Y - cG4)); // P81 10
            Vector2 L = new Vector2(sleeve[0].X, sleeve[0].Y + dr / 2 + 3);
            if (sleevetype == "Короткий")
            {
                sleeve.Add(new Vector2(sleeve[0].X, sleeve[0].Y + (L.Y - sleeve[0].Y) / 3 * 2)); //11
                sin = 10.25f; //это тангенс донт майнд зе нейм
                float slevelen = sleeve[11].Y - sleeve[4].Y + 0.5f; 
                sleeve.Add(new Vector2(sleeve[4].X + slevelen / sin, sleeve[4].Y + slevelen)); //12
                sleeve.Add(new Vector2(sleeve[2].X - slevelen / sin, sleeve[2].Y + slevelen)); //13
                sleeve.Add(new Vector2(sleeve[3].X + 0.5f, sleeve[3].Y)); //14
                sleeve.Add(new Vector2(sleeve[1].X - 0.5f, sleeve[1].Y)); //15
            }
            else if (sleevetype == "Епископ с резинкой")
            {
                sleeve.Add(new Vector2(sleeve[1].X - 0.5f, sleeve[1].Y));
                sleeve.Add(new Vector2(sleeve[3].X + 0.5f, sleeve[3].Y));
                sleeve.Add(new Vector2(sleeve[2].X - 0.5f, sleeve[0].Y + dr));
                sleeve.Add(new Vector2(sleeve[4].X + 0.5f, sleeve[0].Y + dr));
                List<int> list = new List<int>() { 1, 2, 13, 11, 5, 10 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], 0.174533, sleeve[0]);
                list = new List<int>() { 10, 2, 13, 11 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], 0.174533, sleeve[1]);
                list = new List<int>() { 6, 3, 12, 4, 14, 9 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], -0.174533, sleeve[0]);
                list = new List<int>() { 12, 4, 14, 9 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], -0.174533, sleeve[3]);

                sleeve.Add(new Vector2(sleeve[0].X, sleeve[0].Y + dr + 7)); //(15)
            }
            else if (sleevetype == "Епископ с манжетой")
            {
                sleeve.Add(new Vector2(sleeve[1].X - 0.5f, sleeve[1].Y));
                sleeve.Add(new Vector2(sleeve[3].X + 0.5f, sleeve[3].Y));

                sleeve.Add(new Vector2(sleeve[2].X - 0.5f, sleeve[0].Y + dr - 5));
                sleeve.Add(new Vector2(sleeve[4].X + 0.5f, sleeve[0].Y + dr - 5));
                List<int> list = new List<int>() { 1, 2, 13, 11, 5, 10 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], 0.174533, sleeve[0]);
                list = new List<int>() { 10, 2, 13, 11 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], 0.174533, sleeve[1]);
                list = new List<int>() { 6, 3, 12, 4, 14, 9 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], -0.174533, sleeve[0]);
                list = new List<int>() { 12, 4, 14, 9 };
                for (int i = 0; i < list.Count; i++)
                    sleeve[list[i]] = RotatedVector(sleeve[list[i]], -0.174533, sleeve[3]);
                sleeve.Add(new Vector2(sleeve[0].X, sleeve[0].Y + dr - 5 + 7)); //(15)
                cuff.Add(Vector2.Zero);
                cuff.Add(new Vector2(0, 10));
                cuff.Add(new Vector2(oz + 5, 10));
                cuff.Add(new Vector2(oz + 5, 0));
                cuff.Add(new Vector2(0, 5));
                cuff.Add(new Vector2(oz + 5, 5));
            }
        }
        private void Collar(Vector2 A1)
        {
            A1 -= back[3];
            collar.Add(back[1] - back[3]);//0
            collar.Add(back[0] - back[3]);//1
            collar.Add(back[18] - back[3]);//2
            collar.Add(back[3] - back[3]);//3
            collar.Add(back[15] - back[3]);//4

            collar.Add(front[9] - front[1]);//5
            collar.Add(front[1] - front[1]);//5
            if (clasptype.Contains("Застежка"))
                collar.Add(front[17] - front[1]);//7
            else
                collar.Add(front[0] - front[1]);//7

            float sina = (collar[5].Y - collar[6].Y) / Vector2.Distance(collar[5], collar[6]);
            float cosa = (collar[6].X - collar[5].X) / Vector2.Distance(collar[5], collar[6]);

            float sinb = (collar[4].Y - collar[3].Y) / Vector2.Distance(collar[4], collar[3]);
            float cosb = (collar[4].X - collar[3].X) / Vector2.Distance(collar[4], collar[3]);

            float sin5 = (float)Math.Sin(5 * Math.PI / 180);
            float cos5 = (float)Math.Cos(5 * Math.PI / 180);

            float sin = -((sina * cosb + sinb * cosa)*cos5+sin5*(cosa*cosb-sina*sinb));
            float cos = -((cosa * cosb - sinb * sina)*cos5-sin5*(sina*cosb+sinb*cosa));

            for (int i = 0; i < 5; i++)
                collar[i] = RotatedVector(collar[i], sin, cos, collar[3]);
            A1 = RotatedVector(A1, sin, cos, collar[3]);


                collar.Add(Intersec(collar[3], collar[3] + Vector2.UnitY, collar[1], A1)); //точка для рисования горловины спинки (8)
                float collarwidth = 7;
                collar.Add(collar[7] + (collarwidth) * Vector2.UnitY);//9
                                                                        //collar.Add(collar[3]-collarwidth*Vector2.UnitX);//10
            collar.Add(Normal(collar[3], collar[8], true) * collarwidth + collar[3]);

            collar.Add((collar[0] - collar[1])/ Vector2.Distance(collar[0], collar[1])*(collarwidth)+collar[1]); //11

            collar.Add(Intersec(collar[10], collar[10] + Vector2.UnitY, collar[11], collar[11] + collar[8] - collar[1])); //12

            if (collartype == "Отложной с прямыми углами")
            {
                //collar[9] -= 0.5f * collarwidth * Vector2.UnitX;
                //float d = (collar[7].X - collar[10].X - collarwidth*0.5f)/ (float)Math.Sqrt(3);
                //collar.Add(new Vector2(collar[10].X, collar[9].Y-d));//13
                collar[9] = RotatedVector(collar[9],-0.5f,(float)Math.Sqrt(3)/2, collar[7]);
                collar.Add(new Vector2(collar[10].X, collar[9].Y));
                if (neckType=="V-горловина")
                {
                    collar[9] = Intersec(collar[7], collar[9], collar[10], Normal(collar[3], collar[7],true)*collarwidth+ collar[7]);
                }
            }


        }
        private void StandCollar()
        {
            float l1 = BezierQLength(front[1],new Vector2(front[1].X, front[17].Y), front[17]);
            float l2 = BezierQLength(back[3], back[18], back[0]);
            float total = l1 + l2 + boardwidth;
            collar.Add(Vector2.Zero);//0
            collar.Add(new Vector2(0, standcollarwidth));//1
            collar.Add(new Vector2(total/3, standcollarwidth));//2
            collar.Add(new Vector2(total / 3, 0)); // 3
            collar.Add(new Vector2(total, standcollarwidth)); //4
            collar.Add(new Vector2(total-boardwidth, 0)); //5
            collar.Add(new Vector2(total, 0)); //6
            float sin = 3/total;
            float cos = (float)Math.Sqrt(1 - sin * sin);
            collar[4] = RotatedVector(collar[4], sin, cos, collar[2]);
            collar[5] = RotatedVector(collar[5], sin, cos, collar[2]);
            collar[6] = RotatedVector(collar[6], sin, cos, collar[2]);
        }
        private void FrontD()
        {
            if (!clasptype.Contains("Застежка"))
            {
                frontD.Add(front[0] - Vector2.UnitY);//0
            }
            else
            {
                frontD.Add(front[17] - Vector2.UnitY);//0
            }
            frontD.Add(front[1] + Vector2.UnitX); //1
            frontD.Add(Normal(front[1], front[9], false) + front[1]); //2
            frontD.Add(frontD[2] + Vector2.UnitX);//3
            frontD.Add(Normal(front[1], front[9], false) + front[9]); //4
            frontD[4] = Intersec(frontD[4], frontD[2], front[2], front[9]);//4
            frontD.Add(Normal(front[6], front[8],false) + front[8]); //5
            frontD.Add(Normal(front[6], front[8], false) + front[6]); //6
            frontD[5] = Intersec(frontD[5], frontD[6], front[2], front[8]);//5
            frontD.Add(Normal(front[6], frontD[6], true) + frontD[6]); //7
            frontD.Add(Normal(front[6], frontD[6], true) + front[6]); //8
            frontD.Add(Normal(front[7], front[15], true) + front[7]); //9
            frontD.Add(Normal(front[7], front[15], true) + front[15]); //10
            frontD.Add(front[5] - Vector2.UnitX);//11
            frontD.Add(front[4] - Vector2.One / (float)Math.Sqrt(2));//12
            frontD.Add(front[3] - Vector2.UnitY);//13
            frontD.Add(Normal(front[3], front[10], true) + front[3]); //14
            frontD.Add(Normal(front[3], front[10], true) + front[10]); //15
            frontD.Add(Intersec(frontD[14], frontD[15], frontD[13], frontD[13] + Vector2.UnitX));//16 возодно поменять
            frontD[16] = new Vector2(frontD[14].X, frontD[13].Y);
            frontD[15] = Intersec(frontD[14], frontD[15], front[10], front[10] + Vector2.UnitX); //15

            frontD.Add(Normal(front[10], front[14],true) + front[10]);//17
            frontD.Add(Normal(front[10], front[14], true) + front[14]);//18
            frontD[17] = Intersec(frontD[14], frontD[15], frontD[18], frontD[17]);//17
            frontD[18] = Intersec(front[14], front[13], frontD[17], frontD[18]);//18

            if (!clasptype.Contains("Застежка"))
            {
                frontD.Add(Normal(front[11], front[12], true) + front[12]);//19
                frontD.Add(Normal(front[11], front[12], true) + front[11]);//20
                frontD[19] = Intersec(front[12], front[13], frontD[19], frontD[20]);//19
                frontD.Add(front[11] + Vector2.UnitX);//21
                frontD.Add(front[11] + Vector2.One);//22
                frontD.Add(frontD[0] + Vector2.UnitX);//23
            }
            else
            {
                frontD.Add(Normal(front[18], front[12], true) + front[12]);//19
                frontD.Add(Normal(front[18], front[12], true) + front[18]);//20
                frontD[19] = Intersec(front[12], front[13], frontD[19], frontD[20]);//19
                frontD.Add(front[11]);//21
                frontD.Add(front[11] + Vector2.UnitY);//22
                frontD.Add(front[0] - Vector2.UnitY);//23
            }
            Vector2 middle = (frontD[4] + frontD[5]) / 2;
            frontD.Add(Intersec(frontD[6], frontD[5], front[2], middle));//24
            middle = (frontD[19] + frontD[18]) / 2;
            frontD.Add(Intersec(frontD[18], frontD[17], front[13], front[13]+Vector2.UnitY));//25
            if (!clasptype.Contains("Застежка"))
            {
                frontD.Add(new Vector2(front[11].X, frontD[22].Y));//26
            }
            else
            {
                frontD.Add(new Vector2(front[18].X, frontD[22].Y));//26
            }
            if (neckType=="V-горловина")
            {
                Vector2 righttop = front[0];
                if (clasptype.Contains("Застежка"))
                    righttop = front[17];
                Vector2 n1 = Normal(front[1], righttop, false) + front[1];
                Vector2 n2 = Normal(front[1], righttop, false) + righttop;
                if (clasptype == "Без застежки")
                    frontD[0] = Intersec(frontD[0], frontD[26], n1, n2);
                else
                    frontD[0] = Intersec(frontD[0], frontD[23], n1, n2);
            }
        }
        private void BackD()
        {
            backD.Add(back[0] - Vector2.One);//0
            backD.Add(back[1] - Vector2.UnitX + Vector2.UnitY);//1
            backD.Add(back[1] + Vector2.UnitY);//2
            backD.Add(back[11] + Vector2.UnitY); //3
            backD.Add(Normal(back[11], back[9], true) + back[11]);//4
            backD.Add(Normal(back[11], back[9], true) + back[9]);//5
            backD.Add(Intersec(backD[1], backD[2], backD[5], backD[4]));//6
            backD[4] = Intersec(backD[4], backD[5], back[1], back[11]);//4
            backD.Add(back[9]-Vector2.UnitY);//7
            backD.Add(Intersec(backD[7], backD[7] + Vector2.UnitX, backD[5], backD[5]+Vector2.UnitY));//8
            backD.Add(new Vector2(back[10].X + 1 / (float)Math.Sqrt(2), back[10].Y-1/(float)Math.Sqrt(2)));//9
            backD.Add(back[8] + Vector2.UnitX);//10
            backD.Add(Normal(back[4], back[16], false) + back[4]);//11
            backD.Add(Normal(back[4], backD[11], true) + back[4]);//12
            backD.Add(Normal(back[4], backD[11], true) + backD[11]);//13

            backD.Add(Normal(back[4], back[16], false) + back[16]);//14
            backD[14] = Intersec(backD[14], backD[11], back[6], back[16]);//14
            backD.Add(Normal(back[3], back[15], false) + back[15]);//15
            backD.Add(Normal(back[3], back[15], false) + back[3]);//16
            backD[15] = Intersec(backD[15], backD[16], back[6], back[15]);//15
            Vector2 middle = (backD[14] + backD[15]) / 2;
            backD.Add(Intersec(backD[14], backD[11], back[6], middle));//17

            backD.Add(Normal(back[3], back[18], false) + back[3]);//18
            backD.Add(back[0] - Vector2.UnitY);//19
            //backD.Add(Normal(back[3], back[18], false) + back[18]);//20
            //backD[20] = Intersec(backD[0], backD[19], backD[18], backD[20]);//20
            backD.Add(back[18] - Vector2.One);
            backD.Add(back[1] - Vector2.UnitX);//21

            backD.Add(backD[10] - 2* Vector2.UnitY + (backD[13].Y - backD[11].Y) * Vector2.UnitY);//22

        }
        private void SkirtFrontD()
        {
            if (!clasptype.Contains("Застежка на пуговицы до низа"))
            {
                skirtfrontD.Add(skirtfront[4] - Vector2.UnitY);//0
            }
            else
            {
                skirtfrontD.Add(skirtfront[8] - Vector2.UnitY);//0
            }
            skirtfrontD.Add(skirtfront[4] + Vector2.UnitX);//1
            skirtfrontD.Add(skirtfrontD[1] - Vector2.UnitY);//2

            skirtfrontD.Add(skirtfront[3] + Vector2.One);//3

            if (!clasptype.Contains("Застежка на пуговицы до низа"))
            {
                skirtfrontD.Add(skirtfront[3] + Vector2.UnitY);//4
            }
            else
            {
                skirtfrontD.Add(skirtfront[9] + Vector2.UnitY);//4
            }
            skirtfrontD.Add(skirtfront[2] - Vector2.UnitX + Vector2.UnitY);//5
            skirtfrontD.Add(Normal(skirtfront[1], skirtfront[0], false) + skirtfront[1]);//6
            skirtfrontD.Add(Normal(skirtfront[1], skirtfront[0], false) + skirtfront[0]);//7
            skirtfrontD[6] = Intersec(skirtfrontD[5], skirtfrontD[5] + Vector2.UnitY, skirtfrontD[6], skirtfrontD[7]);//6
            skirtfrontD[7] = Intersec(skirtfront[0], skirtfront[0] + Vector2.UnitX, skirtfrontD[6], skirtfrontD[7]);//7
            skirtfrontD.Add(Normal(skirtfront[0], skirtfront[7],false) + skirtfront[0]);//8
            skirtfrontD.Add(Normal(skirtfront[0], skirtfront[7], false) + skirtfront[7]);//9
            skirtfrontD.Add(Intersec(skirtfrontD[6], skirtfrontD[7], skirtfrontD[8], skirtfrontD[9]));//10
            if (!clasptype.Contains("Застежка на пуговицы до низа"))
            {
                skirtfrontD.Add(Normal(skirtfront[5], skirtfront[4], false) + skirtfront[5]);//11
                skirtfrontD.Add(Normal(skirtfront[5], skirtfront[4], false) + skirtfront[4]);//12
            }
            else
            {
                skirtfrontD.Add(Normal(skirtfront[5], skirtfront[8], false) + skirtfront[5]);//11
                skirtfrontD.Add(Normal(skirtfront[5], skirtfront[8], false) + skirtfront[8]);//12
            }
            skirtfrontD[12] = Intersec(skirtfrontD[11], skirtfrontD[12], skirtfrontD[0], skirtfrontD[2]);
            skirtfrontD[9] = Intersec(skirtfrontD[8], skirtfrontD[9], skirtfront[6], skirtfront[7]);
            skirtfrontD[11] = Intersec(skirtfrontD[11], skirtfrontD[12], skirtfront[6], skirtfront[5]);
            Vector2 middle = (skirtfrontD[9] + skirtfrontD[11]) / 2;
            skirtfrontD.Add(Intersec(skirtfrontD[12], skirtfrontD[11], skirtfront[6], middle));//13

            if (clasptype!= "Центральный шов полочки")
            {
                skirtfrontD[2] -= Vector2.UnitX;
                skirtfrontD[1] -= Vector2.UnitX;
                skirtfrontD[3] -= Vector2.UnitX;
            }

        }
        private void SkirtBackD()
        {
            skirtbackD.Add(skirtback[4] - Vector2.One);//0
            skirtbackD.Add(skirtback[4] - Vector2.UnitX);//1
            skirtbackD.Add(skirtback[3] - Vector2.UnitX + Vector2.UnitY);//2
            skirtbackD.Add(skirtback[3] + Vector2.UnitY);//3
            skirtbackD.Add(skirtback[2] + Vector2.One);//4
            skirtbackD.Add(Normal(skirtback[1], skirtback[0], false) + skirtback[1]);//5
            skirtbackD.Add(Normal(skirtback[1], skirtback[0], false) + skirtback[0]);//6
            skirtbackD.Add(Intersec(skirtbackD[5], skirtbackD[6], skirtbackD[4], skirtbackD[4]+Vector2.UnitY));//7
            skirtbackD.Add(Intersec(skirtbackD[5], skirtbackD[6], skirtbackD[0], skirtbackD[0] + Vector2.UnitX));//8
            skirtbackD.Add(skirtbackD[0] + Vector2.UnitX);//9

            //skirtbackD[5] = skirtback[1] + Vector2.UnitX;
            //skirtbackD[6] = skirtback[0] + Vector2.UnitX;
            //skirtbackD[7] = skirtbackD[5];
            //skirtbackD[8] = skirtbackD[6] - Vector2.UnitY;
            //skirtbackD.Add(new Vector2(skirtbackD[6].X, skirtback[0].Y));//10
            skirtbackD.Add(Intersec(skirtbackD[5], skirtbackD[6], skirtbackD[1], skirtbackD[1] + Vector2.UnitX));

            //skirtbackD[7]=Intersec(skirtbackD[5], skirtbackD[10], skirtbackD[4], skirtbackD[4] + Vector2.UnitY);//7
            //skirtbackD[8]=(Intersec(skirtbackD[5], skirtbackD[10], skirtbackD[0], skirtbackD[0] + Vector2.UnitX));//8
            //Vector2 dif = new Vector2(skirtbackD[7].X - skirtbackD[5].X, 0);
            //skirtbackD[5] += dif;
            //skirtbackD[6] += dif;
            //skirtbackD[10] += dif;

        }
        private void SleeveD()
        {
            sleeveD.Add(sleeve[0] - Vector2.UnitY);//0
            sleeveD.Add(Normal(sleeve[1], sleeve[5], false) + sleeve[1]);//1
            sleeveD.Add(Intersec(sleeveD[0], sleeveD[0] + Vector2.UnitX, sleeveD[1], sleeveD[1] + (sleeve[1] - sleeve[5]))); //2
            sleeveD.Add(Normal(sleeve[1], sleeve[10], false) + sleeve[1]);//3
            sleeveD.Add(Normal(sleeve[10], sleeve[2], false) + sleeve[2]);//4
            sleeveD.Add(Intersec(sleeveD[3], sleeveD[3] + (sleeve[1] - sleeve[10]), sleeveD[4], sleeveD[4] + (sleeve[10] - sleeve[2]))); //5
            sleeveD.Add(Intersec(sleeveD[2], sleeveD[1], sleeveD[3], sleeveD[5])); //6

            sleeveD.Add(Normal(sleeve[3], sleeve[6], false) + sleeve[3]);//7
            sleeveD.Add(Intersec(sleeveD[0], sleeveD[0] + Vector2.UnitX, sleeveD[7], sleeveD[7] + (sleeve[3] - sleeve[6]))); //8
            sleeveD.Add(Normal(sleeve[3], sleeve[9], false) + sleeve[3]);//9
            sleeveD.Add(Normal(sleeve[4], sleeve[9], false) + sleeve[4]);//10
            sleeveD.Add(Intersec(sleeveD[10], sleeveD[10] + (sleeve[4] - sleeve[9]), sleeveD[9], sleeveD[9] + (sleeve[3] - sleeve[9]))); //11
            sleeveD.Add(Intersec(sleeveD[9], sleeveD[11], sleeveD[7], sleeveD[8])); //12

            if (sleevetype == "Короткий")
            {
                sleeveD.Add(Normal(sleeve[2], sleeve[13], true) + sleeve[2]);//13
                sleeveD.Add(Normal(sleeve[4], sleeve[12], true) + sleeve[4]);//14
                sleeveD.Add(sleeve[11] + Vector2.UnitY);//15
                Vector2 temp = sleeveD[15] + Vector2.UnitY * (sleeve[13].Y - sleeve[11].Y);
                sleeveD.Add(Intersec(sleeve[2], sleeve[13], temp, temp + Vector2.UnitX));//16
                sleeveD.Add(Normal(sleeve[2], sleeve[13], true) + sleeveD[16]);//17
                sleeveD.Add(new Vector2(sleeveD[16].X, sleeveD[15].Y));//18
                sleeveD[13] = Intersec(sleeveD[13], sleeveD[17], sleeveD[4], sleeveD[5]);

                sleeveD.Add(Intersec(sleeve[4], sleeve[12], temp, temp + Vector2.UnitX));//19
                sleeveD.Add(Normal(sleeve[4], sleeve[12], true) + sleeveD[19]);//20
                sleeveD.Add(new Vector2(sleeveD[19].X, sleeveD[15].Y));//21
                sleeveD[14] = Intersec(sleeveD[14], sleeveD[20], sleeveD[10], sleeveD[11]); //14

            }
            else if (sleevetype.Contains("Епископ"))
            {
                sleeveD.Add(Normal(sleeve[2], sleeve[13], false) + sleeve[2]);//13
                sleeveD.Add(Normal(sleeve[4], sleeve[14], false) + sleeve[4]);//14

                sleeveD.Add(Normal(sleeve[13], sleeve[15], true) + sleeve[13]); //15
                sleeveD.Add(Normal(sleeve[13], sleeve[15], true) + sleeve[15]); //16
                sleeveD.Add(Normal(sleeve[14], sleeve[15], true) + sleeve[14]); //17

                sleeveD.Add(Normal(sleeve[2], sleeve[13], false) + sleeve[13]);//18
                sleeveD.Add(Normal(sleeve[4], sleeve[14], false) + sleeve[14]);//19

                sleeveD.Add(Normal(sleeve[14], sleeve[15], true) + sleeve[15]); //20

                sleeveD[16] = Intersec(sleeveD[15],sleeveD[16],sleeveD[17],sleeveD[20]);

                sleeveD[13] = Intersec(sleeveD[13], sleeveD[18], sleeveD[4], sleeveD[5]);
                sleeveD[14] = Intersec(sleeveD[14], sleeveD[19], sleeveD[10], sleeveD[11]);
                if (cuff.Count != 0)
                {
                    cuffD.Add(cuff[0] - Vector2.One);
                    cuffD.Add(cuff[1] + Vector2.UnitY - Vector2.UnitX);
                    cuffD.Add(cuff[2] + Vector2.One);
                    cuffD.Add(cuff[3] + Vector2.UnitX - Vector2.UnitY);
                }
            }
        }
        private void CollarD()
        {
            collarD.Add(collar[3] + Vector2.UnitX);//0
            collarD.Add((collar[1] - collar[11]) / Vector2.Distance(collar[1], collar[11]) + collar[1]); //1
            collarD.Add(Intersec(collarD[0], collarD[0] + Vector2.UnitY, collarD[1], collarD[1] + (collar[1] - collar[8])));//2
            collarD.Add((collar[11] - collar[1]) / Vector2.Distance(collar[1], collar[11]) + collar[11]); //3
            collarD.Add(collar[10] - Vector2.UnitX);//4
            collarD.Add(Intersec(collarD[4], collarD[4] + Vector2.UnitY, collarD[3], collarD[3] + (collar[1] - collar[8])));//5
            collarD.Add(collar[13] + Vector2.UnitY - Vector2.UnitX);//6
            collarD.Add(collar[9] + Vector2.UnitY);//7
            collarD.Add(Normal(collar[9], collar[7], true) + collar[9]);//8
            collarD.Add(Normal(collar[9], collar[7], true) + collar[7]);//9
            collarD[8] = Intersec(collarD[6], collarD[7], collarD[8], collarD[9]);
            collarD.Add(collar[7] - Vector2.UnitY);//10
            collarD.Add(new Vector2(collarD[0].X, collarD[10].Y));//11
            collarD.Add(new Vector2(collarD[9].X, collarD[10].Y));//12
            if (collartype == "Отложной с прямыми углами")
            {
                if (neckType == "V-горловина")
                {
                    collarD[10] = Normal(collar[3], collar[7], false) + collar[7];
                    collarD[6] = Normal(collar[10], collar[9], true) + collar[9];
                    collarD[8] = Intersec(collarD[6], collarD[4], collarD[8], collarD[9]);
                }
            }
        }
        private void StandCollarD()
        {
            float l1 = BezierQLength(front[1], new Vector2(front[1].X, front[17].Y), front[17]);
            float l2 = BezierQLength(back[3], back[18], back[0]);
            float collarwidth = 3;
            float total = l1 + l2 + boardwidth;
            collarD.Add(-Vector2.UnitY);//0
            collarD.Add(new Vector2(0, collarwidth+1));//1
            collarD.Add(new Vector2(total / 3, collarwidth+1));//2
            collarD.Add(new Vector2(total / 3, -1)); // 3
            collarD.Add(new Vector2(total, collarwidth+1)); //4
            collarD.Add(new Vector2(total - boardwidth, -1)); //5
            collarD.Add(new Vector2(total+1, -1)); //6
            collarD.Add(new Vector2(total + 1, collarwidth)); //7
            collarD.Add(new Vector2(total + 1, collarwidth+1)); //8

            float sin = 3 / total;
            float cos = (float)Math.Sqrt(1 - sin * sin);
            collarD[4] = RotatedVector(collarD[4], sin, cos, collar[2]);
            collarD[5] = RotatedVector(collarD[5], sin, cos, collar[2]);
            collarD[6] = RotatedVector(collarD[6], sin, cos, collar[2]);
            collarD[7] = RotatedVector(collarD[7], sin, cos, collar[2]);
            collarD[8] = RotatedVector(collarD[8], sin, cos, collar[2]);
        }
        private void Offset(List<Vector2> l, Vector2 offset)
        {
            for (int i = 0; i < l.Count; i++)
                l[i] = (float)pixelsizeincm * (l[i] + offset);
        }
        private void DrawBack()
        {
            s += @$"
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} Q {(int)back[18].X},{(int)back[18].Y} {(int)back[3].X},{(int)back[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[0].X} {(int)back[0].Y} L {(int)back[1].X} {(int)back[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[3].X} {(int)back[3].Y} L {(int)back[15].X} {(int)back[15].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[15].X} {(int)back[15].Y} L {(int)back[6].X} {(int)back[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[6].X} {(int)back[6].Y} L {(int)back[16].X} {(int)back[16].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[16].X} {(int)back[16].Y} L {(int)back[4].X} {(int)back[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[4].X} {(int)back[4].Y} C {(int)back[4].X},{(int)back[4].Y} {(int)back[8].X},{(int)(back[8].Y - 2 * pixelsizeincm)} {(int)back[8].X},{(int)back[8].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[8].X} {(int)back[8].Y} Q {(int)back[8].X},{(int)((back[10].Y + back[8].Y) / 2)} {(int)back[10].X},{(int)back[10].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[10].X} {(int)back[10].Y} Q {(int)((back[9].X + back[10].X) / 2)},{(int)back[9].Y} {(int)back[9].X},{(int)back[9].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)back[9].X} {(int)back[9].Y} L {(int)back[11].X} {(int)back[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[11].X} {(int)back[11].Y} L {(int)back[14].X} {(int)back[14].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[14].X} {(int)back[14].Y} L {(int)back[13].X} {(int)back[13].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[13].X} {(int)back[13].Y} L {(int)back[12].X} {(int)back[12].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[12].X} {(int)back[12].Y} L {(int)back[1].X} {(int)back[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)back[17].X} {(int)back[17].Y} L {(int)back[8].X} {(int)back[8].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
            if (clasptype == "Без застежки")
                s += $@"<text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((back[0].X + back[9].X) / 2)}, {(int)(back[13].Y-2*pixelsizeincm)})"" >Back, x2</text>";
            else
                s += $@"<text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((back[0].X + back[9].X) / 2)}, {(int)(back[13].Y - 2 * pixelsizeincm)})"" >Back, x1</text>";
        }
        private void DrawSkirtBack()
        {
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
            if (clasptype == "Без застежки")
                s += $@"<text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((skirtback[3].X + skirtback[1].X) / 2)}, {(int)((skirtback[4].Y + skirtback[3].Y)/2)})"" >Back skirt, x2</text>";
            else
                s += $@"<text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((skirtback[3].X + skirtback[1].X) / 2)}, {(int)((skirtback[4].Y + skirtback[3].Y) / 2)})"" >Back skirt, x1</text>";
        }
        private void DrawFront()
        {
            if (clasptype == "Без застежки" || clasptype == "Центральный шов полочки")
            {
                s += @$"
                    <path d=""M {(int)front[12].X} {(int)front[12].Y} L {(int)front[11].X} {(int)front[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    ";
                if (neckType == "V-горловина")
                    s += @$"
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} L {(int)front[1].X},{(int)front[1].Y}""  stroke-width=""3"" stroke=""black"" />
                    ";
                else
                    s += @$"
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} Q {(int)front[1].X},{(int)front[0].Y} {(int)front[1].X},{(int)front[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    ";
            }
            else if (clasptype == "Застежка на пуговицы до талии" || clasptype== "Застежка на пуговицы до низа")
            {
                s += @$"
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} L {(int)front[11].X},{(int)front[11].Y}""  stroke-width=""3"" stroke=""black"" />
                    <path d=""M {(int)front[0].X} {(int)front[0].Y} L {(int)front[17].X},{(int)front[17].Y}""  stroke-width=""3"" stroke=""black"" />
                    <path d=""M {(int)front[11].X} {(int)front[11].Y} L {(int)front[18].X},{(int)front[18].Y}""  stroke-width=""3"" stroke=""black"" />
                    <path d=""M {(int)front[18].X} {(int)front[18].Y} L {(int)front[17].X},{(int)front[17].Y}""  stroke-width=""3"" stroke=""black"" />
                    <path d=""M {(int)front[19].X} {(int)front[19].Y} L {(int)front[20].X},{(int)front[20].Y}""  stroke-width=""3"" stroke=""black"" />
                    <path d=""M {(int)front[12].X} {(int)front[12].Y} L {(int)front[18].X} {(int)front[18].Y}""  stroke-width=""3"" stroke=""black""/>
                    ";
                if (neckType == "V-горловина")
                    s += @$"
                    <path d=""M {(int)front[1].X} {(int)front[1].Y} L {(int)front[17].X},{(int)front[17].Y}""  stroke-width=""3"" stroke=""black"" />
                    ";
                else
                    s += @$"
                    <path d=""M {(int)front[1].X} {(int)front[1].Y} Q {(int)front[1].X},{(int)front[17].Y} {(int)front[17].X},{(int)front[17].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    ";
            }
                s += @$"
                    <path d=""M {(int)front[1].X} {(int)front[1].Y} L {(int)front[9].X} {(int)front[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[9].X} {(int)front[9].Y} L {(int)front[2].X} {(int)front[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[2].X} {(int)front[2].Y} L {(int)front[8].X} {(int)front[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[8].X} {(int)front[8].Y} L {(int)front[6].X} {(int)front[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[6].X} {(int)front[6].Y} C {(int)front[6].X},{(int)front[6].Y} {(int)front[15].X},{(int)front[15].Y} {(int)front[7].X},{(int)front[7].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[7].X} {(int)front[7].Y} Q {(int)front[5].X},{(int)((front[7].Y + front[5].Y) / 2)} {(int)front[5].X},{(int)front[5].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[5].X} {(int)front[5].Y} Q {(int)front[5].X},{(int)((front[4].Y + front[5].Y) / 2)} {(int)front[4].X},{(int)front[4].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[4].X} {(int)front[4].Y} Q {(int)((front[4].X + front[3].X) / 2)},{(int)front[3].Y} {(int)front[3].X},{(int)front[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)front[3].X} {(int)front[3].Y} L {(int)front[10].X} {(int)front[10].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[10].X} {(int)front[10].Y} L {(int)front[14].X} {(int)front[14].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[14].X} {(int)front[14].Y} L {(int)front[13].X} {(int)front[13].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[13].X} {(int)front[13].Y} L {(int)front[12].X} {(int)front[12].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[11].X} {(int)front[11].Y} L {(int)front[0].X} {(int)front[0].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)front[5].X} {(int)front[5].Y} L {(int)front[16].X} {(int)front[16].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
            if (clasptype == "Без застежки")
                s += $"<text text-anchor=\"middle\" font-size=\"32\" font-family=\"Verdana\" transform=\"translate({(int)((front[3].X + front[11].X) / 2)}, {(int)(front[2].Y+pixelsizeincm)})\" >Front, x1</text>";
            else
                s += $"<text text-anchor=\"middle\" font-size=\"32\" font-family=\"Verdana\" transform=\"translate({(int)((front[3].X + front[11].X) / 2)}, {(int)(front[2].Y + pixelsizeincm)})\" >Front, x2</text>";
        }
        private void DrawSkirtFront()
        {
            s += @$"
                    <path d=""M {(int)skirtfront[0].X} {(int)skirtfront[0].Y} C {(int)skirtfront[0].X},{(int)(skirtfront[0].Y + pixelsizeincm)} {(int)skirtfront[1].X},{(int)(skirtfront[1].Y - pixelsizeincm * 5)} {(int)skirtfront[1].X},{(int)skirtfront[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfront[1].X} {(int)skirtfront[1].Y} L {(int)skirtfront[2].X} {(int)skirtfront[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[3].X} {(int)skirtfront[3].Y} L {(int)skirtfront[4].X} {(int)skirtfront[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[5].X} {(int)skirtfront[5].Y} L {(int)skirtfront[6].X} {(int)skirtfront[6].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[6].X} {(int)skirtfront[6].Y} L {(int)skirtfront[7].X} {(int)skirtfront[7].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[7].X} {(int)skirtfront[7].Y} L {(int)skirtfront[0].X} {(int)skirtfront[0].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
            if (clasptype == "Без застежки" || clasptype == "Центральный шов полочки" || clasptype == "Застежка на пуговицы до талии")
            {
                s += @$"
                    <path d=""M {(int)skirtfront[2].X} {(int)skirtfront[2].Y} C {(int)skirtfront[2].X},{(int)skirtfront[2].Y} {(int)(skirtfront[3].X - pixelsizeincm * 10)},{(int)skirtfront[3].Y} {(int)skirtfront[3].X},{(int)skirtfront[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfront[4].X} {(int)skirtfront[4].Y} L {(int)skirtfront[5].X} {(int)skirtfront[5].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
            }
            else if (clasptype == "Застежка на пуговицы до низа")
            {
                s += @$"
                    <path d=""M {(int)skirtfront[2].X} {(int)skirtfront[2].Y} C {(int)skirtfront[2].X},{(int)skirtfront[2].Y} {(int)(skirtfront[9].X - pixelsizeincm * 10)},{(int)skirtfront[9].Y} {(int)skirtfront[9].X},{(int)skirtfront[9].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfront[8].X} {(int)skirtfront[8].Y} L {(int)skirtfront[5].X} {(int)skirtfront[5].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[8].X} {(int)skirtfront[8].Y} L {(int)skirtfront[4].X} {(int)skirtfront[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[9].X} {(int)skirtfront[9].Y} L {(int)skirtfront[3].X} {(int)skirtfront[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[9].X} {(int)skirtfront[9].Y} L {(int)skirtfront[8].X} {(int)skirtfront[8].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)skirtfront[10].X} {(int)skirtfront[10].Y} L {(int)skirtfront[11].X} {(int)skirtfront[11].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
            }
            if (clasptype=="Без застежки")
                s += @$"
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((skirtfront[1].X + skirtfront[3].X) / 2)}, {(int)((skirtfront[4].Y + skirtfront[3].Y) / 2)})"" >Front skirt, x1</text>
                ";
            else
                s += $@"
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((skirtfront[1].X + skirtfront[3].X) / 2)}, {(int)((skirtfront[4].Y + skirtfront[3].Y) / 2)})"" >Front skirt, x2</text>";
        }
        private void DrawSleeve()
        {
            s += $@"
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((sleeve[4].X + sleeve[2].X) / 2)}, {(int)(sleeve[4].Y)})"" >Sleeve, x2</text>
            ";
            if (sleevetype == "Короткий")
            {
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

                    <path d=""M {(int)sleeve[3].X} {(int)sleeve[3].Y} L {(int)sleeve[14].X} {(int)sleeve[14].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[15].X} {(int)sleeve[15].Y} L {(int)sleeve[1].X} {(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black""/>
                ";
            }
            else if (sleevetype.Contains("Епископ"))
            {
                s += @$"
                    <path d=""M {(int)sleeve[14].X} {(int)sleeve[14].Y} L {(int)sleeve[4].X} {(int)sleeve[4].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[13].X} {(int)sleeve[13].Y} L {(int)sleeve[2].X} {(int)sleeve[2].Y}""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)sleeve[0].X} {(int)sleeve[0].Y} v {(int)(pixelsizeincm / 2)}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeve[1].X} {(int)sleeve[1].Y} L {(int)sleeve[11].X} {(int)sleeve[11].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)sleeve[3].X} {(int)sleeve[3].Y} L {(int)sleeve[12].X} {(int)sleeve[12].Y}""  stroke-width=""3"" stroke=""black""/>

                    <path d=""M {(int)sleeve[0].X},{(int)sleeve[0].Y} Q {(int)sleeve[5].X},{(int)sleeve[5].Y} {(int)sleeve[1].X},{(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[0].X},{(int)sleeve[0].Y} Q {(int)sleeve[6].X},{(int)sleeve[6].Y} {(int)sleeve[3].X},{(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[2].X},{(int)sleeve[2].Y} Q {(int)sleeve[10].X},{(int)sleeve[10].Y} {(int)sleeve[1].X},{(int)sleeve[1].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[4].X},{(int)sleeve[4].Y} Q {(int)sleeve[9].X},{(int)sleeve[9].Y} {(int)sleeve[3].X},{(int)sleeve[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeve[14].X},{(int)sleeve[14].Y} Q {(int)sleeve[15].X},{(int)sleeve[15].Y} {(int)sleeve[13].X},{(int)sleeve[13].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                ";
                if (cuff.Count != 0)
                    DrawCuff();
            }
            
        }
        private void DrawCuff()
        {
            s += $@"
                    <path d=""M {(int)cuff[0].X} {(int)cuff[0].Y} L {(int)cuff[1].X} {(int)cuff[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)cuff[1].X} {(int)cuff[1].Y} L {(int)cuff[2].X} {(int)cuff[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)cuff[2].X} {(int)cuff[2].Y} L {(int)cuff[3].X} {(int)cuff[3].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)cuff[3].X} {(int)cuff[3].Y} L {(int)cuff[0].X} {(int)cuff[0].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)cuff[4].X} {(int)cuff[4].Y} L {(int)cuff[5].X} {(int)cuff[5].Y}""  stroke-width=""1"" stroke=""black""/>

                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((cuff[1].X + cuff[2].X) / 2)}, {(int)((cuff[4].Y + cuff[1].Y) / 2)})"" >Cuff, x2, interfacing</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((cuff[1].X + cuff[2].X) / 2)}, {(int)(cuff[4].Y-1)})"" >Fold</text>
                ";
        }
        private void DrawCollar()
        {
            if (collartype == "Отложной с прямыми углами")
            {
                int deg = 180 + (int)(Math.Asin((collar[1].Y - collar[11].Y) / Vector2.Distance(collar[11], collar[1])) * 180 / Math.PI);
                if (neckType!= "V-горловина")
                s += $@"
                    <path d=""M {(int)collar[11].X} {(int)collar[11].Y} L {(int)collar[1].X} {(int)collar[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[1].X},{(int)collar[1].Y} Q {(int)collar[8].X},{(int)collar[8].Y} {(int)collar[3].X},{(int)collar[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[3].X},{(int)collar[3].Y} Q {(int)collar[3].X},{(int)collar[7].Y} {(int)collar[7].X},{(int)collar[7].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[7].X} {(int)collar[7].Y} L {(int)collar[9].X} {(int)collar[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[9].X},{(int)collar[9].Y} Q {(int)collar[13].X},{(int)collar[13].Y} {(int)collar[10].X},{(int)collar[10].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[10].X},{(int)collar[10].Y} Q {(int)collar[12].X},{(int)collar[12].Y} {(int)collar[11].X},{(int)collar[11].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((collar[10].X + collar[3].X) / 2)}, {(int)(collar[12].Y + 2*pixelsizeincm)}) rotate(90)"" >Collar, x2, interfacing</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)(collar[1].X - pixelsizeincm)}, {(int)(collar[1].Y - pixelsizeincm)}) rotate({deg})"" >Fold</text>
                ";
                else
                {
                    s += $@"
                    <path d=""M {(int)collar[11].X} {(int)collar[11].Y} L {(int)collar[1].X} {(int)collar[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[1].X},{(int)collar[1].Y} Q {(int)collar[8].X},{(int)collar[8].Y} {(int)collar[3].X},{(int)collar[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[3].X},{(int)collar[3].Y} C {(int)collar[3].X},{(int)(collar[3].Y+2*pixelsizeincm)} {(int)(collar[7].X)},{(int)collar[7].Y} {(int)collar[7].X},{(int)collar[7].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[7].X} {(int)collar[7].Y} L {(int)collar[9].X} {(int)collar[9].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[9].X},{(int)collar[9].Y} C {(int)collar[9].X},{(int)collar[9].Y} {(int)collar[10].X},{(int)(collar[10].Y + 2*pixelsizeincm)} {(int)collar[10].X},{(int)collar[10].Y}""  stroke-width=""3"" stroke=""black""  fill-opacity=""0""/>
                    <path d=""M {(int)collar[10].X},{(int)collar[10].Y} Q {(int)collar[12].X},{(int)collar[12].Y} {(int)collar[11].X},{(int)collar[11].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)((collar[10].X + collar[3].X) / 2)}, {(int)(collar[12].Y + 2*pixelsizeincm)}) rotate(90)"" >Collar, x2, interfacing</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)(collar[1].X - pixelsizeincm)}, {(int)(collar[1].Y - pixelsizeincm)}) rotate({deg})"" >Fold</text>
                ";
                }
            }
            else if (collartype=="Стойка с застежкой")
            {
                s += $@"
                    <path d=""M {(int)collar[0].X} {(int)collar[0].Y} L {(int)collar[1].X} {(int)collar[1].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[1].X} {(int)collar[1].Y} L {(int)collar[2].X} {(int)collar[2].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[2].X},{(int)collar[2].Y} C {(int)(collar[2].X+pixelsizeincm)},{(int)collar[2].Y} {(int)collar[4].X},{(int)collar[4].Y} {(int)collar[4].X},{(int)collar[4].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[5].X},{(int)collar[5].Y} C {(int)collar[5].X},{(int)collar[5].Y} {(int)(collar[3].X+pixelsizeincm)},{(int)collar[3].Y} {(int)collar[3].X},{(int)collar[3].Y}""  stroke-width=""3"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collar[3].X} {(int)collar[3].Y} L {(int)collar[0].X} {(int)collar[0].Y}""  stroke-width=""3"" stroke=""black""/>
                    <path d=""M {(int)collar[4].X},{(int)collar[4].Y} Q  {(int)collar[6].X} {(int)collar[6].Y} {(int)collar[5].X},{(int)collar[5].Y}"" fill-opacity=""0"" stroke-width=""3"" stroke=""black""/>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)(collar[0].X + 2* pixelsizeincm)}, {(int)((collar[0].Y + collar[1].Y)/2)})"" >Collar, x2, interfacing</text>
                    <text text-anchor=""middle"" font-size=""32"" font-family=""Verdana"" transform=""translate({(int)(collar[0].X+pixelsizeincm)}, {(int)((collar[0].Y + collar[1].Y) / 2)}) rotate(270)"">Fold</text>
                ";

            }
        }
        private void DrawFrontD()
        {
            if (neckType == "V-горловина")
                s += @$"
                    <path d=""M {(int)frontD[0].X} {(int)frontD[0].Y} L {(int)frontD[1].X},{(int)frontD[1].Y}""  stroke-width=""1"" stroke=""black"" />
                    ";
            else
                s += @$"
                    <path d=""M {(int)frontD[0].X} {(int)frontD[0].Y} Q {(int)frontD[1].X},{(int)frontD[0].Y} {(int)frontD[1].X},{(int)frontD[1].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    ";
            s += @$"
                    <path d=""M {(int)frontD[1].X} {(int)frontD[1].Y} L {(int)frontD[3].X} {(int)frontD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[2].X} {(int)frontD[2].Y} L {(int)frontD[3].X} {(int)frontD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[2].X} {(int)frontD[2].Y} L {(int)frontD[4].X} {(int)frontD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[4].X} {(int)frontD[4].Y} L {(int)frontD[24].X} {(int)frontD[24].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[24].X} {(int)frontD[24].Y} L {(int)frontD[5].X} {(int)frontD[5].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[5].X} {(int)frontD[5].Y} L {(int)frontD[6].X} {(int)frontD[6].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[6].X} {(int)frontD[6].Y} L {(int)frontD[7].X} {(int)frontD[7].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[7].X} {(int)frontD[7].Y} L {(int)frontD[8].X} {(int)frontD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    
                    <path d=""M {(int)frontD[8].X} {(int)frontD[8].Y} C {(int)frontD[8].X},{(int)frontD[8].Y} {(int)frontD[10].X},{(int)frontD[10].Y} {(int)frontD[9].X},{(int)frontD[9].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)frontD[9].X} {(int)frontD[9].Y} Q {(int)frontD[11].X},{(int)((frontD[9].Y + frontD[11].Y) / 2)} {(int)frontD[11].X},{(int)frontD[11].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)frontD[11].X} {(int)frontD[11].Y} Q {(int)frontD[11].X},{(int)((frontD[12].Y + frontD[11].Y) / 2)} {(int)frontD[12].X},{(int)frontD[12].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)frontD[12].X} {(int)frontD[12].Y} Q {(int)((frontD[12].X + frontD[13].X) / 2)},{(int)frontD[13].Y} {(int)frontD[13].X},{(int)frontD[13].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)frontD[13].X} {(int)frontD[13].Y} L {(int)frontD[16].X} {(int)frontD[16].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[16].X} {(int)frontD[16].Y} L {(int)frontD[14].X} {(int)frontD[14].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[14].X} {(int)frontD[14].Y} L {(int)frontD[15].X} {(int)frontD[15].Y}""  stroke-width=""1"" stroke=""black""/>

                ";
            if (waisttype== "Отрезноe по талии" || clasptype!= "Без застежки")
            {
                if (waisttype == "Отрезноe по талии")
                    //если есть низ
                    s += $@"
                    <path d=""M {(int)frontD[15].X} {(int)frontD[15].Y} L {(int)frontD[17].X} {(int)frontD[17].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[17].X} {(int)frontD[17].Y} L {(int)frontD[18].X} {(int)frontD[18].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[18].X} {(int)frontD[18].Y} L {(int)frontD[25].X} {(int)frontD[25].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[25].X} {(int)frontD[25].Y} L {(int)frontD[19].X} {(int)frontD[19].Y}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)frontD[19].X} {(int)frontD[19].Y} L {(int)frontD[20].X} {(int)frontD[20].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[20].X} {(int)frontD[20].Y} L {(int)frontD[26].X} {(int)frontD[26].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                if (clasptype != "Без застежки")
                    //если есть бок
                    s += $@"
                    <path d=""M {(int)frontD[0].X} {(int)frontD[0].Y} L {(int)frontD[23].X} {(int)frontD[23].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[23].X} {(int)frontD[23].Y} L {(int)frontD[21].X} {(int)frontD[21].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                else
                    //если нет бока
                    s += $@"
                    <path d=""M {(int)frontD[26].X} {(int)frontD[26].Y} L {(int)frontD[0].X} {(int)frontD[0].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                if (waisttype == "Отрезноe по талии" && clasptype != "Без застежки")
                //если есть и бок и низ
                    s += $@"
                    <path d=""M {(int)frontD[26].X} {(int)frontD[26].Y} L {(int)frontD[22].X} {(int)frontD[22].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)frontD[22].X} {(int)frontD[22].Y} L {(int)frontD[21].X} {(int)frontD[21].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
            else if (clasptype == "Без застежки")
            {
                s += $@"
                    <path d=""M {(int)frontD[0].X} {(int)frontD[0].Y} L {(int)front[0].X} {(int)front[0].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }


        }
        private void DrawBackD()
        {
            s += @$"
                    <path d=""M {(int)backD[19].X} {(int)backD[19].Y} Q {(int)backD[20].X},{(int)backD[20].Y} {(int)backD[18].X},{(int)backD[18].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    
                    <path d=""M {(int)backD[18].X} {(int)backD[18].Y} L {(int)backD[16].X} {(int)backD[16].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[16].X} {(int)backD[16].Y} L {(int)backD[15].X} {(int)backD[15].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[15].X} {(int)backD[15].Y} L {(int)backD[17].X} {(int)backD[17].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[17].X} {(int)backD[17].Y} L {(int)backD[14].X} {(int)backD[14].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[14].X} {(int)backD[14].Y} L {(int)backD[13].X} {(int)backD[13].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[12].X} {(int)backD[12].Y} L {(int)backD[13].X} {(int)backD[13].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[12].X} {(int)backD[12].Y} C {(int)backD[12].X},{(int)backD[12].Y} {(int)backD[22].X},{(int)(backD[22].Y)} {(int)backD[10].X},{(int)backD[10].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)backD[10].X} {(int)backD[10].Y} Q {(int)backD[10].X},{(int)((backD[9].Y + backD[10].Y) / 2)} {(int)backD[9].X},{(int)backD[9].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)backD[9].X} {(int)backD[9].Y} Q {(int)((backD[7].X + backD[9].X) / 2)},{(int)backD[7].Y} {(int)backD[7].X},{(int)backD[7].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)backD[7].X} {(int)backD[7].Y} L {(int)backD[8].X} {(int)backD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[8].X} {(int)backD[8].Y} L {(int)backD[5].X} {(int)backD[5].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[5].X} {(int)backD[5].Y} L {(int)backD[4].X} {(int)backD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    
            ";
            if (waisttype == "Отрезноe по талии" || clasptype == "Без застежки")
            {
                if (waisttype == "Отрезноe по талии")
                    //если есть низ
                    s += $@"
                    <path d=""M {(int)backD[2].X} {(int)backD[2].Y} L {(int)backD[6].X} {(int)backD[6].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[6].X} {(int)backD[6].Y} L {(int)backD[4].X} {(int)backD[4].Y}""  stroke-width=""1"" stroke=""black""/>

                    ";
                if (clasptype == "Без застежки")
                    //если есть бок
                    s += $@"
                    <path d=""M {(int)backD[19].X} {(int)backD[19].Y} L {(int)backD[0].X} {(int)backD[0].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[0].X} {(int)backD[0].Y} L {(int)backD[21].X} {(int)backD[21].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                else
                    //если нет бока
                    s += $@"
                    <path d=""M {(int)backD[19].X} {(int)backD[19].Y} L {(int)backD[2].X} {(int)backD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                if (waisttype == "Отрезноe по талии" && clasptype == "Без застежки")
                    //если есть и бок и низ
                    s += $@"
                    <path d=""M {(int)backD[21].X} {(int)backD[21].Y} L {(int)backD[1].X} {(int)backD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)backD[1].X} {(int)backD[1].Y} L {(int)backD[2].X} {(int)backD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
            else if (clasptype != "Без застежки")
            {
                s += $@"
                    <path d=""M {(int)backD[19].X} {(int)backD[19].Y} L {(int)back[0].X} {(int)back[0].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
        }
        private void DrawSkirtFrontD()
        {
            s += @$"
                    <path d=""M {(int)skirtfrontD[7].X} {(int)skirtfrontD[7].Y} C {(int)skirtfrontD[7].X},{(int)(skirtfrontD[7].Y + pixelsizeincm)} {(int)skirtfrontD[6].X},{(int)(skirtfrontD[6].Y - pixelsizeincm * 5)} {(int)skirtfrontD[6].X},{(int)skirtfrontD[6].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfrontD[6].X} {(int)skirtfrontD[6].Y} L {(int)skirtfrontD[5].X} {(int)skirtfrontD[5].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[5].X} {(int)skirtfrontD[5].Y} C {(int)skirtfrontD[5].X},{(int)skirtfrontD[5].Y} {(int)(skirtfrontD[4].X - pixelsizeincm * 10)},{(int)skirtfrontD[4].Y} {(int)skirtfrontD[4].X},{(int)skirtfrontD[4].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)skirtfrontD[3].X} {(int)skirtfrontD[3].Y} L {(int)skirtfrontD[4].X} {(int)skirtfrontD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
            if (waisttype == "Отрезноe по талии" || clasptype != "Без застежки")
            {
                if (waisttype == "Отрезноe по талии")
                    //если есть низ
                    s += $@"
                    <path d=""M {(int)skirtfrontD[7].X} {(int)skirtfrontD[7].Y} L {(int)skirtfrontD[10].X} {(int)skirtfrontD[10].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[10].X} {(int)skirtfrontD[10].Y} L {(int)skirtfrontD[8].X} {(int)skirtfrontD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[8].X} {(int)skirtfrontD[8].Y} L {(int)skirtfrontD[9].X} {(int)skirtfrontD[9].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[9].X} {(int)skirtfrontD[9].Y} L {(int)skirtfrontD[13].X} {(int)skirtfrontD[13].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[13].X} {(int)skirtfrontD[13].Y} L {(int)skirtfrontD[11].X} {(int)skirtfrontD[11].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[11].X} {(int)skirtfrontD[11].Y} L {(int)skirtfrontD[12].X} {(int)skirtfrontD[12].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[12].X} {(int)skirtfrontD[12].Y} L {(int)skirtfrontD[0].X} {(int)skirtfrontD[0].Y}""  stroke-width=""1"" stroke=""black""/>

                    ";
                if (clasptype != "Без застежки")
                    //если есть бок
                    s += $@"
                    <path d=""M {(int)skirtfrontD[1].X} {(int)skirtfrontD[1].Y} L {(int)skirtfrontD[3].X} {(int)skirtfrontD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                else
                    //если нет бока
                    s += $@"
                    <path d=""M {(int)skirtfrontD[0].X} {(int)skirtfrontD[0].Y} L {(int)skirtfrontD[4].X} {(int)skirtfrontD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                if (waisttype == "Отрезноe по талии" && clasptype != "Без застежки")
                    //если есть и бок и низ
                    s += $@"
                    <path d=""M {(int)skirtfrontD[0].X} {(int)skirtfrontD[0].Y} L {(int)skirtfrontD[2].X} {(int)skirtfrontD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtfrontD[2].X} {(int)skirtfrontD[2].Y} L {(int)skirtfrontD[1].X} {(int)skirtfrontD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
            else if (clasptype == "Без застежки")
            {
                s += $@"
                    <path d=""M {(int)skirtfrontD[4].X} {(int)skirtfrontD[4].Y} L {(int)skirtfront[3].X} {(int)skirtfront[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
        }
        private void DrawSkirtBackD()
        {
            s += @$"
                    <path d=""M {(int)skirtbackD[10].X} {(int)skirtbackD[10].Y} C {(int)skirtbackD[10].X},{(int)(skirtbackD[10].Y + pixelsizeincm)} {(int)skirtbackD[7].X},{(int)(skirtbackD[7].Y - pixelsizeincm * 5)} {(int)skirtbackD[7].X},{(int)skirtbackD[7].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>

                    <path d=""M {(int)skirtbackD[7].X} {(int)skirtbackD[7].Y} L {(int)skirtbackD[4].X} {(int)skirtbackD[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtbackD[4].X} {(int)skirtbackD[4].Y} L {(int)skirtbackD[3].X} {(int)skirtbackD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
            if (waisttype == "Отрезноe по талии" || clasptype == "Без застежки")
            {
                if (waisttype == "Отрезноe по талии")
                    //если есть низ
                    s += $@"
                    <path d=""M {(int)skirtbackD[9].X} {(int)skirtbackD[9].Y} L {(int)skirtbackD[8].X} {(int)skirtbackD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtbackD[8].X} {(int)skirtbackD[8].Y} L {(int)skirtbackD[10].X} {(int)skirtbackD[10].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                if (clasptype == "Без застежки")
                    //если есть бок
                    s += $@"
                    <path d=""M {(int)skirtbackD[1].X} {(int)skirtbackD[1].Y} L {(int)skirtbackD[2].X} {(int)skirtbackD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtbackD[2].X} {(int)skirtbackD[2].Y} L {(int)skirtbackD[3].X} {(int)skirtbackD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                else
                    //если нет бока
                    s += $@"
                    <path d=""M {(int)skirtbackD[9].X} {(int)skirtbackD[9].Y} L {(int)skirtbackD[3].X} {(int)skirtbackD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                if (waisttype == "Отрезноe по талии" && clasptype == "Без застежки")
                    //если есть и бок и низ
                    s += $@"
                    <path d=""M {(int)skirtbackD[0].X} {(int)skirtbackD[0].Y} L {(int)skirtbackD[1].X} {(int)skirtbackD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)skirtbackD[0].X} {(int)skirtbackD[0].Y} L {(int)skirtbackD[9].X} {(int)skirtbackD[9].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
            else if (clasptype != "Без застежки")
            {
                s += $@"
                    <path d=""M {(int)skirtbackD[3].X} {(int)skirtbackD[3].Y} L {(int)skirtback[4].X} {(int)skirtback[4].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
            }
        }
        private void DrawSleeveD()
        {
            s += @$"
                    <path d=""M {(int)sleeveD[0].X},{(int)sleeveD[0].Y} Q {(int)sleeveD[2].X},{(int)sleeveD[2].Y} {(int)sleeveD[6].X},{(int)sleeveD[6].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeveD[0].X},{(int)sleeveD[0].Y} Q {(int)sleeveD[8].X},{(int)sleeveD[8].Y} {(int)sleeveD[12].X},{(int)sleeveD[12].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeveD[6].X},{(int)sleeveD[6].Y} Q {(int)sleeveD[5].X},{(int)sleeveD[5].Y} {(int)sleeveD[4].X},{(int)sleeveD[4].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeveD[12].X},{(int)sleeveD[12].Y} Q {(int)sleeveD[11].X},{(int)sleeveD[11].Y} {(int)sleeveD[10].X},{(int)sleeveD[10].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                ";
            if (sleevetype == "Короткий")
            {
                s += @$"
                    <path d=""M {(int)sleeveD[4].X} {(int)sleeveD[4].Y} L {(int)sleeveD[13].X} {(int)sleeveD[13].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[13].X} {(int)sleeveD[13].Y} L {(int)sleeveD[17].X} {(int)sleeveD[17].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[17].X} {(int)sleeveD[17].Y} L {(int)sleeveD[16].X} {(int)sleeveD[16].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[19].X} {(int)sleeveD[19].Y} L {(int)sleeveD[20].X} {(int)sleeveD[20].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[20].X} {(int)sleeveD[20].Y} L {(int)sleeveD[14].X} {(int)sleeveD[14].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[14].X} {(int)sleeveD[14].Y} L {(int)sleeveD[10].X} {(int)sleeveD[10].Y}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)sleeveD[16].X},{(int)sleeveD[16].Y} C {(int)sleeveD[16].X},{(int)sleeveD[16].Y} {(int)sleeveD[18].X},{(int)sleeveD[18].Y} {(int)sleeveD[15].X},{(int)sleeveD[15].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)sleeveD[19].X},{(int)sleeveD[19].Y} C {(int)sleeveD[19].X},{(int)sleeveD[19].Y} {(int)sleeveD[21].X},{(int)sleeveD[21].Y} {(int)sleeveD[15].X},{(int)sleeveD[15].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                ";
            }
            else if (sleevetype.Contains("Епископ"))
            {
                s += @$"

                    <path d=""M {(int)sleeveD[4].X} {(int)sleeveD[4].Y} L {(int)sleeveD[13].X} {(int)sleeveD[13].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[13].X} {(int)sleeveD[13].Y} L {(int)sleeveD[18].X} {(int)sleeveD[18].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[19].X} {(int)sleeveD[19].Y} L {(int)sleeveD[14].X} {(int)sleeveD[14].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[14].X} {(int)sleeveD[14].Y} L {(int)sleeveD[10].X} {(int)sleeveD[10].Y}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)sleeveD[19].X} {(int)sleeveD[19].Y} L {(int)sleeveD[17].X} {(int)sleeveD[17].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)sleeveD[18].X} {(int)sleeveD[18].Y} L {(int)sleeveD[15].X} {(int)sleeveD[15].Y}""  stroke-width=""1"" stroke=""black""/>

                    <path d=""M {(int)sleeveD[17].X},{(int)sleeveD[17].Y} Q {(int)sleeveD[16].X},{(int)sleeveD[16].Y} {(int)sleeveD[15].X},{(int)sleeveD[15].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                ";
                if (cuff.Count != 0)
                {
                    s += $@"
                    <path d=""M {(int)cuffD[0].X} {(int)cuffD[0].Y} L {(int)cuffD[1].X} {(int)cuffD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)cuffD[1].X} {(int)cuffD[1].Y} L {(int)cuffD[2].X} {(int)cuffD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)cuffD[2].X} {(int)cuffD[2].Y} L {(int)cuffD[3].X} {(int)cuffD[3].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)cuffD[3].X} {(int)cuffD[3].Y} L {(int)cuffD[0].X} {(int)cuffD[0].Y}""  stroke-width=""1"" stroke=""black""/>
                    ";
                }
            }
        }
        private void DrawCollarD()
        {
            if (collartype == "Отложной с прямыми углами")
            {
                if (neckType != "V-горловина")
                    s += $@"
                    <path d=""M {(int)collarD[3].X} {(int)collarD[3].Y} L {(int)collarD[1].X} {(int)collarD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[1].X},{(int)collarD[1].Y} Q {(int)collarD[2].X},{(int)collarD[2].Y} {(int)collarD[0].X},{(int)collarD[0].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collarD[0].X},{(int)collarD[0].Y} Q  {(int)(collarD[11].X)},{(int)collarD[11].Y} {(int)collarD[10].X},{(int)collarD[10].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collarD[10].X} {(int)collarD[10].Y} L {(int)collarD[12].X} {(int)collarD[12].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[12].X} {(int)collarD[12].Y} L {(int)collarD[9].X} {(int)collarD[9].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[9].X} {(int)collarD[9].Y} L {(int)collarD[8].X} {(int)collarD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[8].X} {(int)collarD[8].Y} L {(int)collarD[7].X} {(int)collarD[7].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[7].X},{(int)collarD[7].Y} Q {(int)collarD[6].X},{(int)collarD[6].Y} {(int)collarD[4].X},{(int)collarD[4].Y}""  stroke-width=""1"" stroke=""black""  fill-opacity=""0""/>
                    <path d=""M {(int)collarD[4].X},{(int)collarD[4].Y} Q {(int)collarD[5].X},{(int)collarD[5].Y} {(int)collarD[3].X},{(int)collarD[3].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    "
                    ;
                else
                {
                    s += $@"
                    <path d=""M {(int)collarD[3].X} {(int)collarD[3].Y} L {(int)collarD[1].X} {(int)collarD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[1].X},{(int)collarD[1].Y} Q {(int)collarD[2].X},{(int)collarD[2].Y} {(int)collarD[0].X},{(int)collarD[0].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collarD[0].X},{(int)collarD[0].Y} C {(int)collarD[0].X},{(int)(collarD[0].Y + 2 * pixelsizeincm)} {(int)(collarD[10].X)},{(int)collarD[10].Y} {(int)collarD[10].X},{(int)collarD[10].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collarD[10].X} {(int)collarD[10].Y} L {(int)collarD[9].X} {(int)collarD[9].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[9].X} {(int)collarD[9].Y} L {(int)collarD[8].X} {(int)collarD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[8].X},{(int)collarD[8].Y} C {(int)collarD[8].X},{(int)collarD[8].Y} {(int)collarD[4].X},{(int)(collarD[4].Y + 2 * pixelsizeincm)} {(int)collarD[4].X},{(int)collarD[4].Y}""  stroke-width=""1"" stroke=""black""  fill-opacity=""0""/>
                    <path d=""M {(int)collarD[4].X},{(int)collarD[4].Y} Q {(int)collarD[5].X},{(int)collarD[5].Y} {(int)collarD[3].X},{(int)collarD[3].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                "
                ;
                }
            }
            else if (collartype == "Стойка с застежкой")
            {
                s += $@"
                    <path d=""M {(int)collarD[0].X} {(int)collarD[0].Y} L {(int)collarD[1].X} {(int)collarD[1].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[1].X} {(int)collarD[1].Y} L {(int)collarD[2].X} {(int)collarD[2].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[2].X},{(int)collarD[2].Y} C {(int)(collarD[2].X + pixelsizeincm)},{(int)collarD[2].Y} {(int)collarD[4].X},{(int)collarD[4].Y} {(int)collarD[4].X},{(int)collarD[4].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collarD[5].X},{(int)collarD[5].Y} C {(int)collarD[5].X},{(int)collarD[5].Y} {(int)(collarD[3].X + pixelsizeincm)},{(int)collarD[3].Y} {(int)collarD[3].X},{(int)collarD[3].Y}""  stroke-width=""1"" stroke=""black"" fill-opacity=""0""/>
                    <path d=""M {(int)collarD[3].X} {(int)collarD[3].Y} L {(int)collarD[0].X} {(int)collarD[0].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[7].X},{(int)collarD[7].Y} Q  {(int)collarD[6].X} {(int)collarD[6].Y} {(int)collarD[5].X},{(int)collarD[5].Y}"" fill-opacity=""0"" stroke-width=""1"" stroke=""black""/>   
                    <path d=""M {(int)collarD[4].X} {(int)collarD[4].Y} L {(int)collarD[8].X} {(int)collarD[8].Y}""  stroke-width=""1"" stroke=""black""/>
                    <path d=""M {(int)collarD[8].X} {(int)collarD[8].Y} L {(int)collarD[7].X} {(int)collarD[7].Y}""  stroke-width=""1"" stroke=""black""/>
                ";
            }
        }
        public override string GenerateContent()
        {
            float a1 = cg3 + pg;
            
            float a = shs + pshs;
            float a2 = a1 - (shg + cg2 - cg1 + pshp);
            float gbdif = ((cb + pb) - (cg3 + pg)) / 2;
            Vector2 A1 = new Vector2(csh / 3 + pshgor, (csh / 3 + pshgor) / 3);

            BackFront(a, a1, a2, A1);
            if (sleevetype!= "Без рукава")
                Sleeve(a2, a);


            if (collartype != "Без воротника")
            {
                if (collartype.Contains("Стойка"))
                    StandCollar();
                else if (collartype.Contains("Отложной"))
                    Collar(A1);
            }
            if (isLecal)
            {
                FrontD();
                BackD();
                SkirtBackD();
                SkirtFrontD();
                if (collartype != "Без воротника")
                {
                    if (collartype.Contains("Стойка"))
                        StandCollarD();
                    else if (collartype.Contains("Отложной"))
                        CollarD();
                }
                if (sleevetype != "Без рукава")
                    SleeveD();
            }

            Vector2 offsetFront = new Vector2(0, Math.Max(-front[1].Y,0));
            Vector2 offsetB = new Vector2(Math.Max(gbdif*2,0), 0);
            Vector2 sleeveoff = new Vector2(0,0);
            Vector2 cuffoff = new Vector2(0, 0);
            Vector2 collaroff = new Vector2(0, 0);
            Vector2 skirtsoffsetvert = new Vector2(0, 0);


            float rightborder = front[0].X;
            float downborder = skirtfront[3].Y;
            if (isLecal)
            {
                rightborder = frontD[23].X + 1;
                downborder = skirtfrontD[4].Y;
                offsetFront = new Vector2(1, Math.Max(Math.Max(-frontD[2].Y, -frontD[24].Y), 0));
                offsetB = new Vector2(Math.Max(gbdif * 2, 0) + 2, 0);
                if (waisttype == "Отрезноe по талии")
                {
                    skirtsoffsetvert = 2.5f * Vector2.UnitY;
                    downborder += 2.5f;
                }
            }

            widthcm = (rightborder + offsetB.X) * pixelsizeincm;
            heightcm = (downborder + offsetFront.Y) * pixelsizeincm;
            int width = (int)((rightborder + offsetB.X) * 10);
            int height = (int)((downborder + offsetFront.Y) * 10);

            float sleeveleft = 0;
            float sleevetop = 0;
            float sleeveright = 0;
            float sleevebottomBishop = 0;
            float sleevebottomShort = 0;
            float cuffbottom = 0;
            float cuffleft = 0;
            if (sleevetype != "Без рукава")
            {
                sleeveleft = Math.Min(sleeve[4].X, sleeve[14].X);
                sleevetop = -(sleeve[0].Y);
                sleeveright = Math.Max(sleeve[2].X, sleeve[13].X);
                sleevebottomBishop = BezierQ(sleeve[14], sleeve[15], sleeve[13], 0.5f).Y;
                sleevebottomShort = sleeve[12].Y;
                if (isLecal)
                {
                    sleeveleft = Math.Min(sleeveD[19].X, sleeveD[14].X);
                    sleevetop = -(sleeveD[0].Y);
                    sleeveright = Math.Max(sleeveD[18].X, sleeveD[13].X);
                    sleevebottomBishop = BezierQ(sleeveD[17], sleeveD[16], sleeveD[15], 0.5f).Y;
                    sleevebottomShort = sleeveD[20].Y;
                }

                sleeveoff = new Vector2(offsetB.X + rightborder - sleeveleft, sleevetop);
                widthcm = (sleeveright + sleeveoff.X) * pixelsizeincm;
                width = (int)((sleeveright + sleeveoff.X) * 10);
                if (cuff.Count != 0)
                {
                    cuffbottom = cuff[1].Y;
                    cuffleft = cuff[3].X;
                    float t = 0;
                    if (isLecal)
                    {
                        cuffbottom = cuffD[1].Y;
                        cuffleft = cuffD[3].X;
                        t = 1;
                    }
                    cuffoff = new Vector2(offsetB.X + rightborder + t, sleeveoff.Y + sleevebottomBishop + 1);

            }
                }
            if (collartype!= "Без воротника")
            {
                if (collartype.Contains("Отложной"))
                {
                    Vector2 ctop = collar[11];
                    Vector2 cleft = collar[10];
                    Vector2 cright = collar[7];
                    Vector2 cdown = collar[9];
                    if (isLecal)
                    {
                        ctop = collarD[3];
                        cleft = collarD[4];
                        cright = collarD[9];
                        cdown = collarD[8];
                    }
                    //рукав без манжеты
                    if (cuff.Count == 0 && sleeve.Count != 0)
                    {
                        if (sleevetype == "Короткий")
                        {
                            collaroff = new Vector2(offsetB.X + rightborder - cleft.X, sleeveoff.Y + sleevebottomShort - (ctop.Y));
                        }
                        if (sleevetype == "Епископ с резинкой")
                        {
                            collaroff = new Vector2(offsetB.X + rightborder - cleft.X, sleeveoff.Y + sleevebottomBishop - (ctop.Y));
                        }
                    }
                    else if (sleeve.Count == 0) //нет рукава
                    {
                        collaroff = new Vector2(offsetB.X + rightborder - cleft.X, -(ctop.Y));
                    }
                    //рукав с манжетой
                    else if (cuff.Count != 0)
                    {
                        collaroff = cuffoff + new Vector2(cuffleft - cleft.X, -(ctop.Y));
                    }
                    heightcm = Math.Max((cdown.Y + collaroff.Y) * pixelsizeincm, heightcm);
                    height = Math.Max((int)((cdown.Y + collaroff.Y) * 10), height);
                    widthcm = Math.Max((cright.X + collaroff.X) * pixelsizeincm, widthcm);
                    width = Math.Max((int)((cright.X + collaroff.X) * 10),width);
                }
                if (collartype=="Стойка с застежкой")
                {
                    Vector2 ctop = collar[6];
                    Vector2 cright = collar[4];
                    Vector2 cleft = collar[1];
                    Vector2 cbottom = collar[1];
                    if (isLecal)
                    {
                        ctop = collarD[6];
                        cright = collarD[7];
                        cleft = collarD[1];
                        cbottom = collarD[1];
                    }
                    //рукав без манжеты
                    if (cuff.Count == 0 && sleeve.Count != 0)
                    {
                        if (sleevetype == "Короткий")
                        {
                            collaroff = new Vector2(offsetB.X + rightborder, sleeveoff.Y + sleevebottomShort - ctop.Y);
                        }
                        if (sleevetype == "Епископ с резинкой")
                        {
                            collaroff = new Vector2(offsetB.X + rightborder, sleeveoff.Y + sleevebottomBishop - ctop.Y);
                        }
                    }
                    else if (sleeve.Count == 0) //нет рукава
                    {
                        collaroff = new Vector2(offsetB.X + rightborder, -ctop.Y);
                    }
                    //рукав с манжетой
                    else if (cuff.Count != 0)
                    {
                        collaroff = cuffoff + new Vector2(0, cuffbottom - ctop.Y);
                    }
                    heightcm = Math.Max((cbottom.Y + collaroff.Y) * pixelsizeincm, heightcm);
                    height = Math.Max((int)((cbottom.Y + collaroff.Y) * 10), height);
                    widthcm = Math.Max((cright.X + collaroff.X) * pixelsizeincm,widthcm);
                    width = Math.Max((int)((cright.X + collaroff.X) * 10),width);
                }
            }
            Offset(back, offsetFront);
            Offset(front, offsetFront + offsetB);
            Offset(skirtback, offsetFront + skirtsoffsetvert);
            Offset(skirtfront, offsetFront + offsetB + skirtsoffsetvert);
            Offset(sleeve, sleeveoff);
            Offset(cuff, cuffoff);
            Offset(collar, collaroff);

            Offset(frontD, offsetFront+offsetB);
            Offset(backD, offsetFront);
            Offset(skirtbackD, offsetFront+ skirtsoffsetvert);
            Offset(skirtfrontD, offsetFront + offsetB+ skirtsoffsetvert);
            Offset(collarD, collaroff);
            Offset(sleeveD, sleeveoff);
            Offset(cuffD, cuffoff);


            s = $"<svg version=\"1.1\" width = \"{width}mm\" height = \"{height}mm\" xmlns =\"http://www.w3.org/2000/svg\">";
            DrawBack();
            DrawSkirtBack();
            DrawFront();
            DrawSkirtFront();
            if (sleeve.Count != 0) DrawSleeve();
            if (collar.Count != 0) DrawCollar();
            if (isLecal)
            {
                DrawFrontD();
                DrawBackD();
                DrawSkirtFrontD();
                DrawSkirtBackD();
                if (collarD.Count != 0)
                    DrawCollarD();

                if (sleeveD.Count != 0) 
                    DrawSleeveD();
            }
            s += "</svg>";
            return s;
        }
    }
}
