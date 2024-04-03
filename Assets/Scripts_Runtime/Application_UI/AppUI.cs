
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {


    public class AppUI {

        Dictionary<string, GameObject> prefabDict;

        // 注入的方法
        Canvas screenCanvas;
        Panel_Login login;

        public AppUI() {
            prefabDict = new Dictionary<string, GameObject>();

        }

        public void Inject(Canvas screenCanvas,Panel_Login loginPrefab) {
            // 得到的东西一样 但是如果unity里面改了名字 第一个就会出问题 第二个不会
            // prefabDict.Add("Panel_Login", loginPrefab.gameObject);
            prefabDict.Add(nameof(Panel_Login), loginPrefab.gameObject);
            this.screenCanvas = screenCanvas;
        }

        public void Login_Open() {

            GameObject go = Open(nameof(Panel_Login));
            login = go.GetComponent<Panel_Login>();
            login.Ctor();

        }
        GameObject Open(string uiName) {

            bool has = prefabDict.TryGetValue(uiName, out GameObject prefab);
            if (!has) {
                Debug.LogError("没有找到对应的UI");
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab,screenCanvas.transform);
            return go;
        }

        void Close(string uiName) { }
    }
}