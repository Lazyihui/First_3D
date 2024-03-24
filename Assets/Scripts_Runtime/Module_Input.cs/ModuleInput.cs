using UnityEngine;
namespace Zelda {
    public class ModuleInput {
        public Vector2 moveAxis;//移动反向

        public bool isAttack;//攻击

        public bool isJump;//跳跃

        public ModuleInput() {
            isAttack = false;
            isJump = false;
        }
    }
}