namespace MachineAccessControl.Model.Models
{
    using MachineAccessControl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccessControlTransaction")]
    public partial class AccessControlTransaction : Entity<int>
    {
        [Key]
        public int TransactionID { get; set; }

        public int AccessControlID { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Type")]
        public string TranType { get; set; }

        [Required]
        [StringLength(70)]
        [DisplayName("Created")]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Machine")]
        public string MachineName { get; set; }

        [StringLength(200)]
        [DisplayName("Password")]
        public string PasswordEntry { get; set; }
        
        [DisplayName("Created")]
        public DateTime RecordCreated { get; set; }

        public virtual AccessControl AccessControl { get; set; }
    }
}
