using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Rules : MonoBehaviour
{
    public float RototationOfObjects, SpeedOfTreadmill, Points, PointsPerSec, Distance, ChanceObject, CoinTotal;
    public Vector3 TreadmillDirection, TreadmillRotation;

    GameObject PlayerObject;

    public bool Game_On, AllowSpawn,AllowProgress,AllowCoinInObjects;
    Transform player;

    public Text PointText;
    public Text DistanceText;

    public GameObject RestartMenu;

    [SerializeField] Camera cam;
    CameraFollow CamFollow;

    public AudioClip NewTrack;
   
    public float count, ChancePerSec, TimeDelay;

    public Color Current_level_color;
    public float H, S, V;

    int rand;

    PlayerInfo info;
    float Rate = 0;

    public bool Respawning;
    

    CoinSpawnLocation C_Spawn;
    private void Awake()
    {
        Points = 0f;
        Distance = 0f;
        Game_On = true;
        AllowSpawn = true;
        AllowProgress = true;
        PointsPerSec = 1f;
        TimeDelay = 1.5f;

        ChanceObject = 1f;
        ChancePerSec = 0.04f;


        CamFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        player = GameObject.Find("player").GetComponent<Transform>();
        C_Spawn = GameObject.Find("Collectable").GetComponent<CoinSpawnLocation>();
        PlayerObject = GameObject.Find("player");

        info = FindObjectOfType<PlayerInfo>();

        Color.RGBToHSV(Current_level_color, out H, out S, out V);

        H = Random.Range(0f, 0.8f);

        GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("Main Theme");

         rand = Random.Range(0, 6);
        if (info.Music)
        {
            if (rand >= 0 && rand < 2)
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Level 1"); //Debug.Log("song1");
            if (rand >= 2 && rand < 4)
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Level 2"); //Debug.Log("song2");
            if (rand >= 4 && rand < 6)
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Level 3"); //Debug.Log("song3");
        }

        

        if (!info.PlayedBefore)
        {
            StartCoroutine(Tutorial());
        }

        
    }

    private void FixedUpdate()
    {
        
        switch (Game_On)
        {
            case true:
                Points += PointsPerSec * Time.deltaTime;
                distanceChange();

                ChanceObject -= ChancePerSec * Time.deltaTime; // chance of an object spawning

                
                PointText.text = "Points: " + Points.ToString("N0");

                DistanceText.text = "Distance: " + Distance.ToString("N0") + "m";

                RestartMenu.SetActive(false);

                progress();

                Invoke("CamReact", 1f);

                ColorCycle();

                //Debug.Log(TreadmillDirection);
                break;
                
            case false:
                SpeedOfTreadmill = 0f;

               
                //if(a)
                    //StartCoroutine(RestartDelay());

                break;
        
        
        }

        if (info.BestDistance < Distance)
            info.BestDistance = Distance;

        if (info.HighScore < Points)
            info.HighScore = Points;
    }

    void ColorCycle()
    {
        H += 0.005f * Time.deltaTime;
        Current_level_color = Color.HSVToRGB(H, S, V);

        if (H >= 1f)
            H = 0f;        
    }

    void distanceChange()
    {
        if (TreadmillDirection.x != 0)
            Distance += (player.transform.position.x * Time.deltaTime) / 10;



        if (TreadmillDirection.y != 0)
            Distance += (player.transform.position.y * Time.deltaTime) / 10;


        if (TreadmillDirection.z != 0)
            Distance += (player.transform.position.z * Time.deltaTime) / 10;

    }

    void progress()
    {
         if (RototationOfObjects < 200)
               RototationOfObjects += 0.5f * Time.deltaTime;

         if (SpeedOfTreadmill < 15)
               SpeedOfTreadmill += 0.6f * Time.deltaTime;

         if (cam.fieldOfView < 40)
               cam.fieldOfView += 0.1f * Time.deltaTime;

         PointsPerSec += 0.1f * Time.deltaTime;

        if (AllowProgress)
            StartCoroutine(Delay());

        if (Distance > 10000f)
            AllowCoinInObjects = true;
    }

    void CamReact()
    {
        if (Rate < 1)
        {  
            cam.fieldOfView = Mathf.Lerp(FindObjectOfType<ScreenAdjustment>().FOV, 30 + info.FOV, Rate);
            Rate += 0.5f * Time.deltaTime;
        }
        

        count += 1f * Time.deltaTime;

        if (count > 5)
        {
            count = 0;
            //if (CamFollow.smoothSpeed != 0.2f)
            //CamFollow.smoothSpeed -= 0.1f;

            if(TimeDelay != 0.5f)
                TimeDelay -= 0.1f;
        }
    }

    public IEnumerator PreventMultiSpawn()
    {
        AllowSpawn = false;

        yield return new WaitForSeconds(TimeDelay /2);

       
        CoinSpawn();
        if (TimeDelay >= 0.9f)
        {
           Invoke("CoinSpawn", TimeDelay / 2);

           if (TimeDelay >= 1.1f)
           {
               Invoke("CoinSpawn", TimeDelay);

               if (TimeDelay >= 1.3f)
                    Invoke("CoinSpawn", TimeDelay * 1.5f);
               
           }
 
            //Debug.Log("SpawnCoin");
        }
        yield return new WaitForSeconds(TimeDelay / 2);

        yield return new WaitForSeconds(TimeDelay);
        AllowSpawn = true;

    }
    private void CoinSpawn()
    {
        C_Spawn.SpawnObject(); // Spawns in the coin
    }

    private IEnumerator Delay()
    {

        AllowProgress = false;
        yield return new WaitForSeconds(3f);

        //Debug.Log("Difficulty Increase");

        //if (TimeDelay > 0.5f)
            //TimeDelay -= 1f * Time.deltaTime;

        if (ChancePerSec < 0.3f)
            ChancePerSec += (1.5f * Time.deltaTime) / 10;

        AllowProgress = true;
    }

    public IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(0.15f);
        PlayerObject.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        RestartMenu.SetActive(true);
       

    }

    public void respawn()
    {
        Respawning = true;

        if (TreadmillDirection.x != 0)
        {
            player.transform.position = new Vector3(player.transform.position.x + 30, player.transform.position.y, player.transform.position.z);
            StartCoroutine(respawndelay());
        }


        if (TreadmillDirection.y != 0)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 30 , player.transform.position.z);
            StartCoroutine(respawndelay());
        }


        if (TreadmillDirection.z != 0)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 30);
            StartCoroutine(respawndelay());
        }
    }

    private IEnumerator respawndelay()
    {
        yield return new WaitForSeconds(1f);
        PlayerObject.SetActive(true);
        Game_On = true;
        SpeedOfTreadmill = 10f;
        Respawning = false;
    }

    private IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(20f);
        info.PlayedBefore = true;
    }
}
