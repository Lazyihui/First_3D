using System;
using UnityEngine;
namespace Zelda {
    public static class BussinessGame {
        public static void Enter(GameContext ctx) {
            RoleEntity owner = RoleDomain.Spawn(ctx, 0);
            ctx.onwerRoleID = owner.id;

            // int occupiedSlot = owner.bagCom.GetOccpiedSlot();
            LootEntity loot1 = LootDomain.Spawn(ctx, 1, 10, new Vector3(0, 1, 5));
            LootEntity loot2 = LootDomain.Spawn(ctx, 2, 10, new Vector3(2, 1, 8));
            LootEntity loot3 = LootDomain.Spawn(ctx, 4, 10, new Vector3(1, 1, 1));
            LootEntity loot4 = LootDomain.Spawn(ctx, 3, 10, new Vector3(2, 1, 1));
            LootEntity loot5 = LootDomain.Spawn(ctx, 8, 10, new Vector3(3, 1, 1));
            LootEntity loot6 = LootDomain.Spawn(ctx, 7, 10, new Vector3(4, 1, 1));




        }
        public static void FixedTick(GameContext ctx, float fixdt) {

            ModuleInput input = ctx.input;
            bool hasOwner = ctx.roleRepository.TryGet(ctx.onwerRoleID, out RoleEntity owner);
            if (!hasOwner) {
                return;
            }
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
            if (!hasOwner) {
                return;
                // moduleCamera.Follow(role.transform.position, 2, 3);
            }


            RoleDomain.UpdateHUD(ctx, moduleCamera.camera.transform.forward, role);

            // 相机看向
            // moduleCamera.LookAt(role.transform.position);
            // 注：看向会影响旋转，所以旋转失效
            // 注：绕会影响看向和跟随
            // 相机旋转
            // 因为有看向所以旋转失效
            // moduleCamera.Rotate(ctx.input.cameraRotationAxis,dt);
            // moduleCamera.RotateByEuler(ctx.input.cameraRoationAxis,dt);
            moduleCamera.Round(role.transform.position, ctx.input.cameraRotationAxis, new Vector2(0, 0), 5, dt);
            ModuleInput input = ctx.input;
            if (input.isUIToggleBag) {
                BagDomain.Toggle(ctx, role.bagCom);
            }

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