using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildingSystem : MonoBehaviour
{
    [SerializeField]
    GameObject gameplayPrefab;
    [SerializeField]
    GameObject placingPrefab;
    [SerializeField]
    LayerMask groundMask;

    GameObject placingVisual;
    Vector3 placingVisualLimboPosition = Vector3.down * 15f;

    [SerializeField]
    Transform buildingsContainer;

    void OnEnable()
    {
        placingVisual = Instantiate(placingPrefab, placingVisualLimboPosition, Quaternion.identity);
    }
    
    void OnDisable()
    {
        Destroy(placingVisual);
        placingVisual = null;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100f, groundMask))
        {
            placingVisual.transform.position = hit.point;
        }
        else
        {
            placingVisual.transform.position = placingVisualLimboPosition;
        }

        if (Input.GetMouseButton(0) && placingVisual.transform.position != placingVisualLimboPosition)
        {
            Instantiate(gameplayPrefab, placingVisual.transform.position, placingVisual.transform.rotation, buildingsContainer);
            enabled = false;
        }

        if (Input.GetMouseButton(1))
        {
            enabled = false;
        }
    }
}
