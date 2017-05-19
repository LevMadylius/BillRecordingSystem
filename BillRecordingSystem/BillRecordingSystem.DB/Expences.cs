namespace BillRecordingSystem.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Expences
    {
        [Key]
        public int IdExpence { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int MonthAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime BillDate { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comment { get; set; }

        public int IdUser { get; set; }

        public int IdExpenceType { get; set; }

        public virtual ExpenceTypes ExpenceTypes { get; set; }

        public virtual Users Users { get; set; }
    }
}
