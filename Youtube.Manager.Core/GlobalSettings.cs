using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.Manager.Core
{
    public static class GlobalSettings
    {
        public static string ApiKey { get; private set; }

        static GlobalSettings()
        {
            ApiKey = "AIzaSyBdMiW-2wzipg1OxKeQIs-sTZTa4pDt4o8";
        }
    }
}
