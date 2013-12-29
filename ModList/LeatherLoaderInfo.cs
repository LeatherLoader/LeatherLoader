using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeatherLoader.ModList
{
    /// <summary>
    /// Version & authoring info for LeatherLoader- ideally will be used some day to check version compatibility, etc.
    /// </summary>
    public class LeatherLoaderInfo : IModInfo
    {
        public LeatherLoaderInfo()
        {

        }

        public string GetModName()
        {
            return "LEATHERLOADER-LeatherLoader";
        }

        public string GetModVersion()
        {
            return "1.0.0";
        }

        public string GetPrettyModName()
        {
            return "Leather Loader";
        }

        public string GetPrettyModVersion()
        {
            return "Version 1";
        }

        public bool CanAcceptModlessClients()
        {
            return true;
        }

        public bool CanConnectToModlessServers()
        {
            return true;
        }

        public string GetCreditString()
        {
            return "By CanVox";
        }
    }
}
