using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class ClientMain : MonoBehaviour {
        // Start is called before the first frame update
        [SerializeField] Camera mainCamera;
        [SerializeField] Panel_Login loginPrefab;

        [SerializeField] Canvas screenCanvas;

        [SerializeField] Canvas worldCanvas;

        [SerializeField] HUD_HpBar hpBarPrefab;

        AppUI ui;
        ModuleInput input;

        ModuleAssets assets;

        ModuleCamera moduleCamera;

        GameContext gameContext;

        //问题
        // [SerializeField] RoleEntity role;
        // [SerializeField] RoleEntity role;

        void Awake() {
            // === Phase : Instantiate===
            input = new ModuleInput();
            assets = new ModuleAssets();
            gameContext = new GameContext();
            moduleCamera = new ModuleCamera();
            ui = new AppUI();
            //=== Phase : Inject ===
            ui.Inject(screenCanvas, worldCanvas,loginPrefab,hpBarPrefab);
            moduleCamera.Inject(mainCamera);
            gameContext.Inject(ui,assets, input, moduleCamera);

            // === Phase :Init==
            ui.onStartHandle = () => {
                BussinessGame.Enter(gameContext);
                ui.Login_Close();
            };

            assets.Load();

            //=== Phase: Enter Game ===
            ui.Login_Open();


            Debug.Log("hello");
        }
        //记笔记restTD 找出restDT为什么为0
        float restDT = 0;
        // Update is called once per frame
        void Update() {
            float dt = Time.deltaTime;
            // === Phase : Input===
            input.Process(moduleCamera.camera.transform.rotation);

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
            LateTick(dt);
        }
        void FixedTick(float dt) {
            // === Phase:Logic===
            BussinessGame.FixedTick(gameContext, dt);
            // === phade: Simulate===
            Physics.Simulate(dt);
        }

        void LateTick(float dt) {
            BussinessGame.LateTick(gameContext, dt);
        }


    }


}
