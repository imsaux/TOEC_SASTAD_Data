namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.sys_code")]
    public partial class sys_code
    {
        [Key]
        public int Code_ID { get; set; }

        [StringLength(2)]
        public string CodeMap_ID { get; set; }

        public int? tabindex { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Meaning { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        [StringLength(2)]
        public string IsEnable { get; set; }
    }
}
