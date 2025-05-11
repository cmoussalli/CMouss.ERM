using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERMUIKit
{
    public static class ERMUIKitSettings
    {
        public static class Defaults
        {
            public static void Initiate()
            {
                //Stage
                Stage.BorderColor = "blue";

            }


            public static class Generics
            {
                public static string LinkColor { get; set; } = "#a58d63";
            }



            //public static



            //Page Header



            //Stage
            public static class Stage
            {
                public static string BorderColor { get; set; } = "black";
                public static StageBorderSize BorderSize { get; set; } = StageBorderSize.Normal;

                public static int Height { get; set; } = 180;

            }

            public static class PageHeaderInfoArea
            {
                public static string BackgroundColor { get; set; } = "";
                public static string BackgroundColor_Hover { get; set; } = "lightgray";
                public static string FontColor { get; set; } = "darkgray";
            }




            //Stage Tab
            public static class StageTab
            {
                //public static string BackgroundColor { get; set; }
                //public static string StageTabActiveText { get; set; }
                //public static string StageTabDisabledColor { get; set; }

            }


            //Stage Tab Box
            public static class StageTabBox
            {
                public static string BackgroundColor { get; set; }
                public static string BackgroundColor_Hover { get; set; }

            }


            //Stage Rule



            //Stage Metric



            //Stage Help Text
            //border-style: solid;/* border: dashed; */border-color: blueviolet;border-width



        }
    }
}
