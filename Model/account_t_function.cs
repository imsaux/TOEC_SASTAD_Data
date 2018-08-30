namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.account_t_function")]
    public partial class account_t_function
    {
        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(36)]
        public string PID { get; set; }

        [Required]
        [StringLength(200)]
        public string FunctionName { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public int? OrderNum { get; set; }

        public int Visible { get; set; }
    }
}
