using System;
using UnityEngine;
using System.Collections;


namespace Zelda {
    public static class RoleDomain {

        public static RoleEntity Spawn(GameContext ctx, int typeID) {
            RoleEntity role = GameFactory.Role_Create(ctx.assets, typeID, ctx.idServics);



            // 这里是一个委托
            role.OnCollisionEnterHandle = OnCollisionEnter;

            role.OnTriggerEnterHandle = (role, other) => {
                OnTriggerEnter(ctx, role, other);

            };

            // UI
            ctx.ui.HpBar_Open(role.id, role.hp, role.maxHp);

            ctx.roleRepository.Add(role);
            return role;
        }

        public static void OnTriggerEnter(GameContext ctx, RoleEntity role, Collider other) {
            LootEntity loot = other.GetComponent<LootEntity>();
            if (loot != null) {
                bool isPicked = role.bagCom.Add(loot.itemTypeDI, loot.itemCount, () => {
                    BagItemModel item = new BagItemModel();
                    // 从模板表里读取物品信息
                    item.id = ctx.idServics.itemIDRecord++;
                    item.typeID = loot.itemTypeDI;
                    item.count = loot.itemCount;
                    return item;
                });
                // 2. 移除 Loot
                if (isPicked) {
                    LootDomain.UnSpawn(ctx, loot);
                } else {
                    // 弹窗/浮字提示: 背包满了
                    Debug.LogWarning("背包满了");
                }

                // 3. 如果背包是打开着的, 则刷新背包
                BagDomain.Update(ctx, role.bagCom);

            }
        }


        // 委托的实现 就是直接等于就好了 是语法
        static void OnCollisionEnter(RoleEntity role, Collision other) {
            if (other.gameObject.CompareTag("Ground")) {
                role.SetGround(true);
            }
        }


        public static void UpdateHUD(GameContext ctx, Vector3 cameraForward, RoleEntity role) {
            // 更新血条
            ctx.ui.HpBar_UpdataPostion(role.id, role.transform.position + Vector3.up * 2.5f, cameraForward);
        }
    }
}