using System.Numerics;

namespace PatternConstructor.Data
{
    public class Pattern
    {
        protected const double pixelsizeincm = 37.795275591;
        public double widthcm { get; set; }
        public double heightcm { get; set; }
        public Pattern()
        {
            widthcm = 0;
            heightcm = 0;
        }
        protected double CountLength(string Length, double Front)
        {
            double coef=1;
            switch (Length)
            {
                case "Мини":
                    coef *= 4.0 / 9;
                    break;
                case "До колена":
                    coef *= 5.0 / 9;
                    break;
                case "Миди":
                    coef *= 13.0 / 18;
                    break;
            }
            return coef*Front;
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
        public virtual string GenerateContent() { return ""; }
    }
}
