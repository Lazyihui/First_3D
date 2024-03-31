using UnityEngine;

namespace Zelda {

    public class ModuleCamera {

        public Camera camera;

        public ModuleCamera() { }

        public void Inject(Camera camera) {
            this.camera = camera;
        }
        // 跟随
        public void Follow(Vector3 target, float height, float radius) {
            camera.transform.position = new Vector3(target.x, target.y + height, target.z - radius);
        }

        //看向
        public void LookAt(Vector3 target) {
            Vector3 dir = target - camera.transform.position;
            camera.transform.forward = dir.normalized;
            // camera.transform.LookAt(target);

        }
        // 绕
        // 这个是绕着目标点旋转
        public void Round(Vector3 targetPos, Vector2 rotateAxisCamera, Vector2 followOffset, float radius, float dt) {

            float sensitivity = 60f * dt;
            // 旋转的角度 * 灵敏度
            rotateAxisCamera *= sensitivity;
            // 目标点到相机的方向
            Vector3 targetToCameraDir = camera.transform.position - targetPos;
            targetToCameraDir.Normalize();
            //  得到一个绕目标点到相机的方向旋转的四元数
            Quaternion oldRot = Quaternion.Euler(targetToCameraDir);
            // 
            Vector3 oldEuler = camera.transform.eulerAngles;

            // 向量 * 四元数 = 旋转后的向量
            // 旋转后的向量 * 四元数 = 旋转后的向量

            // 四元数 * 四元数 = 四元数
            // 四元数 * 四元数 = 四元数
            // 四元数 * 向量 = 旋转后的向量

            // 绕 X 轴旋转
            Quaternion newRot = oldRot;
            float targetXEuler = oldEuler.x + rotateAxisCamera.y;

            if (targetXEuler > 0 && targetXEuler < 90) {
                // roateAxis.y 是相机的旋转角度  0-90 之间 camera.transform.right 是绕着哪一个轴旋转
                // 是相机在旋转 绕着自己的右边轴旋转 x轴
                Quaternion xRot = Quaternion.AngleAxis(rotateAxisCamera.y, camera.transform.right);
                targetToCameraDir = xRot * targetToCameraDir;
            }

            // 绕 Y 轴旋转
            Quaternion yRot = Quaternion.AngleAxis(rotateAxisCamera.x, Vector3.up);
            targetToCameraDir = yRot * targetToCameraDir;
            // newDir 只是等效替代了一下
            Vector3 newDir = newRot * targetToCameraDir;
            newDir.Normalize();
            // 乘以半径 是相机和目标的距离
            newDir *= radius;

            camera.transform.position = targetPos + newDir;
            // 相机指向目标
            camera.transform.forward = (targetPos - camera.transform.position).normalized;
        }
        //旋转
        // 单纯的相机旋转
        public void Rotate(Vector2 axis, float dt) {

            float sensitivity = 60f * dt;

            // Quaternion quaternion = camera.transform.rotation;
            // quaternion * Vector3.forward = camera.transform.forward

            Vector3 fwd = camera.transform.forward;
            //四元数*Vector3 = 旋转后的Vector3

            // 绕x轴旋转
            Quaternion xQuaternion = Quaternion.AngleAxis(-axis.y * sensitivity, Vector3.right);

            // 绕y轴旋转
            Quaternion yQuaternion = Quaternion.AngleAxis(axis.x * sensitivity, Vector3.up);

            fwd = yQuaternion * xQuaternion * fwd;
            // fwd = yQuaternion * fwd;

            camera.transform.forward = fwd;

        }

        public void RotateByEuler(Vector2 axis, float dt) {
            // 欧拉角 有万象解锁问题
            float sensitivity = 60f * dt;
            Quaternion originalRotation = camera.transform.rotation;
            Vector3 euler = originalRotation.eulerAngles;
            euler += new Vector3(-axis.y * sensitivity, axis.x * sensitivity, 0);
            camera.transform.eulerAngles = euler;


        }

    }
}