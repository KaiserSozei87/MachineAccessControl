namespace MachineAccessControl.Model.Models
{
    using MachineAccessControl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Machine")]
    public partial class Machine : Entity<int>
    {
        public Machine()
        {
            AccessControls = new HashSet<AccessControl>();
        }

        [Key]
        public int MachineID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Machine")]
        public string MachineName { get; set; }
                [DisplayName("Location")]
        public int MachineLocation { get; set; }
                [DisplayName("Active")]
        public bool IsActive { get; set; }

        public virtual ICollection<AccessControl> AccessControls { get; set; }

        public virtual MachineLocation MachineLocations { get; set; }
    }
}
