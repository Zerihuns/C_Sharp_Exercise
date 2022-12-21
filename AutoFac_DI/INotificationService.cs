using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFac_DI
{
    internal interface INotificationService
    {
        void NotifyUsernameChanged(User user);
    }
}
