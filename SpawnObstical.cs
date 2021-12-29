using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstical : MonoBehaviour
{
    [SerializeField] private Transform BasicLevelPartEnd;
    [SerializeField] private List<Transform> ObsticalList;
    private Vector3 LastEndPosition;

    [SerializeField] Loot_Table1 loottable;

    private void Awake()
    {
        BasicLevelPartEnd = GameObject.Find("ObjectSpawnLocation").GetComponent<Transform>();
        //AddObstical();
    }


    private void Update()
    {
        LastEndPosition = BasicLevelPartEnd.position;
    }


    public void SpawnObject()
    {
        //Debug.Log("spawn_obstical");
        if(FindObjectOfType<PlayerInfo>().PlayedBefore)
        {
            item item = loottable.GetDrop();
            Debug.Log(item.name);
            //Transform ChosenObstical = ObsticalList[Random.Range(0, ObsticalList.Count)];
            Transform ChosenObstical = item.Object.transform;
            Transform LastLevelPartTransform = SpawnObject(ChosenObstical, LastEndPosition);
            LastEndPosition = LastLevelPartTransform.position;
        }
        
    }

    private Transform SpawnObject(Transform Obstical, Vector3 spawnPosition)
    {
        Transform LevelPartTransform = Instantiate(Obstical, spawnPosition, Quaternion.identity);
        return LevelPartTransform;
    }

    void AddObstical()
    {
        ObsticalList.Add(GameObject.Find("obstical1").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical2").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical3").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical4").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical5").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical6").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical7").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical8").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical9").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical10").GetComponent<Transform>());
        ObsticalList.Add(GameObject.Find("obstical11").GetComponent<Transform>());
    }
}
