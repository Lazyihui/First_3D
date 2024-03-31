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
        //旋转
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