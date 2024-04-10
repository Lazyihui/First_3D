using System;
using UnityEngine;
using System.Collections;


namespace Zelda {
    public static class RoleDomain {

        public static RoleEntity Spawn(GameContext ctx, int typeID) {
            RoleEntity role = GameFactory.Role_Create(ctx.assets, typeID,ctx.idServices);

            // UI
            ctx.ui.HpBar_Open(role.id, role.hp, role.maxHp);

            // 这里是一个委托
            role.OnCollisionEnterHandle = OnCollisionEnter;

            role.OnTriggerEnterHandle = (role, other) => {

                LootEntity loot = other.gameObject.GetComponent<LootEntity>();
                if(loot!=null){
                    Debug.Log("拾取了物品"+loot.itemTypeDI);
                }

                Debug.Log("OnTriggerEnterHandle");
                // if (other.gameObject.CompareTag("Loot")) {
                //     // LootEntity loot = other.gameObject.GetComponent<LootEntity>();
                //     // role.bagCom.Add(loot.itemTypeDI, loot.count);
                //     // ctx.lootRepository.Remove(loot);
                //     // GameObject.Destroy(other.gameObject);
                // }
            };
            ctx.roleRepository.Add(role);
            return role;
        }
        // 委托的实现 就是直接等于就好了 是语法
        static void OnCollisionEnter(RoleEntity role, Collision other) {
            if (other.gameObject.CompareTag("Ground")) {
                role.SetGround(true);
            }
        }


        public static void UpdateHUD(GameContext ctx, Vector3 cameraForward,RoleEntity role) {
            // 更新血条
            ctx.ui.HpBar_UpdataPostion(role.id, role.transform.position + Vector3.up * 2.5f,cameraForward);
        }
    }
}