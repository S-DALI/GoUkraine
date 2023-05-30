using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectsManager :  Singleton<PoolObjectsManager>
{
    class Pool_obj
    {
        private List<GameObject> InactiveObjects = new List<GameObject>();
        private GameObject prefab;
        public Pool_obj(GameObject prefab) { this.prefab = prefab; }
        
        public GameObject Spawn(Vector3 pos,Quaternion rot)
        {
            GameObject obj;
            if(InactiveObjects.Count == 0 )
            {
                obj = Instantiate(prefab, pos, rot);
                obj.name= prefab.name;
                obj.transform.SetParent(Instance.transform);
            }
            else
            {
                obj = InactiveObjects[InactiveObjects.Count - 1];
                InactiveObjects.RemoveAt(InactiveObjects.Count - 1);
            }
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);
            return obj;
        }

        public void Despawn(GameObject obj) 
        {
         obj.SetActive(false);
            InactiveObjects.Add(obj);
        }
    
    }

    private Dictionary<string, Pool_obj> Objects_pool = new Dictionary<string, Pool_obj>();
    
    void Initial (GameObject prefab)
    {
        if(prefab != null && Objects_pool.ContainsKey(prefab.name) == false) 
        {
            Objects_pool[prefab.name] = new Pool_obj(prefab);
        }
    }


    public void Preload(GameObject prefab, int num)
    {
        Initial(prefab);
        GameObject[] mass_objects = new GameObject[num];
        for(int i = 0; i < num; i++)
        {
            mass_objects[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
        }
        for (int i = 0; i<num; i++)
        {
            Despawn(mass_objects[i]);
        }
    }
    public GameObject Spawn(GameObject prefab,Vector3 pos,Quaternion rot)
    {
        Initial(prefab);
        return Objects_pool[prefab.name].Spawn(pos, rot);
    }
    public void Despawn(GameObject obj)
    {
        if (Objects_pool.ContainsKey(obj.name))
        {
            Objects_pool[obj.name].Despawn(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}
