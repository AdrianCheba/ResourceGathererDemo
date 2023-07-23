using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionBuilding : MonoBehaviour
{
    public float timeToExtract = 5f;

    float timeProgress = 0f;
    public GameResourceSO resourceSO;
    public GameResourcesList resourcesList;
    
    [SerializeField]
    FloatingText floatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timeProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeProgress += Time.deltaTime;

        if (timeProgress > timeToExtract)
        {
            Extract();
            timeProgress = 0f;
        }
    }

    private void Extract()
    {
        resourcesList.Add(resourceSO, 1);

        var floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        floatingText.SetText(resourceSO.resourceName + " +1");
    }
}
