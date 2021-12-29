using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    [SerializeField] GameObject Fade_Object;

    [SerializeField] Material Obj_Material;

    [SerializeField] Color NewColor;

    bool BeginFade;
    public float fade;

    private void Awake()
    {     
        Obj_Material = Fade_Object.GetComponent<Renderer>().material;
        fade = 100f;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            BeginFade = true;
            Debug.Log("destroyed");
        }
    }

    private void Update()
    {
        NewColor = Obj_Material.color;

        if (BeginFade)
        {
            StartCoroutine(delay());
        }
    }

    private IEnumerator delay()
    {
        Obj_Material.color = new Color (Obj_Material.color.r, Obj_Material.color.g, Obj_Material.color.b, fade / 100);
        yield return new WaitForSeconds(0.1f);

        if (fade > 0)
            fade -= 1.8f;

        if (fade <= 0)
        {
            fade = 0f;
            BeginFade = false;
            //this.enabled = false;
        }
    }
}
