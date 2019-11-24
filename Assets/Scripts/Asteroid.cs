using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    float size = 1;
    float currentScale = 0;

    void Start() {
        transform.localScale = Vector3.zero;
        size = Random.Range(.3f, 5);
    }
    void Update() {
        currentScale = Mathf.Lerp(currentScale, size, Time.deltaTime * 5);
        transform.localScale = Vector3.one * currentScale;
    }
}
