using PatternConstructor.Models;

namespace PatternConstructor.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.standartMeasures.Any())
                {
                    context.standartMeasures.AddRange(new List<StandartMeasure>()
                    {
                        new StandartMeasure()
                        {
                            //Id = 0,
                            Size = 36,
                            Height = 152,
                            Measure = new Measure()
                            {
                                //MeasureId =0,
                                BustCenter = 17,
                                LegLength = 71,
                                ShoulderToWrist = 50,
                                SeatHeight = 23,
                                HipsGirth = 78,
                                BustGirth = 72,
                                UpperArm = 21,
                                WaistGirth = 52,
                                HipHeight = 18,
                                BustHeight = 22,
                                ShoulderToNeck = 12,
                                ElbowLength = 30,
                                BackWaistLength=37,
                                FrontWaistLength=38,
                                BackArmholeDepth=17,
                                NeckGirth=31,
                                BustWidth = 28,
                                BurstGirthSecond=36,
                                BustGirthUp=71,
                                ShoulderHeight=19,
                                BackWidth=30,
                                WaistFloorSideLength=96,
                                WaistFloorFrontLength=95,
                                WaistFloorBackLength=95
                            },
                            //MeasureId = 0
                         },
                        new StandartMeasure()
                        {
                            //Id = 12,
                            Size = 42,
                            Height = 152,
                            Measure = new Measure()
                            {
                                //MeasureId = 12,  
                                BustCenter = 18.8, 
                                LegLength = 70.2,
                                ShoulderToWrist = 51.2,
                                SeatHeight = 24.7,
                                HipsGirth = 90,
                                BustGirth = 84,
                                UpperArm = 26,
                                WaistGirth = 64,
                                HipHeight = 18.8,
                                BustHeight = 25.1,
                                ShoulderToNeck = 12.4,
                                ElbowLength = 30.4,
                                BackWaistLength=37.7,
                                FrontWaistLength=40.7,
                                BackArmholeDepth=17.8,
                                NeckGirth=34,
                                BustWidth = 31.2,
                                BurstGirthSecond=42,
                                BustGirthUp=80.8,
                                ShoulderHeight=19.3,
                                BackWidth=30.4,
                                WaistFloorSideLength=96.7,
                                WaistFloorFrontLength=99.3,
                                WaistFloorBackLength=96
                            },
                            //MeasureId = 12
                         },
                        new StandartMeasure()
                        {
                            //Id = 59,
                            Size = 64,
                            Height = 182,
                            Measure = new Measure()
                            {
                                //MeasureId = 59,
                                BustCenter = 25.4,
                                LegLength = 88.1,
                                ShoulderToWrist = 64.4,
                                SeatHeight = 30.5,
                                HipsGirth = 134,
                                BustGirth = 128,
                                UpperArm = 41.4,
                                WaistGirth = 112.8,
                                HipHeight = 22.4,
                                BustHeight = 37,
                                ShoulderToNeck = 14.9,
                                ElbowLength = 37.1,
                                BackWaistLength=44.8,
                                FrontWaistLength=53.8,
                                BackArmholeDepth=21.5,
                                NeckGirth=42.8,
                                BustWidth = 42.6,
                                BurstGirthSecond=64,
                                BustGirthUp=116.8,
                                ShoulderHeight=23.5,
                                BackWidth=43.6,
                                WaistFloorSideLength=120.3,
                                WaistFloorFrontLength=118.4,
                                WaistFloorBackLength=119.4
                            },
                            //MeasureId = 59
                         },
                    });
                    context.SaveChanges();
                }
                
            }
        }
    }
}
