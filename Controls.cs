using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] private Vector3 finger_position;
    
    public Joystick joystick;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public float Sensitivity;
    
    public Transform aim;

    Game_Rules GameRules;
    public bool bend;

    [SerializeField] private Color NewColor;

    private void Awake()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.y; //extents = size of height / 2

        GameRules = GameObject.Find("GameController").GetComponent<Game_Rules>();

        
        finger_position = new Vector3(0.0f, 0.0f, 0.0f);

        NewColor = new Color (GameRules.Current_level_color.r + 0.3f, GameRules.Current_level_color.g + 0.3f, GameRules.Current_level_color.b + 0.3f);

    }

    private void FixedUpdate()
    {
        transform.parent.position -= GameRules.TreadmillDirection * (GameRules.SpeedOfTreadmill * Time.deltaTime);

        // Handle screen touches.
        if (GameRules.Game_On && !bend)
        {
            Move();
        }
        
        
        //this.GetComponent<Renderer>().material.color = NewColor;
        ScreenBoundries();
    }

    private void ScreenBoundries()
    {
        if (GameRules.TreadmillDirection.x != 0)
        {
            
        }


        if (GameRules.TreadmillDirection.y != 0)
        {


        }
        if (GameRules.TreadmillDirection.z != 0)
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
            transform.position = viewPos;
        }

      
    }
    
    private void Move()
    {
        
           

       // Position the player.
       if (GameRules.TreadmillDirection.x != 0)
       {
           transform.position = new Vector3(transform.position.x, joystick.Vertical * Sensitivity, joystick.Horizontal * Sensitivity);
       }
                

       if (GameRules.TreadmillDirection.y != 0)
       {
           transform.position = new Vector3(joystick.Horizontal * Sensitivity, transform.position.y, joystick.Vertical * Sensitivity);
       }
                

       if (GameRules.TreadmillDirection.z != 0)
       {
           transform.position = new Vector3(joystick.Horizontal * Sensitivity, joystick.Vertical * Sensitivity, transform.position.z);
       }
                


       ship_rotation();
  
        
    }


    private void ship_rotation()
    {
        
        aim.position = new Vector3(joystick.Horizontal * Sensitivity, joystick.Vertical * Sensitivity, + 10);
        Vector3 target_rotation = aim.transform.position;
        Vector3 current_rotation = Vector3.RotateTowards(transform.forward,target_rotation, Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(current_rotation);
        
    }

       
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
