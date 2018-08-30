namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.account_r_users_roles")]
    public partial class account_r_users_roles
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string UserID { get; set; }

        [Required]
        [StringLength(36)]
        public string RoleID { get; set; }
    }
}
