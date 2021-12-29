using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField] private Transform BasicLevelPartEnd;
    [SerializeField] private List<Transform> CoinTypeList;

    Transform CoinLocation;
    Game_Rules GameRules;

    private Vector3 LastEndPosition;

    CoinSpawnLocation C_Spawn;

    private void Awake()
    {
        CoinLocation = GameObject.Find("CoinSpawnLocation").GetComponent<Transform>();
        GameRules = GameObject.Find("GameController").GetComponent<Game_Rules>();
        C_Spawn = this.gameObject.GetComponent<CoinSpawnLocation>();
       

        CoinTypeList.Add(GameObject.Find("Coin").GetComponent<Transform>());

        if (GameRules.Distance > 1000f)
            CoinTypeList.Add(GameObject.Find("Coin + 2").GetComponent<Transform>());

        if (GameRules.Distance > 5000f)
            CoinTypeList.Add(GameObject.Find("Coin + 3").GetComponent<Transform>());

        BasicLevelPartEnd = CoinLocation;
    }


    private void Update()
    {
        BasicLevelPartEnd = CoinLocation;
        LastEndPosition = BasicLevelPartEnd.position;
    }


    public void SpawnObject()
    {
        //Debug.Log("spawn_obstical");
        if(FindObjectOfType<PlayerInfo>().PlayedBefore)
        {
            Transform ChosenObstical = CoinTypeList[Random.Range(0, CoinTypeList.Count)];
            Transform LastLevelPartTransform = SpawnObject(ChosenObstical, LastEndPosition);
            LastEndPosition = LastLevelPartTransform.position;

        }
        
    }

    private Transform SpawnObject(Transform Obstical, Vector3 spawnPosition)
    {
        Transform LevelPartTransform = Instantiate(Obstical, spawnPosition, Quaternion.identity);
        return LevelPartTransform;
    }
}
