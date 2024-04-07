using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zelda {
    public class Panel_Bag : MonoBehaviour {
        // 背包内容
        [SerializeField] GridLayoutGroup group;
        [SerializeField] Panel_BagElement prefabElement;
        // 用来存储背包中的物品  用于记录物品的id
        List<Panel_BagElement> elements;

        public void Ctor() {
            elements = new List<Panel_BagElement>();
        }

        public void Init(int maxSlotCount) {
            for (int i = 0; i < maxSlotCount; i++) {
                Panel_BagElement ele = GameObject.Instantiate(prefabElement, group.transform);
                ele.Init(-1, null, 0);
                elements.Add(ele);
            }

        }

        // 背包 有添加物品 和 移除物品的方法 关闭背包的方法
        public void Add(int id, Sprite icon, int count) {
            for(int i =0; i<elements.Count;i++){
                Panel_BagElement ele = elements[i];
                if(ele.id == -1){
                    ele.Init(id,icon,count);
                    break;
                }
            }
        }
        public void Remove(int id) {

            int index = elements.FindIndex(ele => ele.id == id);
            if (index != -1) {
                GameObject.Destroy(elements[index].gameObject);
                elements.RemoveAt(index);
            }
        }

        public void Close() {

            foreach (var ele in elements) {
                GameObject.Destroy(ele.gameObject);
            }
            GameObject.Destroy(gameObject);
        }
    }
}