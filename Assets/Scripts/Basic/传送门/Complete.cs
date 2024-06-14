using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : MonoBehaviour
{
    public GameObject Transmit;
    DialogueGiver DG;

    public int num;
    private void Start()
    {
        DG = gameObject.GetComponent<DialogueGiver>();
    }
    private void Update()
    {
        if(DG.i==num)
        {
            Transmit.SetActive(true);
        }
    }
}
