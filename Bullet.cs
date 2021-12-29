using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector3 target; // where to shoot

    [SerializeField] float Velocity, explosedCount;

    float timer;
    private void Start()
    {
        target = FindObjectOfType<BulletTargetSystem>().ShootAt; // aim at player position
        timer = 0;
    }

    void Update()
    {
        if (FindObjectOfType<Game_Rules>().Game_On)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Velocity * Time.deltaTime); // move bullet towards target per second
            transform.LookAt(target);

            if (timer <= explosedCount)
                timer += 1 * Time.deltaTime;
            else
                Invoke("DestroyBullet", 0.2f);
        }
    }

    void OnTriggerEnter(Collider other) // checks for a collision
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstical") // if that collision is with the player
        {
            Hit();
        }
    }

    void Hit()
    {
       FindObjectOfType<Game_Rules>().Game_On = false;
       StartCoroutine(FindObjectOfType<Game_Rules>().RestartDelay());
       Invoke("DestroyBullet", 0.2f);
        //Debug.Log("Hit");
    }

    void DestroyBullet()
    {
        Destroy(gameObject); //destroys the prefab 
    }
}
