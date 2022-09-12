using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPosition : MonoBehaviour
{
    Transform playerTransform;
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 0.5f);
    }
}
