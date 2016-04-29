namespace MachineAccessControl.Model.Models
{
    using MachineAccessControl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccessControlError")]
    public partial class AccessControlError : Entity<int>
    {
        [Key]
        public int ErrorID { get; set; }

        [Required]
        [StringLength(100)]
        public string ErrorName { get; set; }

        public string ErrorDesc { get; set; }

        [Required]
        [StringLength(70)]
        public string CreatedBy { get; set; }

        public int AccessControlID { get; set; }

        public virtual AccessControl AccessControl { get; set; }
    }
}
