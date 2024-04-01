using System;
using UnityEngine;
using System.Collections;


namespace Zelda {
    public static class RoleDomain {

        public static RoleEntity Spawn(GameContext ctx, int typeID) {
            RoleEntity role = GameFactory.Role_Create(ctx.assets, typeID);

            // 这里是一个委托
            role.OnCollisionEnterHandle = OnCollisionEnter;
            ctx.roleRepository.Add(role);
            return role;
        }
        // 委托的实现 就是直接等于就好了 是语法
        static void OnCollisionEnter(RoleEntity role, Collision other) {
            if (other.gameObject.CompareTag("Ground")) {
                role.SetGround(true);
            }
        }
    }
}