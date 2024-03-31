using System;
using UnityEngine;
namespace Zelda {
    public static class BussinessGame {
        public static void Enter(GameContext ctx) {
            RoleEntity owner = RoleDomain.Spawn(ctx, 0);
            ctx.onwerRoleID = owner.id;
        }
        public static void FixedTick(GameContext ctx, float fixdt) {

            ModuleInput input = ctx.input;
            bool hasOwner = ctx.roleRepository.TryGet(ctx.onwerRoleID, out RoleEntity owner);

            owner.Move(input.moveCmaeraDir, fixdt);
            owner.Face(input.moveCmaeraDir, fixdt);
            // 记笔记 先检测再起跳
            // CheckGround();

            owner.Jump(input.isJump);
            if (input.isAttack) {
                owner.Anim_Attack();
            }
        }

        public static void LateTick(GameContext ctx, float dt) {
            // 相机跟随
            ModuleCamera moduleCamera = ctx.moduleCamera;
            bool hasOwner = ctx.roleRepository.TryGet(ctx.onwerRoleID, out RoleEntity role);
            if (hasOwner) {
                moduleCamera.Follow(role.transform.position, 2, 3);
            }

            // 相机旋转
            moduleCamera.Rotate(ctx.input.cameraRoationAxis,dt);
            // moduleCamera.RotateByEuler(ctx.input.cameraRoationAxis,dt);

        }
        // 检测地面代码
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