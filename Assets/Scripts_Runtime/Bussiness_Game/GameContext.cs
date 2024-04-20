namespace Zelda {
    public class GameContext {
        public int onwerRoleID;

        public ModuleAssets assets;

        public AppUI ui;
        public ModuleInput input;
        public ModuleCamera moduleCamera;
        public RoleRepository roleRepository;
        public LootRepository lootRepository;

        public IDSercices idServics;

        public GameContext() {
            roleRepository = new RoleRepository();
            idServics = new IDSercices();
            lootRepository = new LootRepository();
        }

        public void Inject(AppUI ui,ModuleAssets assets, ModuleInput input, ModuleCamera moduleCamera) {
            this.ui = ui;
            this.assets = assets;
            this.input = input;
            this.moduleCamera = moduleCamera;
        }
    }
}