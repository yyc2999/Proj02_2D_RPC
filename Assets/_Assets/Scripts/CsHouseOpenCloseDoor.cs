using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsHouseOpenCloseDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        door.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        door.gameObject.SetActive(true);
    }
}
