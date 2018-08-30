namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.hotwheel_axle")]
    public partial class hotwheel_axle
    {
        [Key]
        [StringLength(36)]
        public string HWAxleID { get; set; }

        [StringLength(36)]
        public string TrainDetail_ID { get; set; }

        public int? Axle_OrderNum { get; set; }

        public int? TrainDetail_OrderNum { get; set; }

        public float? Left_TemperatureRise { get; set; }

        public float? Right_TemperatureRise { get; set; }

        [StringLength(100)]
        public string LeftWave { get; set; }

        [StringLength(100)]
        public string RightWave { get; set; }

        public float? Speed { get; set; }

        [StringLength(50)]
        public string NextAxleDistance { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime InTime { get; set; }
    }
}
