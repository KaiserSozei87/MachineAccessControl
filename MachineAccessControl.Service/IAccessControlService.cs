using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Service
{
    public interface IAccessControlService : IEntityService<AccessControl>
    {

        AccessControl GetById(int id);
        AccessControl GetByIdNotTracked(int id);
        IEnumerable<AccessControl> GetByMachineId(int MachineId);
        IEnumerable<AccessControl> GetByLocationId(int LocationId);
        IEnumerable<AccessControl> GetAllActive();
        IEnumerable<AccessControl> GetUpdatedInLast7Days();
        IEnumerable<AccessControl> GetViewedInLast7Days();

    }
}
