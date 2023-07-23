using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Canvas buildingCanvas;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject == gameObject)
            {
                buildingCanvas.gameObject.SetActive(true);
            }
            else
            {
                buildingCanvas.gameObject.SetActive(false);
            }
        }
        else
        {
            buildingCanvas.gameObject.SetActive(false);
        }
    }
}
