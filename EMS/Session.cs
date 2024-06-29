using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    public static class Session
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static int UserId { get; set;}
        public static string UserName { get; set;}
    }
} 

