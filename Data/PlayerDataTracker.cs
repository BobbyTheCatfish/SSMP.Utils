using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSMPUtils.Data
{
    internal class PlayerDataTracker
    {
        internal static PlayerDataTracker ClientInstance = new();
        internal static PlayerDataTracker ServerInstance = new();

        readonly Dictionary<ushort, StoredPlayerData> data = new();

        public StoredPlayerData GetPlayer(ushort id)
        {
            if (data.TryGetValue(id, out StoredPlayerData playerData))
            {
                return playerData;
            }

            playerData = new StoredPlayerData(id);

            data.Add(id, playerData);
            return playerData;
        }

        public List<StoredPlayerData> GetAllData()
        {
            return data.Values.ToList();
        }
    }

    internal class StoredPlayerData
    {
        public ushort Id;
        public int health = 5;
        public int maxHealth = 5;
        public int blueMasks = 0;
        public bool lifebloodState = false;

        public StoredPlayerData(ushort id)
        {
            Id = id;
        }
    }
}
