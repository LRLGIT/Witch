using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleUIManager : Singleton<SimpleUIManager>
{
    [SerializeField]private GameObject hint;


    public void Hint(string text)
    {
        var h = Instantiate(hint,transform);
        

        h.GetComponentInChildren<Text>().text = text;

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
