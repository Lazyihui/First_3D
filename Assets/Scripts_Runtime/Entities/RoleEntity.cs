using System;
using UnityEngine;
using UnityEngine.AI;

namespace Zelda {
    public class RoleEntity : MonoBehaviour {
        [SerializeField] Transform bodyTF;
        [SerializeField] Rigidbody rb;

        [SerializeField] Animator anim;
        [SerializeField] public float mvoeSpeed;
        Vector3 oldForward;
        //对比一下 记录 之前是 Vector2 startForward
        Vector3 startForward;

        Vector3 endForward;

        public int id;

        float time;

        float duration;

        public bool isGrounded;
        // ui
        public float hp;
        public float maxHp;

        public BagComponent bagCom;
        public Action<RoleEntity, Collision> OnCollisionEnterHandle;

        public Action<RoleEntity, Collider> OnTriggerEnterHandle;

        public void Ctor() {
            bagCom = new BagComponent();
        }

        public void Init(int bagComMaxSlot) {
            bagCom.Init(bagComMaxSlot);

        }
        //问题 为什么两个Move可以一起 √ 因为参数不一样 所以是两个不一样的函数
        public void Move(Vector3 moveAxis, float dt) {
            Move(moveAxis, this.mvoeSpeed, dt);
            // mangnitude 速度的大小 记笔记
            if (moveAxis != Vector3.zero) {
                anim.SetFloat("F_MoveSpeed", rb.velocity.magnitude);
            } else {
                anim.SetFloat("F_MoveSpeed", 0);
            }
        }

        public void Jump(bool isJumpKeyDown) {
            if (isJumpKeyDown && isGrounded) {
                Vector3 velo = rb.velocity;
                velo.y = 5;
                rb.velocity = velo;
                isGrounded = false;
            }
        }

        public void SetGround(bool isGround) {
            this.isGrounded = isGround;
        }
        // 记笔记
        public void Anim_Attack() {
            anim.SetTrigger("T_Attack");
        }

        // public void Face(Vector2 moveAxis, float dt) {
        //     if (moveAxis == Vector2.zero) {
        //         return;
        //     }
        //     Vector3 newForward = new Vector3(moveAxis.x, 0, moveAxis.y).normalized;
        //     if (oldForward != newForward) {
        //         oldForward = newForward;

        //         startForward = oldForward;//缓动的开始
        //         if (startForward == Vector2.zero) {
        //             startForward = transform.forward;
        //         }

        //         endForward = newForward;//缓动的结束
        //         time = 0;
        //         duration = 0.1f;
        //         transform.forward = newForward;
        //     }
        //     // 硬转
        //     // transform.forward = newForward;
        //     // 平滑转
        //     if (time <= duration) {
        //         time += dt;
        //         float t = time / duration;
        //         //lerp缓动函数 forward
        //         // Vector3 forward = Vector3.Lerp(startForward, endForward, t);
        //         // transform.forward = forward;
        //         Quaternion startRot = Quaternion.LookRotation(startForward); // 旋转到startForward
        //         Quaternion endRot = Quaternion.LookRotation(endForward); // 旋转到endForward
        //         transform.rotation = Quaternion.Slerp(startRot, endRot, t);
        //     }
        // }
        // 对比和上面的区别
        public void Face(Vector3 moveAxis, float dt) {

            if (moveAxis == Vector3.zero) {
                return;
            }

            // 根据正面进行旋转
            // old forward: (x0, y0, z1)
            // new forward: (moveAxis.x, 0, moveAxis.y)
            Vector3 newForward = new Vector3(moveAxis.x, 0, moveAxis.z).normalized;
            if (oldForward != newForward) {
                startForward = oldForward; // 缓动开始
                if (startForward == Vector3.zero) {
                    startForward = transform.forward;
                }
                endForward = newForward; // 缓动结束
                time = 0;
                duration = 0.25f;
                oldForward = newForward;
            }
            // transform.rotation = Quaternion.LookRotation(newForward);

            // 硬转
            // transform.forward = newForward;

            // 平滑转
            if (time <= duration) {
                time += dt;
                float t = time / duration;
                Quaternion startRot = Quaternion.LookRotation(startForward);
                Quaternion endRot = Quaternion.LookRotation(endForward);
                transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            }

        }
        // normalize and normalized 区别
        public void Move(Vector3 moveAxis, float moveSpeed, float dt) {
            // 移动的时候不能改y轴,改变y轴会导致角色飞起来 不落地了 记笔记
            // 过程也记笔记
            Vector3 velo = rb.velocity;
            float oldY = velo.y;
            Vector3 moveDir = new Vector3(moveAxis.x, 0, moveAxis.z);
            moveDir.Normalize();
            //  记 veloctity
            velo = moveDir * moveSpeed;
            velo.y = oldY;
            rb.velocity = velo;

        }

        void OnCollisionEnter(Collision other) {
            OnCollisionEnterHandle.Invoke(this, other);
        }
        void OnCollisionStay(Collision other) {
        }
        void OnCollisionExit(Collision other) {
        }

        void OnTriggerEnter(Collider other) {
            OnTriggerEnterHandle.Invoke(this, other);

        }
        void OnTriggerStay(Collider other) {
        }
        void OnTriggerExit(Collider other) {
        }
    }
}