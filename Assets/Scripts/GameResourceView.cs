using UnityEngine;

public class GameResourceView : MonoBehaviour
{
    public TMPro.TMP_Text tmpResourceName;
    public TMPro.TMP_Text tmpAmount;

    public GameResourceSO resourceSO;

    public void UpdateResourceName(string resourceName)
    {
        tmpResourceName.text = resourceName;
    }

    public void UpdateAmount(int amount)
    {
        tmpAmount.text = amount.ToString();
    }
}