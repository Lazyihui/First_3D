using UnityEngine;
using UnityEngine.UI;

namespace Zelda {


    public class Panel_BagElement : MonoBehaviour {


        public int id;

        [SerializeField] Image imgIcon;
        [SerializeField] Text txtCount;

        public void Ctor() { }

        public void Init(int id, Sprite icon, int count) {
            this.id = id;
            imgIcon.sprite = icon;

            if (count <= 0) {
                txtCount.text = null;
            } else {
                txtCount.text = count.ToString();
            }
        }
    }
}