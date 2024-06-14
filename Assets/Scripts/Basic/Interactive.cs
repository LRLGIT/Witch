using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool late;
    [Header("Åö×²")]
    public GameObject Panel1; 
    [Header("ºó³öÏÖ")]
    public GameObject Panel2;

    KeyChange kc;
    bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {
        kc = gameObject.GetComponent<KeyChange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (kc!=null&&kc.allRight)
            {
                Panel1.SetActive(false);
                Panel2.SetActive(true);
            }
            else
            {
                Panel1.SetActive(true);
                if (Panel2 != null)
                {
                    Panel2.SetActive(false);
                }
            }
        }
        if (kc != null)
        {
            if (kc.music.activeSelf == true)
            {
                Panel1.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!late)
        {
            isTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Panel1.SetActive(false);
        if (Panel2 != null)
        {
            Panel2.SetActive(false);
        }
        isTrigger = false;
    }
}
