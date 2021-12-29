using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChosenShip : MonoBehaviour
{
    PlayerInfo info;

    private void Awake()
    {
        info = FindObjectOfType<PlayerInfo>();


    }
}
