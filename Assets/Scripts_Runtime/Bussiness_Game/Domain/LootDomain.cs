using UnityEngine;

namespace Zelda {

    public static class LootDomain {

        public static LootEntity Spawn(GameContext ctx, int itemTypeID, int itemCount, Vector3 pos) {
            LootEntity loot = GameFactory.Loot_Create(ctx.assets, itemTypeID, ctx.idServices, itemCount, pos);
            ctx.lootRepository.Add(loot);
            return loot;
        }

        public static void UnSpawn(GameContext ctx, LootEntity loot) {
            ctx.lootRepository.Remove(loot);
            GameObject.Destroy(loot.gameObject);
        }
    }
}