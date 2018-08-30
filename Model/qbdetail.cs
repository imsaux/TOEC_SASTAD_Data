namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.qbdetail")]
    public partial class qbdetail
    {
        [StringLength(36)]
        public string QBDetailID { get; set; }

        [StringLength(36)]
        public string QBMasterID { get; set; }

        [StringLength(50)]
        public string CC { get; set; }

        [StringLength(50)]
        public string SWBZ { get; set; }

        [StringLength(50)]
        public string CH { get; set; }

        [StringLength(50)]
        public string SWH { get; set; }

        [StringLength(50)]
        public string CZ { get; set; }

        [StringLength(50)]
        public string CX { get; set; }

        [StringLength(50)]
        public string PM { get; set; }

        [StringLength(255)]
        public string JS { get; set; }

        [StringLength(50)]
        public string PB { get; set; }

        [StringLength(50)]
        public string JYBZ { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Intime { get; set; }
    }
}
