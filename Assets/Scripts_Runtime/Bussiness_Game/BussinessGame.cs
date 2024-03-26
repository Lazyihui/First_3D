using System;
using UnityEngine;
namespace Zelda {
    public static class BussinessGame {
        public static void Enter(GameContext ctx) {
            RoleEntity owner = RoleDomain.Spawn(ctx, 0);
            ctx.onwerRoleID = owner.id;
        }
        public static void FixedTick(GameContext ctx, float fixdt) {
            bool hasOwner = ctx.roleRepository.TryGet(ctx.onwerRoleID, out RoleEntity owner);
            ModuleInput input = ctx.input;
            owner.Move(input.moveAxis, fixdt);
            owner.Face(input.moveAxis, fixdt);
            // 记笔记 先检测再起跳
            // CheckGround();

            owner.Jump(input.isJump);
            if (input.isAttack) {
                owner.Anim_Attack();
            }
        }
        static void CheckGround(RoleEntity role) {
            RaycastHit[] hits = Physics.RaycastAll(role.transform.position + Vector3.up, Vector3.down, 1.05f);
            if (hits != null) {
                for (int i = 0; i < hits.Length; i++) {
                    var hit = hits[i];
                    if (hit.collider.CompareTag("Ground")) {
                        role.SetGround(true);
                        break;
                    }
                }
            }
        }
    }
}