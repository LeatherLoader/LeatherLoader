using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeatherLoader.ModList
{
    /// <summary>
    /// I'd like to eventually use this to provide info about what mods are loaded, as well as negotiating requirements between client & server.
    /// Right now all it does is, all mods need a class with a parameterless constructor that implements this interface in order for us to load
    /// the scripts.
    /// </summary>
    public interface IModInfo
    {
        /// <summary>
        /// Gets the computer-friendly mod name.  We use this mod name to compare whether any two entities have the same mod.
        /// </summary>
        string GetModName();

        /// <summary>
        /// Gets the computer-friendly mod version.  We use this mod name to compare whether any two entities have the same version, so it should only
        /// increment when compatibility is actually broken.
        /// </summary>
        string GetModVersion();

        /// <summary>
        /// Gets a mod name that's fit to show a user.
        /// </summary>
        string GetPrettyModName();

        /// <summary>
        /// Gets a mod version that's fit to show a user.  Not compared to anything, so feel free to include build numbers or whatever.
        /// </summary>
        string GetPrettyModVersion();

        /// <summary>
        /// Whether a server with this mod should allow clients without the mod to connect.
        /// </summary>
        bool CanAcceptModlessClients();

        /// <summary>
        /// Whether a client with this mod should be able to see & connect to servers without the mod.
        /// </summary>
        bool CanConnectToModlessServers();

        /// <summary>
        /// Get a string that we can show a user which contains the authors responsible.
        /// </summary>
        string GetCreditString();
    }
}
