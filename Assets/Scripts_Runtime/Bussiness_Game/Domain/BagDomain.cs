using UnityEngine;

namespace Zelda {
    public static class BagDomain {

        public static void Toggle(GameContext ctx, BagComponent bag) {
            var ui = ctx.ui;
            if(ui.Bag_IsOpen()) {
                ui.Bag_Close();
            } else {
                Open(ctx, bag);
            }
         
        }
        public static void Open(GameContext ctx, BagComponent bag) {

            var ui = ctx.ui;
            // 空格子
            ui.Bag_Open(bag.GetMaxSlot());
            // 每个格子上的物品
            bag.Foreach(item => {
                ui.Bag_Add(item.id, item.icon, item.count);
            });

            ui.Bag_onUseHandle = (id) => {
                Debug.Log("使用物品:" + id);
            };
        }

        public static void Update(GameContext ctx, BagComponent bag) {

            var ui = ctx.ui;
            if(ui.Bag_IsOpen()){
                ui.Bag_Close();
                Open(ctx, bag);
            }

        }

    }
}