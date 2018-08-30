namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.account_t_role")]
    public partial class account_t_role
    {
        [Key]
        [StringLength(36)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        public int? OrderNum { get; set; }
    }
}
