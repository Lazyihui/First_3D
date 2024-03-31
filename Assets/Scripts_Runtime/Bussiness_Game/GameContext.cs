namespace Zelda {
    public class GameContext {
        public int onwerRoleID;

        public ModuleAssets assets;
        public ModuleInput input;
        public ModuleCamera moduleCamera;
        public RoleRepository roleRepository;

        public GameContext() {
            roleRepository = new RoleRepository();
        }

        public void Inject(ModuleAssets assets, ModuleInput input, ModuleCamera moduleCamera) {
            this.assets = assets;
            this.input = input;
            this.moduleCamera = moduleCamera;
        }
    }
}