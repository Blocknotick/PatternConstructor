using PatternConstructor.Data.Enum;
using System.Numerics;

namespace PatternConstructor.Data
{
    public class Pattern
    {
        protected const double pixelsizeincm = 37.795275591;
        public double widthcm { get; set; } = 0;
        public double heightcm { get; set; } = 0;
        protected double CountLength(LengthType length, double Front)
        {
            double coef = 1;
            switch (length)
            {
                case LengthType.Mini:
                    coef *= 4.0 / 9;
                    break;
                case LengthType.Knee:
                    coef *= 5.0 / 9;
                    break;
                case LengthType.Midi:
                    coef *= 13.0 / 18;
                    break;
                default:
                    break;
            }
            return coef * Front;
        }
        //пересечение двух прямых, заданных точками
        public Vector2 Intersec(Vector2 pABDot1, Vector2 pABDot2, Vector2 pCDDot1, Vector2 pCDDot2)
        {
            float a1 = pABDot2.Y - pABDot1.Y;
            float b1 = pABDot1.X - pABDot2.X;
            float c1 = -pABDot1.X * pABDot2.Y + pABDot1.Y * pABDot2.X;

            float a2 = pCDDot2.Y - pCDDot1.Y;
            float b2 = pCDDot1.X - pCDDot2.X;
            float c2 = -pCDDot1.X * pCDDot2.Y + pCDDot1.Y * pCDDot2.X;

            return new Vector2((b1 * c2 - b2 * c1) / (a1 * b2 - a2 * b1), (a2 * c1 - a1 * c2) / (a1 * b2 - a2 * b1));
        }
        //пересечение двух окружностей, заданных центрами и радиусами
        public Vector2[] CirclesIntersec(Vector2 O1, double r1, Vector2 O2, double r2)
        {
            double d = Vector2.Distance(O1, O2);
            if (d==0 && r1==r2 || d>r1+r2 || d<Math.Abs(r1-r2)) return Array.Empty<Vector2>();
            double a = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
            double h = Math.Sqrt(r1 * r1 - a * a);
            Vector2 P3 = O1 + (float)(a / d) * (O2 - O1);
            Vector2 P1 = new Vector2(P3.X + (float)(h / d) * (O2.Y - O1.Y),
                P3.Y-(float)(h/d)*(O2.X-O1.X));
            Vector2 P2 = new Vector2(P3.X - (float)(h / d) * (O2.Y - O1.Y),
                P3.Y + (float)(h / d) * (O2.X - O1.X));
            Vector2[] arr = new Vector2[2];
            arr[0] = P1; arr[1] = P2;
            return arr;
        }
        public Vector2 RotatedVector(Vector2 v, double angleRad, Vector2 rotationPoint)
        {
            float dx = rotationPoint.X;
            float dy = rotationPoint.Y;
            float sin = (float)Math.Sin(angleRad);
            float c = (float)Math.Cos(angleRad);
            return new Vector2((v.X - dx) * c + ( v.Y - dy) * sin + dx,
                            dy + ((-dy + v.Y) * c) - ((v.X - dx) * sin));
        }
        public Vector2 RotatedVector(Vector2 v, float sin, float cos, Vector2 rotationPoint)
        {
            float dx = rotationPoint.X;
            float dy = rotationPoint.Y;
            return new Vector2((v.X - dx) * cos + (v.Y - dy) * sin + dx,
                            dy + ((-dy + v.Y) * cos) - ((v.X - dx) * sin));
        }
        public Vector2 BezierQ(Vector2 P1, Vector2 P2, Vector2 P3, float t)
        {
            return P1 * (1 - t) * (1 - t) + 2 * (1 - t) * t * P2 + t * t * P3;
        }
        public float BezierQLength(Vector2 a, Vector2 b, Vector2 c)
        {
            Vector2 v,w;
            v.X = 2 * (b.X - a.X);
            v.Y = 2 * (b.Y - a.Y);
            w.X = c.X - 2 * b.X + a.X;
            w.Y = c.Y - 2 * b.Y + a.Y;

            float uu = 4 * (w.X * w.X + w.Y * w.Y);

            if (uu < 0.00001)
                return (float)Math.Sqrt((c.X - a.X) * (c.X - a.X) + (c.Y - a.Y) * (c.Y - a.Y));

            float vv = 4 * (v.X * w.X + v.Y * w.Y);
            float ww = v.X * v.X + v.Y * v.Y;

            float t1 = (float)(2 * Math.Sqrt(uu * (uu + vv + ww)));
            float t2 = 2 * uu + vv;
            float t3 = vv * vv - 4 * uu * ww;
            float t4 = (float)(2 * Math.Sqrt(uu * ww));

            return (float)((t1 * t2 - t3 * Math.Log(t2 + t1) - (vv * t4 - t3 * Math.Log(vv + t4))) / (8 * Math.Pow(uu, 1.5)));
        }
        /// <summary>
        /// direction = 0 -> y<0, direction = 1 -> y>0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector2 Normal (Vector2 v1, Vector2 v2, bool direction)
        {
            Vector2 Normal = v2 - v1;
            float x = Normal.X;
            Normal.X = Normal.Y;
            Normal.Y = -x;
            if (direction&&Normal.Y<0 || !direction&&Normal.Y>0)
            {
                Normal *= -1;
            }
            return Normal/Normal.Length();
        }
        public virtual string GenerateContent() { return ""; }
    }
}
