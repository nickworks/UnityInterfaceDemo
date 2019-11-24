using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;
    public float easeMultiplier = 1;

    PlayerMovement player;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        EaseTowardsTarget();
        if (player && player.isBoosting) Shake();
    }

    private void Shake() {
        transform.position += Random.insideUnitSphere * .1f;
    }

    private void EaseTowardsTarget() {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * easeMultiplier);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * easeMultiplier);
    }
}
