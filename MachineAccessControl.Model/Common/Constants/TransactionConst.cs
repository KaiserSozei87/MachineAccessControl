using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineAccessControl.Model.Common.Constants
{
    public class TransactionConst
    {

        public enum TransactionType
        {
            Created,
            Deleted,
            Modified,
            PasswordReset,
            OldPassword,
            Viewed,
            Expired,
            Disabled
        }

    }
}
