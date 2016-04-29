using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Service
{
    public interface IMachineService : IEntityService<Machine>
    {

        Machine GetById(int Id);
        Machine GetByName(string MachineName);
        IEnumerable<Machine> GetByLocationId(int LocationId);
        IEnumerable<Machine> GetAllActive();

    }
}
