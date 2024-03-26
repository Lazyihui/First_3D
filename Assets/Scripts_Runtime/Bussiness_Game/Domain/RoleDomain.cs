using System;
using System;
using UnityEngine;
using System.Collections;


namespace Zelda{
    public static class RoleDomain{

        public  static RoleEntity Spawn(GameContext ctx,int typeID){
            RoleEntity role = GameFactory.Role_Create(ctx.assets,typeID);
            //这里有问题
            role.OnCollisionEnterHandle = OnCollisionEnter;
            ctx.roleRepository.Add(role);
            return role;
        }

        static void OnCollisionEnter(RoleEntity role,Collision other ){
            if(other.gameObject.CompareTag("Ground")){
                role.SetGround(true);
            }
        }
    }
}