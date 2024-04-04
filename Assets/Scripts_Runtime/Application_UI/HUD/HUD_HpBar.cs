using UnityEngine;
using UnityEngine.UI;

namespace Zelda {

    public class HUD_HpBar : MonoBehaviour {
        [SerializeField] Image imgBg;
        [SerializeField] Image imgBar;

        public void Ctor() { }

        public void SetHp(float hp ,float maxHp) {

            if(maxHp == 0){
                imgBar.fillAmount = 0;
                imgBg.fillAmount = 0;             
                return;
            }
            imgBar.fillAmount = hp / maxHp;
         }

         public void SetPos(Vector3 pos,Vector3 cameraForward){
             transform.position = pos;
             transform.forward = cameraForward;
         }
    }
}