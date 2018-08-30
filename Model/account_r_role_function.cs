namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.account_r_role_function")]
    public partial class account_r_role_function
    {
        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(36)]
        public string RoleID { get; set; }

        [StringLength(36)]
        public string FunctionID { get; set; }
    }
}
