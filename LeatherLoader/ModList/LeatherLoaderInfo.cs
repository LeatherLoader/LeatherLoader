//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using NeolithLib.Bootstrapper;


namespace LeatherLoader
{
	public class LeatherLoaderInfo : IModInfo
	{
		public LeatherLoaderInfo ()
		{
		}

		#region IModInfo implementation

		public string GetModName ()
		{
			return "NEOLITH-NEOLITH";
		}

		public string GetModVersion ()
		{
			return "1.0.0";
		}

		public string GetPrettyModName ()
		{
			return "Neolith Loader";
		}

		public string GetPrettyModVersion ()
		{
			return "Version 1.0";
		}

		public bool CanAcceptModlessClients ()
		{
			return true;
		}

		public bool CanConnectToModlessServers ()
		{
			return true;
		}

		public string GetCreditString ()
		{
			return "By CanVox & the Neolith Contributors on GitHub";
		}

		#endregion
	}
}

