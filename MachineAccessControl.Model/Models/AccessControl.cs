namespace MachineAccessControl.Model.Models
{
    using MachineAccessControl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccessControl")]
    public partial class AccessControl : Entity<int>
    {
        public AccessControl()
        {
            AccessControlErrors = new HashSet<AccessControlError>();
            AccessControlTransactions = new HashSet<AccessControlTransaction>();
        }

        [Key]
        public int AccessControlID { get; set; }

        public int MachineID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string PasswordEntry { get; set; }

        [DisplayName("Created")]
        public DateTime RecordCreated { get; set; }


        [StringLength(70)]
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

                [DisplayName("Last Modified")]
        public DateTime LastUpdated { get; set; }


        [StringLength(70)]
        [DisplayName("Last Modified By")]
        public string LastUpdatedBy { get; set; }

                [DisplayName("Active")]
        public bool IsActive { get; set; }

                [DisplayName("Status")]
        public string ViewedState { get; set; }

        public virtual Machine Machine { get; set; }

        public virtual ICollection<AccessControlError> AccessControlErrors { get; set; }

        public virtual ICollection<AccessControlTransaction> AccessControlTransactions { get; set; }
    }
}
