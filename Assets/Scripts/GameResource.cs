using UnityEngine;

public class GameResource
{
    public int amount = 0;
    public GameResourceSO resourceSO;

    public GameResource(GameResourceSO resourceSO)
    {
        this.resourceSO = resourceSO;
    }
}
