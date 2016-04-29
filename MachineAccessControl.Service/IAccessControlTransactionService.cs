using MachineAccessControl.Model.Models;
using MachineAccessControl.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Service
{
    public interface IAccessControlTransactionService : IEntityService<AccessControlTransaction>
    {

        AccessControlTransaction GetById(int Id);
        IEnumerable<AccessControlTransaction> GetAccessControlTrans(int AccessControlId);
        AccessControlTransaction LogTransaction(int AccessControlID, MachineAccessControl.Model.Common.Constants.TransactionConst.TransactionType TranType,
            string CreatedBy, string MachineName, string PasswordEntry);

    }
}
