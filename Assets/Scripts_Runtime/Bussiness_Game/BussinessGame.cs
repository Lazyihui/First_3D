namespace Zelda{
    public static class BussinessGame{
        public static void Enter(GameContext ctx){
            RoleDomain.Spawn(ctx,0);
        }
    }
}