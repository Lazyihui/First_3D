using System.Collections;
using UnityEngine;
namespace Zelda {

    // /工厂的作用是创建实体 所有实体都写这里
    public static class GameFactory {

        public static RoleEntity Role_Create(ModuleAssets assets, int typeID, IDSercices idServices) {
            bool has = assets.TryGetEntity("Entity_Role", out var go);
            if (!has) {
                Debug.LogError("Role prefab not found");
                return null;
            }

            go = GameObject.Instantiate(go);
            RoleEntity role = go.GetComponent<RoleEntity>();
            role.id = idServices.roleIDRecord++;
            role.Ctor();
            role.Init(20);
            return role;

        }

        public static LootEntity Loot_Create(ModuleAssets assets, int itemTypeID, IDSercices idServices, int itemCount, Vector3 pos) {
            bool has = assets.TryGetEntity("Entity_Loot", out var go);
            if (!has) {
                Debug.LogError("Loot prefab not found");
                return null;
            }

            go = GameObject.Instantiate(go);
            LootEntity loot = go.GetComponent<LootEntity>();
            loot.Setpos(pos);
            loot.itemTypeDI = itemTypeID;
            loot.itemCount = itemCount;
            loot.id = idServices.lootIDRecord++;

            return loot;
        }
    }
}