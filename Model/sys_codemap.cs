namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.sys_codemap")]
    public partial class sys_codemap
    {
        [Key]
        [StringLength(2)]
        public string CodeMap_ID { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Value { get; set; }

        [StringLength(20)]
        public string Meaning { get; set; }
    }
}
