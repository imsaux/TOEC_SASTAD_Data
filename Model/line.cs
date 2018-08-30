namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.line")]
    public partial class line
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string LineID { get; set; }

        [StringLength(255)]
        public string LineCode { get; set; }

        [StringLength(255)]
        public string LineName { get; set; }

        [StringLength(255)]
        public string BaseServiceSocket { get; set; }

        [StringLength(255)]
        public string ClientSocket { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }
    }
}
