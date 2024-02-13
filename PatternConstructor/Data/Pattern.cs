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
        public virtual string GenerateContent() { return ""; }
    }
}
