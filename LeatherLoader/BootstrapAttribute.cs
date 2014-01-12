using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeatherLoader
{
    /// <summary>
    /// A dummy custom attribute- classes in mods that have this attribute & extend UnityEngine.MonoBehaviour will be loaded as scripts.
    /// </summary>
    public class BootstrapAttribute : Attribute
    {
    }
}
