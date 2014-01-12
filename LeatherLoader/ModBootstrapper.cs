using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LeatherLoader.ModList;
using UnityEngine;
using LitJson;

namespace LeatherLoader
{
    public class ModBootstrapper : MonoBehaviour
    {
        /// <summary>
        /// List of all currently loaded mods.  Used for not a whole lot currently.
        /// </summary>
        List<IModInfo> mLoadedMods = new List<IModInfo>();

        public void Start()
        {
            //Set the server to modded if you have Leather installed, in order to be good citizens.
            //This definintion was obtained by Black Magicks, so please don't be mad, it was for a good cause.
            Rust.Steam.Server.SetModded();

            //Load the loader info into the list of mods
            mLoadedMods.Add(new LeatherLoaderInfo());

			ConsoleSystem.Log ("Loading leather config...");
			LeatherConfig config = new LeatherConfig ();
			string leatherConfigPath = Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), "leather.cfg");

			if (!File.Exists (leatherConfigPath)) {
				ConsoleSystem.Log ("Couldn't find leather.cfg! Loading default values...");
			} else {
				if (!config.Read(leatherConfigPath)) {
					ConsoleSystem.Log("Couldn't parse leather.cfg properly! Loading default values...");
				} else {
					ConsoleSystem.Log("Loaded leather.cfg successfully.");
				}
			}

            ConsoleSystem.Log("Loading mods...");

            string modsFolder = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), config.ModDirectoryPath));

            if (Directory.Exists(modsFolder))
            {
                //If the mods directory exists, cyle through all the assemblies looking for mods
                //Leather mods have no extension, are in Rust_Server_data or Rust_data and all end in _mod - we do this 
                //because many server hosts aren't too okay with people uploading random DLLs, so we conceal the DLL-ness 
                //by removing the extension
                foreach (string file in Directory.GetFiles(modsFolder, config.ModFilePattern))
                {
                    ConsoleSystem.Log(string.Format("Attempting to scan Assembly: {0}", file));
                    
                    //Load the assembly & cycle through types
                    Assembly modFile = Assembly.LoadFile(file);

                    List<Type> behaviours = new List<Type>();
                    IModInfo modInfo = null;
                    foreach (Type type in modFile.GetExportedTypes())
                    {
                        //I really need to move over to MonoDevelop! Anywhere you see object.ReferenceEquals used it's because the Visual Studio
                        //version of a class has an equality operator that isn't in Mono
                        if (!type.IsAbstract && !type.IsGenericType && !object.ReferenceEquals(type.GetConstructor(Type.EmptyTypes), null))
                        {
                            //All types we care about are non-generic, non abstract types with a parameterless constructor
                            if (modInfo == null && typeof(IModInfo).IsAssignableFrom(type))
                            {
                                modInfo = (IModInfo)Activator.CreateInstance(type);
                            } else if (typeof(MonoBehaviour).IsAssignableFrom(type) && type.GetCustomAttributes(typeof(BootstrapAttribute), false).Length != 0)
                            {
                                behaviours.Add(type);
                            }
                        }
                    }

                    if (modInfo != null)
                    {
                        //We found a modinfo so load the mod & any bootstrap behaviours
                        ConsoleSystem.Log(string.Format("Located mod '{0}' with {1} MonoBehaviours to bootstrap.", modInfo.GetPrettyModName(), behaviours.Count));
                        mLoadedMods.Add(modInfo);

                        foreach (Type type in behaviours)
                        {
                            //We do one gameobject per script, to allow the bootstrap scripts to do keep-alives on their gameobjects with some level of
                            //granularity.
							GameObject obj = new GameObject(type.FullName);
							obj.AddComponent(type);
							obj.SendMessage ("ReceiveLeatherConfiguration", config, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                }
            }

            ConsoleSystem.Log("Finished scanning for mods.");
        }
    }
}
