namespace MachineAccessControl.Model.Models
{
    using MachineAccessControl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MachineLocation")]
    public partial class MachineLocation : Entity<int>
    {
        public MachineLocation()
        {
            Machines = new HashSet<Machine>();
        }

        [Key]
        public int LocationID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Location")]
        public string LocationName { get; set; }
                [DisplayName("Active")]
        public bool IsActive { get; set; }

        public virtual ICollection<Machine> Machines { get; set; }
    }
}
