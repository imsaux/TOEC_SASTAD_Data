namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sartas.account_t_station")]
    public partial class account_t_station
    {
        [Key]
        [StringLength(36)]
        public string StationID { get; set; }

        [Required]
        [StringLength(20)]
        public string StationName { get; set; }

        [Required]
        [StringLength(255)]
        public string ConnectString { get; set; }

        [StringLength(255)]
        public string TelexCode { get; set; }

        [Required]
        [StringLength(2)]
        public string StationType { get; set; }

        public int? OrderNum { get; set; }
    }
}
