using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils {
    public class SimpleObjectPool : MonoBehaviour {
        public GameObject Prefab;

        private readonly Stack<GameObject> inactiveInstances = new Stack<GameObject>();

        public GameObject GetObject() {
            GameObject spawnedGameObject;
            if (inactiveInstances.Count > 0) {
                spawnedGameObject = inactiveInstances.Pop();
            }
            else {
                spawnedGameObject = Instantiate(Prefab);
                var pooledObject = spawnedGameObject.AddComponent<PooledObject>();
                pooledObject.Pool = this;
            }
            spawnedGameObject.transform.SetParent(null);
            spawnedGameObject.SetActive(true);
            return spawnedGameObject;
        }

        public void ReturnObject(GameObject toReturn) {
            var pooledObject = toReturn.GetComponent<PooledObject>();
            if (pooledObject != null && pooledObject.Pool == this) {
                toReturn.transform.SetParent(transform);
                toReturn.SetActive(false);
                inactiveInstances.Push(toReturn);
            }
            else {
                Destroy(toReturn);
            }
        }
    }

    public class PooledObject : MonoBehaviour {
        public SimpleObjectPool Pool;
    }
}