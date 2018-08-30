namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.alarmdetail")]
    public partial class alarmdetail
    {
        [Key]
        [StringLength(36)]
        public string AlarmDetail_ID { get; set; }

        [StringLength(36)]
        public string Train_ID { get; set; }

        [StringLength(36)]
        public string TrainDetail_ID { get; set; }

        [StringLength(5)]
        public string Side { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string FileContent { get; set; }

        [StringLength(50)]
        public string ProblemType { get; set; }

        public int? AlarmLevel { get; set; }

        [StringLength(10)]
        public string Source { get; set; }

        public int? AlgResult { get; set; }

        [StringLength(5)]
        public string HandleResult { get; set; }

        [StringLength(36)]
        public string OpUser { get; set; }

        public DateTime? OpTime { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime InsertTime { get; set; }
    }
}
