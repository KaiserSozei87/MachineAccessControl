using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Service
{
    public interface IAccessControlErrorService : IEntityService<AccessControlError>
    {
        AccessControlError GetById(int Id);
        IEnumerable<AccessControlError> GetErrorsByAccessControlId(int Id);
    }
}
