namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.statistics_image")]
    public partial class statistics_image
    {
        public Guid ID { get; set; }

        public Guid? TrainID { get; set; }

        public int? LineID { get; set; }

        public int? AllCarNum { get; set; }

        public int? OneCarCheckImagesNums { get; set; }

        public DateTime? TrainComeDate { get; set; }

        public int? StaggerBgnOrderNo { get; set; }

        public int? StaggerEndOrderNo { get; set; }

        public int? MonitorImageNum { get; set; }

        public int? CallAlgImageNum { get; set; }

        public int? SucCheckImageNum { get; set; }

        public int? FailCheckImageNum { get; set; }

        public int? NoCheckImageNum { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string MonitorImageRemark { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string CallAlgImageRemark { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string SucCheckImageRemark { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string FailCheckImageRemark { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string NoCheckImageRemark { get; set; }

        public int? RecvCarInfoNum { get; set; }

        public int? UseQBCarInfoNum { get; set; }

        [StringLength(50)]
        public string DataSource { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? InsertTime { get; set; }
    }
}
