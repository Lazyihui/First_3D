using UnityEngine;

namespace Zelda{

    public class LootEntity : MonoBehaviour{

        public int id ;
        public int itemTypeDI;

        public int itemCount ;

        public void Setpos(Vector3 pos){
            transform.position = pos;
        }
    }
}