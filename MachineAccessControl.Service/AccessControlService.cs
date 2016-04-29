using MachineAccessControl.Model.Context;
using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace MachineAccessControl.Service
{
    public class AccessControlService : EntityService<AccessControl>, IAccessControlService
    {

        IContext _context;

        public AccessControlService(IContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<AccessControl>();
        }

        public AccessControl GetById(int id)
        {
            return _dbset.Find(id);
        }

        public AccessControl GetByIdNotTracked(int id)
        {
            return _dbset.AsNoTracking().Where(x => x.AccessControlID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<AccessControl> GetByMachineId(int MachineId)
        {
            return _dbset.Where(x => x.MachineID.Equals(MachineId));
        }
        public IEnumerable<AccessControl> GetByLocationId(int LocationId)
        {
            return _dbset.Where(x => x.Machine.MachineLocation.Equals(LocationId));
        }
        public IEnumerable<AccessControl> GetAllActive()
        {
            return _dbset.Include(x => x.Machine).Where(x => x.IsActive);
        }
        public IEnumerable<AccessControl> GetUpdatedInLast7Days()
        {
            DateTime Last7Days = System.DateTime.Now.AddDays(-7);
            return _dbset.Where(x => x.LastUpdated >= Last7Days);
        }
        public IEnumerable<AccessControl> GetViewedInLast7Days()
        {
            DateTime Last7Days = System.DateTime.Now.AddDays(-7);
            var ViewedInLast7Days = _context.AccessControlTransactions.Where(x => x.RecordCreated >= Last7Days & x.TranType.Equals("VIEWED"));

            var ViewedItems = (from a in ViewedInLast7Days
                               join b in _dbset
                               on a.AccessControlID equals b.AccessControlID
                               select b).ToList();

            return ViewedItems;
        }

    }
}
