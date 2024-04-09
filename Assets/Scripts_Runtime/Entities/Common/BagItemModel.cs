using UnityEngine;
namespace Zelda {


    public class BagItemModel {
        // ID
        public int id;

        public int typeID;
        public string name;

        public string description;

        public Sprite icon;


        // 数量
        public int count;
        public int countMax;

        // 特性（通用）
        public bool isConsumable; // 可消耗


        // 特点
        public bool isEatable;

        public int eatRestoreHP;

        // public bool isEquipable;

        // public bool isCastable; //技能

    }
}