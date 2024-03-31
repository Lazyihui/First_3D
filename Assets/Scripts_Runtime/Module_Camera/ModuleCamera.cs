using UnityEngine;

namespace Zelda{

    public class ModuleCamera {

        public Camera camera;

        public ModuleCamera(){}

        public void Inject(Camera camera){
            this.camera = camera;
        }

        public void Follow(Vector3 target,float height,float radius){
            camera.transform.position = new Vector3(target.x,target.y + height,target.z - radius);
        }

    }
}