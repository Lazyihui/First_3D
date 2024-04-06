using System;
using System.Collections.Generic;

namespace Zelda {
    public class RoleRepository {
        Dictionary<int, RoleEntity> all;

        public RoleRepository() {
            all = new Dictionary<int, RoleEntity>();
        }

        public void Add(RoleEntity role) {
            all.Add(role.id, role);
            
        }

        public void Remove(RoleEntity role) {
            all.Remove(role.id);
        }
        public bool TryGet(int id, out RoleEntity role) {
            return all.TryGetValue(id, out role);
        }
        //不懂这个方法的作用
        public void Foreach(Action<RoleEntity> action) {
            foreach (var role in all.Values) {
                action(role);
            }
        }

    }
}