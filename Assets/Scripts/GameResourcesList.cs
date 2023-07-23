using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesList : MonoBehaviour
{
    [HideInInspector]
    public List<GameResource> resources;
    public List<GameResourceSO> resourceSOs;

    List<GameResourceView> resourceViews;
    [SerializeField]
    GameResourceView resourceViewPrefab;

    [SerializeField]
    Transform resourceViewsParent;

    // Start is called before the first frame update
    void Start()
    {
        resources = new List<GameResource>();
        resourceViews = new List<GameResourceView>();

        foreach (var resourceSO in resourceSOs)
        {
            CreateResource(resourceSO);
        }
    }

    public void Add(GameResourceSO resourceSO, int amount)
    {
        var resource = resources.Find((x) => x.resourceSO == resourceSO);

        if (resource == null)
        {
            CreateResource(resourceSO);
        }

        var resourceView = resourceViews.Find((x) => x.resourceSO == resourceSO);

        resource.amount += amount;
        
        resourceView.UpdateAmount(resource.amount);
    }

    public bool TryUse(GameResourceSO resourceSO, int amount)
    {
        var resource = resources.Find((x) => x.resourceSO == resourceSO);

        if (resource == null)
        {
            CreateResource(resourceSO);
        }

        var resourceView = resourceViews.Find((x) => x.resourceSO == resourceSO);

        if (amount > resource.amount)
        {
            return false;
        }

        resource.amount -= amount;
        resourceView.UpdateAmount(resource.amount);

        return true;
    }

    private void CreateResource(GameResourceSO resourceSO)
    {
        var resource = new GameResource(resourceSO);
        resources.Add(resource);

        GameResourceView resourceView = Instantiate<GameResourceView>(resourceViewPrefab, resourceViewsParent);
        resourceView.resourceSO = resourceSO;
        resourceView.UpdateResourceName(resourceSO.resourceName);
        resourceViews.Add(resourceView);
    }
}
