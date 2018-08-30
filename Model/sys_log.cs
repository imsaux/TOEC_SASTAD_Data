namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.sys_log")]
    public partial class sys_log
    {
        [Key]
        [StringLength(36)]
        public string LogID { get; set; }

        public DateTime? OpTime { get; set; }

        [StringLength(50)]
        public string ApplicationName { get; set; }

        [StringLength(50)]
        public string FunctionName { get; set; }

        [StringLength(10)]
        public string State { get; set; }

        [StringLength(5000)]
        public string Detail { get; set; }

        [StringLength(36)]
        public string ErrorID { get; set; }

        [StringLength(15)]
        public string Version { get; set; }
    }
}
