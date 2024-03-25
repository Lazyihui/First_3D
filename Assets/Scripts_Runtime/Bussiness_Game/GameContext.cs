namespace Zelda{
    public class GameContext{
        public ModuleAssets assets;

        public GameContext(){
        }

        public void Inject(ModuleAssets assets){
            this.assets = assets;
        }
    }
}