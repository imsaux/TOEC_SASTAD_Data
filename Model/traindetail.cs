namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.traindetail")]
    public partial class traindetail
    {
        [Key]
        public Guid TrainDetail_ID { get; set; }

        public Guid? Train_ID { get; set; }

        public int? TrainDetail_OrderNo { get; set; }

        [StringLength(50)]
        public string TrainDetail_No { get; set; }

        [StringLength(50)]
        public string TrainDetail_Remark { get; set; }

        public int? TrainDetail_IF { get; set; }

        [StringLength(50)]
        public string vehicletype { get; set; }

        [StringLength(50)]
        public string startstation { get; set; }

        [StringLength(50)]
        public string endstation { get; set; }

        [StringLength(50)]
        public string GoodsName { get; set; }

        public decimal? Ziz { get; set; }

        public decimal? Zaiz { get; set; }

        public decimal? HC { get; set; }

        [StringLength(50)]
        public string smoke_result { get; set; }

        public DateTime? DealTime { get; set; }

        [StringLength(2)]
        public string AlarmLevel { get; set; }

        public DateTime? AlarmTime { get; set; }

        public int? IsCrossCar { get; set; }

        public int? IsLastCar { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? InsertTime { get; set; }
    }
}
