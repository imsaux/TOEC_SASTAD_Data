//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class hotwheel_axle
    {
        public string HWAxleID { get; set; }
        public string TrainDetail_ID { get; set; }
        public Nullable<int> Axle_OrderNum { get; set; }
        public Nullable<int> TrainDetail_OrderNum { get; set; }
        public Nullable<float> Left_TemperatureRise { get; set; }
        public Nullable<float> Right_TemperatureRise { get; set; }
        public string LeftWave { get; set; }
        public string RightWave { get; set; }
        public Nullable<float> Speed { get; set; }
        public string NextAxleDistance { get; set; }
        public string FileName { get; set; }
        public System.DateTime InTime { get; set; }
    }
}
