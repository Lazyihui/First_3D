using System.Collections;
using UnityEngine;
namespace Zelda{

    public static class GameFactory{

        public static RoleEntity Role_Create(ModuleAssets assets,int typeID){
            bool has = assets.TryGetEntity("Role",out var go);
            if(!has){
                    Debug.LogError("Role prefab not found");
                    return null;
            }

            go = GameObject.Instantiate(go);
            return go.GetComponent<RoleEntity>();

        }
    }
}