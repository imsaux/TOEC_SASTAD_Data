namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.qbmaster")]
    public partial class qbmaster
    {
        [StringLength(36)]
        public string QBMasterID { get; set; }

        [StringLength(36)]
        public string TrainID { get; set; }
    }
}
