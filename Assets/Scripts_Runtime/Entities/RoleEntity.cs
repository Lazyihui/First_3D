using System;
using UnityEngine;

namespace Zelda {
    public class RoleEntity : MonoBehaviour {
        [SerializeField] Transform bodyTF;
        [SerializeField] Rigidbody rb;
        [SerializeField] public float mvoeSpeed;
        Vector3 oldForward;

        Vector2 startForward;

        Vector3 endForward;

        float time;

        float duration;
        public void Ctor() {
        }
        //问题 为什么两个Move可以一起
        public void Move(Vector2 moveAxis, float dt) {
            Move(moveAxis, this.mvoeSpeed, dt);
        }

        public void Face(Vector2 moveAxis, float dt) {
            if (moveAxis == Vector2.zero) {
                return;
            }



            Vector3 newForward = new Vector3(moveAxis.x, 0, moveAxis.y).normalized;

            if (oldForward != newForward) {
                oldForward = newForward;
                if (startForward == Vector2.zero) {
                    startForward = transform.forward;
                }
                startForward = oldForward;//缓动的开始
                endForward = newForward;//缓动的结束
                time = 0;
                duration = 0.1f;
                transform.forward = newForward;

            }
            // 硬转
            // transform.forward = newForward;
            // 平滑转
            if (time <= duration) {
                time += dt;
                float t = time / duration;
                //lerp缓动函数 forward
                // Vector3 forward = Vector3.Lerp(startForward, endForward, t);
                // transform.forward = forward;

                Quaternion startRot = Quaternion.LookRotation(startForward); // 旋转到startForward
                Quaternion endRot = Quaternion.LookRotation(endForward); // 旋转到endForward
                transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            }
        }
        // normalize and normalized 区别
        public void Move(Vector2 inputAxis, float moveSpeed, float dt) {
            Vector3 moveDir = new Vector3(inputAxis.x, 0, inputAxis.y);
            moveDir.Normalize();
            //  记 veloctity
            rb.velocity = moveDir * moveSpeed;
        }

    }
}