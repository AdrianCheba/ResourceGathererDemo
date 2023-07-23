using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatingSpeed = 5f;
    public float timeToDestroy = 1.2f;
    float startTime;

    [SerializeField]
    TMPro.TMP_Text tmpText;

    void Start()
    {
        startTime = Time.time;
    }

    public void SetText(string text)
    {
        tmpText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + floatingSpeed * Time.deltaTime, transform.position.z);

        if (startTime + timeToDestroy < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
