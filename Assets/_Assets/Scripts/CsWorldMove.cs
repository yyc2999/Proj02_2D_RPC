using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsWorldMove : MonoBehaviour
{
    public string moveSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(string.IsNullOrEmpty(moveSceneName) == false)
            SceneManager.LoadScene(moveSceneName);
    }
}
