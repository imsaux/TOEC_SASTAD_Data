namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.account_t_user")]
    public partial class account_t_user
    {
        [Key]
        [StringLength(36)]
        public string UserID { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        public int? DeptCode { get; set; }

        [Required]
        [StringLength(50)]
        public string RealName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string ShiftName { get; set; }

        [StringLength(50)]
        public string VoiceName { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        [StringLength(10)]
        public string TelexCode { get; set; }
    }
}
