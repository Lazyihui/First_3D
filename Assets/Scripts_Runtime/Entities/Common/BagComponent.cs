using System;
using System.Collections.Generic;


namespace Zelda {
    // 背包组件
    public class BagComponent {

        BagItemModel[] all;

        public BagComponent() {
        }

        public void Init(int maxSlotCount) {
            all = new BagItemModel[maxSlotCount];
        }

        // 添加物品
        public bool Add(int typeID, int count, Func<BagItemModel> onAddItemToNewSlot) {
            // 有两种情况 1.物品已经存在 2.物品不存在
            // 1.物品已经存在 是否可叠加 上限是99
            // 1.物品已经存在 
            for (int i = 0; i < all.Length; i++) {
                BagItemModel old = all[i];
                if (old != null && old.typeID == typeID) {
                    int allCount = old.countMax - old.count;
                    if (allCount >= count) {
                        old.count += count;
                        return true;
                    } else {

                        // eg max = 50 oldcount 48 count 3 继续查找
                        old.count = old.countMax;
                        count -= allCount;
                    }
                }

            }
            if (count > 0) {
                // null 表示空格子
                // 如果没有空格子
                int index = -1;
                for (int i = 0; i < all.Length; i++) {
                    BagItemModel old = all[i];
                    if (old == null) {
                        index = 1;
                        break;
                    }
                }

                if (index == -1) {
                    return false;
                }

                all[index] = onAddItemToNewSlot.Invoke();
                return true;

            } else {

                return true;
            }
        }

        // 查找物品

        // 移除物品

        // 遍历物品

        public int GetMaxSlot() {
            return all.Length;
        }
        public int GetOccpiedSlot() {
            int count = 0;
            for (int i = 0; i < all.Length; i++) {
                if (all[i] != null) {
                    count++;
                }
            }
            return count;
        }

    }
}