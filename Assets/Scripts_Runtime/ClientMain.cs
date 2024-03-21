using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda
{

    public class ClientMain : MonoBehaviour
    {
        // Start is called before the first frame update

        ModuleInput input;

        [SerializeField] GameObject role;
        void Awake()
        {
            // === Phase : Instantiate===
            input = new ModuleInput();
            //=== Phase : Inject ===

            // === Phase :Init==

            //=== Phase: Enter Game ===


            Debug.Log("hello");
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;
            // === Phase : Input===
            Vector2 moveAxis = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveAxis.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveAxis.y = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveAxis.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveAxis.x = 1;
            }
            // 左右和上下要分开写
            moveAxis.Normalize();
            role.transform.position += new Vector3(moveAxis.x, 0, moveAxis.y) * dt * 5;
            Debug.Log("moveAxis:" + moveAxis.normalized);
            //=== Phase : Login===

            //=== Phase : Draw===
        }
    }
}
