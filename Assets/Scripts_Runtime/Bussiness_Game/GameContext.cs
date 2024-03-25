namespace Zelda{
    public class GameContext{
        public ModuleAssets assets;

        public RoleRepository roleRepository;

        public GameContext(){
            roleRepository = new RoleRepository();
        }

        public void Inject(ModuleAssets assets){
            this.assets = assets;
        }
    }
}