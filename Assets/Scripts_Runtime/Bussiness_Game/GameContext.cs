namespace Zelda{
    public class GameContext{
        public int onwerRoleID;

        public ModuleAssets assets;
        public ModuleInput input;
        public RoleRepository roleRepository;

        public GameContext(){
            roleRepository = new RoleRepository();
        }

        public void Inject(ModuleAssets assets, ModuleInput input){
            this.assets = assets;
            this.input = input;
        }
    }
}