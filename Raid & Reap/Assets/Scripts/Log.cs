using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public float moveDistance = 0.2f; 
    public float moveSpeed = 1f;

    private Vector3 startPosition;
    private float time;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime * moveSpeed;
        float offset = Mathf.Sin(time) * moveDistance;
        transform.position = startPosition + new Vector3(0f, offset, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}