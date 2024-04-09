using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {


    public class AppUI {

        ModuleAssets assets;

        // 注入的方法
        Canvas screenCanvas;

        Canvas worldCanvas;
        Panel_Login panel_Login;

        Panel_Bag bag;
        Dictionary<int, HUD_HpBar> hpBars;
        public Action onStartHandle;

        public Action<int> Bag_onUseHandle;
        public AppUI() {
            hpBars = new Dictionary<int, HUD_HpBar>();

        }

        public void Inject(ModuleAssets assets, Canvas screenCanvas, Canvas worldCanvas) {
            // 得到的东西一样 但是如果unity里面改了名字 第一个就会出问题 第二个不会
            // prefabDict.Add("Panel_Login", loginPrefab.gameObject);
            // prefabDict.Add(nameof(Panel_Login), loginPrefab.gameObject);
            // prefabDict.Add(nameof(HUD_HpBar), hpBarPrefab.gameObject);

            this.assets = assets;
            this.screenCanvas = screenCanvas;
            this.worldCanvas = worldCanvas;
        }
        // ==== Panel_Login ====
        public void Login_Open() {

            GameObject go = Open(nameof(Panel_Login), screenCanvas);
            panel_Login = go.GetComponent<Panel_Login>();
            panel_Login.Ctor();
            panel_Login.onStartHandle = () => {
                //    要改
                onStartHandle?.Invoke();
            };

        }

        public void Login_Close() {
            GameObject.Destroy(panel_Login.gameObject);
            panel_Login = null;
        }



        // ==== Panel_HpBar ====
        // public void HpBar_Open(int id, float hp, float maxHp) {
        //     // if (hpBars == null) {
        //     //     hpBars = new Dictionary<int, GameObject>();
        //     // }
        //     // if (hpBars.ContainsKey(id)) {
        //     //     Debug.LogError("已经存在了");
        //     //     return;
        //     // }
        //     // 记笔记
        //     GameObject go = Open(nameof(HUD_HpBar), worldCanvas);
        //     HUD_HpBar hpBar = go.GetComponent<HUD_HpBar>();
        //     hpBar.Ctor();
        //     hpBar.SetHp(hp, maxHp);
        //     hpBars.Add(id, hpBar);
        // }

        public void HpBar_Open(int id, float hp, float hpMax) {
            GameObject go = Open(nameof(HUD_HpBar), worldCanvas);
            HUD_HpBar hpBar = go.GetComponent<HUD_HpBar>();
            hpBar.Ctor();
            hpBar.SetHp(hp, hpMax);
            hpBars.Add(id, hpBar);
        }

        public void HpBar_UpdataPostion(int id, Vector3 postion, Vector3 cameraForward) {

            hpBars.TryGetValue(id, out HUD_HpBar hpBar);
            hpBar.SetPos(postion, cameraForward);
        }

        //  ==== Panel_Bag ====
        // 打开背包时生成空格子
        public void Bag_Open(int maxSlotCount) {
            if (bag == null) {

                GameObject go = Open(nameof(Panel_Bag), screenCanvas);
                Panel_Bag panel = go.GetComponent<Panel_Bag>();
                panel.Ctor();
                panel.onUseHandle = (int id) => {
                    Bag_onUseHandle?.Invoke(id);
                };
                this.bag = panel;
            }
            bag.Init(maxSlotCount);
        }
        public void Bag_Add(int id, Sprite icon, int count) {
            bag?.Add(id, icon, count);
            // 等于bag不为空的时候才会调用
        }

        public void Bag_Close() {
            bag?.Close();
        }




        GameObject Open(string uiName, Canvas canvas) {

            bool has = assets.TryGetUIPrefab(uiName, out GameObject prefab);
            if (!has) {
                Debug.LogError($"UI: {uiName} not found.");
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab, canvas.transform);
            return go;
        }

    }
}