using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Common
{
    public class Enums
    {
        public enum UserRole
        {
            Customer=1,
            Admin=2
        }

        public enum ApiCallRequestType
        {
            GET = 0,
            POST = 1,
            PUT = 2,
            DELETE = 3
        }

        public enum TableStatus
        {
            Active = 1,
            Deleted = 0,
        }

        public enum Gender
        {
            Male=1,
            Female=2
        }
    }
}
