using UnityEngine;
using System.Collections;

public class SineBehaviour : MonoBehaviour

{
    float counter;
    Vector2 startPosition;
    public float timeChange = 4;
    [SerializeField] float hightChange =0.5f;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        counter = (counter + Time.deltaTime* timeChange * 2) % (Mathf.PI * 2);
        transform.position = new Vector2(transform.position.x, startPosition.y + Mathf.Sin(counter) * hightChange);

    }
}
