using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Service
{
    public interface IMachineLocationService : IEntityService<MachineLocation>
    {

        MachineLocation GetById(int id);
        MachineLocation GetByName(string LocationName);
        IEnumerable<MachineLocation> GetAllActive();

    }
}
