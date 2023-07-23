using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    public int inputAmountRequired = 2;
    public GameResourceSO inputResourceSO;
    public GameResourceSO outputResourceSO;

    public float timeToExtract = 5f;

    float timeProgress = 0f;
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
            Product();
            timeProgress = 0f;
        }
    }

    private void Product()
    {
        if (resourcesList.TryUse(inputResourceSO, inputAmountRequired))
        {
            resourcesList.Add(outputResourceSO, 1);

            var floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            floatingText.SetText($"{inputResourceSO.resourceName} -{inputAmountRequired}\n{outputResourceSO.resourceName}+1");
        }
    }
}
