using MachineAccessControl.Model.Context;
using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Service
{
    public class AccessControlErrorService : EntityService<AccessControlError>
    {

        IContext _context;

        public AccessControlErrorService(IContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<AccessControlError>();
        }

        public AccessControlError GetById(int Id)
        {
            return _dbset.Find(Id);
        }

        public IEnumerable<AccessControlError> GetErrorsByAccessControlId(int Id)
        {
            return _dbset.Where(x => x.AccessControlID == Id);
        }
    }
}
