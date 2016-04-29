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
    public class AccessControlTransactionService : EntityService<AccessControlTransaction>, IAccessControlTransactionService
    {

        IContext _context;

        public AccessControlTransactionService(IContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<AccessControlTransaction>();
        }


        public AccessControlTransaction GetById(int Id)
        {
            return _dbset.Find(Id);
        }

        public IEnumerable<AccessControlTransaction> GetAccessControlTrans(int AccessControlId)
        {
            return _dbset.Where(x => x.AccessControlID == AccessControlId);
        }

        public AccessControlTransaction LogTransaction(int AccessControlID, MachineAccessControl.Model.Common.Constants.TransactionConst.TransactionType TranType,
            string CreatedBy, string MachineName, string PasswordEntry)
        {
            AccessControlTransaction _Transaction = new AccessControlTransaction();
            _Transaction.AccessControlID = AccessControlID;
            _Transaction.TranType = TranType.ToString();
            _Transaction.CreatedBy = CreatedBy;
            _Transaction.MachineName = MachineName;
            _Transaction.PasswordEntry = PasswordEntry;
            _Transaction.RecordCreated = System.DateTime.Now;
            return _Transaction;
        }

    }
}
