using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.StringConsts
{
    public static class RoleConsts
    {
        public const string AdminOnly = "admin";
        public const string ModOnly = "admin,moderator";
        public const string MemberOnly = "admin,moderator,member";
    }
}
