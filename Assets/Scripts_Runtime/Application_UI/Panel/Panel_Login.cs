using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zelda {

    public class Panel_Login : MonoBehaviour {


        [SerializeField] Button btnStart;

        [SerializeField] Button btnSetting;

        [SerializeField] Button btnStaff;

        [SerializeField] Button btnExit;

        public Action onStartHandle;

        public Action onSettingHandel;

        public Action onStaffHandle;

        public Action onExitHandle;

        public void Ctor() {

            btnStart.onClick.AddListener(() => {
                onStartHandle?.Invoke();
            });

            btnSetting.onClick.AddListener(() => {
                onSettingHandel?.Invoke();
            });

            btnStaff.onClick.AddListener(() => {
                onStaffHandle?.Invoke();
            });

            btnExit.onClick.AddListener(() => {
                onExitHandle?.Invoke();
            });



        }


    }
}