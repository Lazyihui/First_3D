using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {


    public class AppUI {

        Dictionary<string, GameObject> prefabDict;

        // 注入的方法
        Canvas screenCanvas;

        Canvas worldCanvas;
        Panel_Login panel_Login;
        Dictionary<int, HUD_HpBar> hpBars;
        public Action onStartHandle;
        public AppUI() {
            prefabDict = new Dictionary<string, GameObject>();
            hpBars = new Dictionary<int, HUD_HpBar>();

        }

        public void Inject(Canvas screenCanvas, Canvas worldCanvas, Panel_Login loginPrefab, HUD_HpBar hpBarPrefab) {
            // 得到的东西一样 但是如果unity里面改了名字 第一个就会出问题 第二个不会
            // prefabDict.Add("Panel_Login", loginPrefab.gameObject);
            prefabDict.Add(nameof(Panel_Login), loginPrefab.gameObject);
            prefabDict.Add(nameof(HUD_HpBar), hpBarPrefab.gameObject);

            this.screenCanvas = screenCanvas;
            this.worldCanvas = worldCanvas;
        }

        public void Login_Open() {

            GameObject go = Open(nameof(Panel_Login),screenCanvas);
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


        #region  HUB_HpBar


        public void HpBar_Open(int id, float hp, float maxHp) {
            // if (hpBars == null) {
            //     hpBars = new Dictionary<int, GameObject>();
            // }
            // if (hpBars.ContainsKey(id)) {
            //     Debug.LogError("已经存在了");
            //     return;
            // }
            // 记笔记
            GameObject go = Open(nameof(HUD_HpBar),worldCanvas);
            HUD_HpBar hpBar = go.GetComponent<HUD_HpBar>();
            hpBar.Ctor();
            hpBar.SetHp(hp, maxHp);
            hpBars.Add(id, hpBar);
        }
        #endregion

        public void HpBar_UpdataPostion(int id, Vector3 postion,Vector3 cameraForward) { 

            hpBars.TryGetValue(id, out HUD_HpBar hpBar);
            hpBar.SetPos(postion,cameraForward);
        }
        GameObject Open(string uiName,Canvas canvas) {

            bool has = prefabDict.TryGetValue(uiName, out GameObject prefab);
            if (!has) {
                Debug.LogError("没有找到对应的UI");
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab, canvas.transform);
            return go;
        }

    }
}