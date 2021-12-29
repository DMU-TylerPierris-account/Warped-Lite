using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTargetSystem : MonoBehaviour
{
    public Vector3 ShootAt;

    [SerializeField] float Accuracy;

    public GameObject bullet; // prefabs to spawn in
    [SerializeField] float interval, startInterval; // timer for set intervals



    private void Update()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        //ShootAt = new Vector3(GameObject.Find("player").transform.position.x, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);

        ShootAt = GameObject.Find("player").transform.position;
        yield return new WaitForSecondsRealtime(Accuracy);

        if (!FindObjectOfType<DisableGun>().DisableFiring)
        {
            if (interval <= 0) // when the interval allows another bullet to spawn
            {
                Instantiate(bullet, transform.position, Quaternion.identity); //spawns a clone bullet, at a specific location, with identical rotation
                interval = startInterval; // sets the interval timer
            }
            else
            {
                interval -= Time.deltaTime; //counts down the interval
            }
        }
    }
}
