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
        float restDT = 0;
        // Update is called once per frame
        void Update() {
            float dt = Time.deltaTime;
            // === Phase : Input===
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
            input.moveAxis = moveAxis;
            // 左右和上下要分开写
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

            Physics.Simulate(dt);
        }
    }


}
