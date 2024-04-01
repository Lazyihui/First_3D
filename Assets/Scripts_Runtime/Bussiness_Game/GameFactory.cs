using System.Collections;
using UnityEngine;
namespace Zelda{

// /工厂的作用是创建实体 所有实体都写这里
    public static class GameFactory{

        public static RoleEntity Role_Create(ModuleAssets assets,int typeID){
            bool has = assets.TryGetEntity("Entity_Role",out var go);
            if(!has){
                    Debug.LogError("Role prefab not found");
                    return null;
            }

            go = GameObject.Instantiate(go);
            return go.GetComponent<RoleEntity>();

        }
    }
}