using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class ClientMain : MonoBehaviour {
        // Start is called before the first frame update

        ModuleInput input;

        //问题
        // [SerializeField] RoleEntity role;
        [SerializeField] RoleEntity role;

        void Awake() {
            // === Phase : Instantiate===
            input = new ModuleInput();
            //=== Phase : Inject ===

            // === Phase :Init==

            //=== Phase: Enter Game ===


            Debug.Log("hello");
        }
        //记笔记restTD 找出restDT为什么为0
        float restDT = 0;
        // Update is called once per frame
        void Update() {
            float dt = Time.deltaTime;
            // === Phase : Input===
            input.Process();

            //=== Phase : Login===
            float fixedDT = Time.fixedDeltaTime; // 0.02
            restDT += dt;// 0.0083 (0.0000000001, 10)
            if (restDT >= fixedDT) {
                while (restDT > 0) {
                    restDT -= fixedDT;
                    FixedTick(fixedDT);
                }
            } else {
                FixedTick(restDT);
                restDT = 0;
            }
            //=== Phase : Draw===
        }
        void FixedTick(float dt) {

            role.Move(input.moveAxis, dt);
            role.Face(input.moveAxis, dt);
            // 记笔记 先检测再起跳
            RaycastHit[] hits = Physics.RaycastAll(role.transform.position + Vector3.up, Vector3.down, 1.05f);
            if (hits != null) {
                for (int i = 0; i < hits.Length; i++) {
                    var hit = hits[i];
                    if (hit.collider.CompareTag("Ground")) {
                        role.SetGround(true);
                        break;
                    }
                }
                Debug.Log("isGrounded" + role.isGrounded);
            }
            role.Jump(input.isJump);

            if (input.isAttack) {
                role.Anim_Attack();
            }
            Physics.Simulate(dt);
        }
    }


}
