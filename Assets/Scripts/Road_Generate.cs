using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Generate : Singleton<Road_Generate>
{
    [SerializeField] private GameObject[] RoadPrefabs;
    private float SpawnPlatformPos = 0;
    [SerializeField] private float RoadLenght;
    [SerializeField] private Transform Player;
    private int StartRoad = 3;
    private List<GameObject> RoadList = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < StartRoad; i++)
        {
            RoadSpawn(0);
        }
    }

    void Update()
    {
        if (Player.position.z - (RoadLenght / 2 + RoadLenght / 5) > SpawnPlatformPos - (StartRoad * RoadLenght))
        {
            RoadSpawn(Random.Range(0, RoadPrefabs.Length));
            DeleteRoad();
        }

    }

    private void RoadSpawn(int RoadIndex)
    {
        GameObject NowCreatedRoad = PoolObjectsManager.Instance.Spawn(RoadPrefabs[RoadIndex], transform.forward * SpawnPlatformPos, transform.rotation);
        RoadList.Add(NowCreatedRoad);
        SpawnPlatformPos += RoadLenght;
    }
    private void DeleteRoad()
    {
        PoolObjectsManager.Instance.Despawn(RoadList[0]);
        RoadList.RemoveAt(0);
    }
}
