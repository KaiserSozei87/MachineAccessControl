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
    public class MachineService : EntityService<Machine>, IMachineService
    {

        IContext _context;

        public MachineService(IContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<Machine>();
        }

        public Machine GetById(int Id)
        {
            return _dbset.Find(Id);
        }

        public Machine GetByName(string MachineName)
        {
            return _dbset.Where(x => x.MachineName.Equals(MachineName)).FirstOrDefault();
        }

        public IEnumerable<Machine> GetByLocationId(int LocationId)
        {
            return _dbset.Where(x => x.MachineLocation == LocationId);
        }

        public IEnumerable<Machine> GetAllActive()
        {
            return _dbset.Where(x => x.IsActive);
        }

    }
}
