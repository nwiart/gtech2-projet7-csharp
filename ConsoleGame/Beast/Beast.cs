using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Beast
{
	public class Beast
	{
		// Beast registry, listing all beasts by their registry ID.
		private static Dictionary<string, Beast> _beastsByRegistryID;

		// Beast registration.
		static Beast()
		{
			_beastsByRegistryID = new Dictionary<string, Beast>();

			registerBeast("leggedthing", new Beast("Truc à Pats"));
			registerBeast("ambush", new Beast("Embuisscade"));
			registerBeast("papiermachette", new Beast("Origamonstre"));
		}

		private static void registerBeast(string registryID, Beast beast)
		{
			_beastsByRegistryID.Add(registryID, beast);
			beast._registryID = registryID;
		}

		public static Beast? GetBeastByID(string registryID)
		{
			return _beastsByRegistryID[registryID];
		}



		private string? _registryID;
		public string? RegistryID
		{
			get { return _registryID; }
		}

		public string Name { get; }

		Beast(string name)
		{
			_registryID = null;
			Name = name;
		}
	}
}
