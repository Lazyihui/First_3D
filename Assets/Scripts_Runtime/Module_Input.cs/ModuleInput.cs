using UnityEngine;
namespace Zelda {
    public class ModuleInput {
        Vector2 moveAxis;//移动反向

        public bool isAttack;//攻击

        public bool isJump;//跳跃
        public Vector3 moveCmaeraDir;

        public Vector3 cameraRoationAxis;

        public ModuleInput() {
            isAttack = false;
            isJump = false;
        }

        public void Process(Quaternion cameraRotation) {
            // ===角色移动===
            Vector2 moveAxis = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) {
                moveAxis.y = 1;
            } else if (Input.GetKey(KeyCode.S)) {
                moveAxis.y = -1;
            }
            if (Input.GetKey(KeyCode.A)) {
                moveAxis.x = -1;
            } else if (Input.GetKey(KeyCode.D)) {
                moveAxis.x = 1;
            }
            this.moveAxis = moveAxis;


            // Camera cam = ctx.moduleCamera.camera;
            // Quaternion quaternion = cam.transform.rotation;
            //=== 相机面向的方向 ===
            moveCmaeraDir = new Vector3(moveAxis.x, 0, moveAxis.y);
            moveCmaeraDir = cameraRotation * moveCmaeraDir;
            // 四元数*Vector3 = 旋转后的Vector3

            // 左右和上下要分开写
            // if (Input.GetKeyDown(KeyCode.Space)) {
            //     input.isAttack = true;
            // } else {
            //     input.isAttack = false;
            // }
            // === 攻击 ===
            this.isAttack = Input.GetKeyDown(KeyCode.F);
            // === 跳跃 ===
            this.isJump = Input.GetKeyDown(KeyCode.Space);
            // === 相机旋转 ===
            if(Input.GetMouseButton(1)){
                // float x = Input.GetAxis("Mouse X");
                // float y = Input.GetAxis("Mouse Y");
                // moveRoationAxis = new Vector3(x, y, 0);
                //delta是上一帧和这一帧的差值
                cameraRoationAxis = Input.mousePositionDelta;
            }
        }
    }
}