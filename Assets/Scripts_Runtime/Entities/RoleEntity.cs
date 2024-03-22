using System;
using UnityEngine;

namespace Zelda {
    public class RoleEntity : MonoBehaviour {
        [SerializeField] Transform bodyTF;
        [SerializeField] Rigidbody rb;
        [SerializeField] public float mvoeSpeed;
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

            Vector3 newForward = new Vector3(moveAxis.x, 0, moveAxis.y);
            transform.rotation = Quaternion.LookRotation(newForward);
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