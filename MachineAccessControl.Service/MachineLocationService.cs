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
    public class MachineLocationService : EntityService<MachineLocation>, IMachineLocationService
    {

        IContext _context;

        public MachineLocationService(IContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<MachineLocation>();
        }


        public MachineLocation GetById(int id)
        {
            return _dbset.Find(id);
        }
        
        public MachineLocation GetByName(string LocationName)
        {
            return _dbset.Where(x => x.LocationName.Equals(LocationName)).FirstOrDefault();
        }

        public IEnumerable<MachineLocation> GetAllActive()
        {
            return _dbset.Where(x => x.IsActive);
        }

    }
}
