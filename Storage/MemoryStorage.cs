using System;
using System.Collections.Generic;
using Steamworks;

namespace SkillSets.Storage
{
	public class MemoryStorage : ISkillSetsUsersStorage
	{
		Dictionary<ulong, string> Memory;

		public string Get(CSteamID steamId)
		{
            Memory.TryGetValue(steamId.m_SteamID, out string result);
            return result;
		}

		public void Load()
		{
			Memory = new Dictionary<ulong, string>();
		}

		/// <summary>
		/// Useless in a case of memory storage
		/// </summary>
		public void PeriodicSave() {}

		public bool Remove(CSteamID steamId)
		{
			if (Memory.ContainsKey(steamId.m_SteamID))
			{
				Memory.Remove(steamId.m_SteamID);
				return true;
			}
			return false;
		}

		public bool Save(CSteamID steamId, string skillSetName)
		{
			Memory[steamId.m_SteamID] = skillSetName;
			return true;
		}

		public void Unload()
		{
			Memory.Clear();
			Memory = null;
		}
	}
}
