using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class ClientMain : MonoBehaviour {
        // Start is called before the first frame update
        [SerializeField] Camera mainCamera;

        [SerializeField] Canvas screenCanvas;

        [SerializeField] Canvas worldCanvas;


        AppUI ui;
        ModuleInput input;

        ModuleAssets assets;

        ModuleCamera moduleCamera;

        GameContext gameContext;

        bool isTearDown;

        //问题
        // [SerializeField] RoleEntity role;
        // [SerializeField] RoleEntity role;

        void Awake() {
            isTearDown = false;
            // === Phase : Instantiate===
            input = new ModuleInput();
            assets = new ModuleAssets();
            gameContext = new GameContext();
            moduleCamera = new ModuleCamera();
            ui = new AppUI();
            //=== Phase : Inject ===
            ui.Inject(assets, screenCanvas, worldCanvas);
            moduleCamera.Inject(mainCamera);
            gameContext.Inject(ui, assets, input, moduleCamera);

            // === Phase :Init==
            ui.onStartHandle = () => {
                BussinessGame.Enter(gameContext);
                ui.Login_Close();
            };

            assets.Load();

            //=== Phase: Enter Game ===
            // ui.Login_Open();
            ui.Bag_Open(100);



        }
        //记笔记restTD 找出restDT为什么为0 √
        float restDT = 0;
        // Update is called once per frame
        void Update() {

            float dt = Time.deltaTime;
            // === Phase : Input===
            input.Process(moduleCamera.camera.transform.rotation);

            //=== Phase : Login===
            // fixeDT系统的固定时间
            float fixedDT = Time.fixedDeltaTime; // 0.02
            // dt会变化
            // 这个代码保证了每一帧都会运行一次
            restDT += dt;// 0.0083 (0.0000000001, 10)
            if (restDT >= fixedDT) {
                while (restDT >= fixedDT) {
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

        void OnApplicationQuit() {
            TearDown();
        }
        void OnDestroy() {
            TearDown();
        }
        void TearDown() {
            if (isTearDown) {
                return;
            }
            isTearDown = true;
            assets.Unload();
        }

    }


}
