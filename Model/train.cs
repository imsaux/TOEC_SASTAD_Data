namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.train")]
    public partial class train
    {
        [Key]
        public Guid Train_ID { get; set; }

        public int? Line_ID { get; set; }

        [StringLength(50)]
        public string Train_No { get; set; }

        [StringLength(10)]
        public string Train_Speed { get; set; }

        public int? Train_Count { get; set; }

        [StringLength(20)]
        public string Train_type { get; set; }

        public DateTime? Train_ComeDate { get; set; }

        public int Train_WorkFlag { get; set; }

        public int? Train_Real { get; set; }

        [StringLength(10)]
        public string source { get; set; }

        [StringLength(10)]
        public string TelexCode { get; set; }

        [StringLength(2)]
        public string AlarmLevel { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? InsertTime { get; set; }
    }
}
