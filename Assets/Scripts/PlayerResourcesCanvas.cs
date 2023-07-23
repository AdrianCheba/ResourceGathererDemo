using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesCanvas : MonoBehaviour
{
    [SerializeField]
    Canvas playerResourcesCan;

    private void Start()
    {
        playerResourcesCan.enabled = false;
    }

    private void OnMouseEnter()
    {
       playerResourcesCan.enabled = true;
     
    }

    private void OnMouseExit()
    {
        playerResourcesCan.enabled = false;
    }

}
