using System;
using System.Collections.Generic;

namespace Zelda {
    public class LootRepository {
        Dictionary<int, LootEntity> all;

        public LootRepository() {
            all = new Dictionary<int, LootEntity>();
        }

        public void Add(LootEntity loot) {
            all.Add(loot.id, loot);

        }

        public void Remove(LootEntity loot) {
            all.Remove(loot.id);
        }
        public bool TryGet(int id, out LootEntity loot) {
            return all.TryGetValue(id, out loot);
        }
        //不懂这个方法的作用
        public void Foreach(Action<LootEntity> action) {
            foreach (var loot in all.Values) {
                action(loot);
            }
        }

    }
}