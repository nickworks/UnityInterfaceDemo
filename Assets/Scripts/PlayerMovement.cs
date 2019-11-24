using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float rotateSpeed = 45;
    public float throttleMultiplier = 10;
    public AnimationCurve throttleFalloff;

    public float velocity = 0;
    public float maxVelocity = 100;
    public float minVelocity = -10;

    float boostAmountMax = 5;
    float boostAmountRemaining = 0;
    float boostRechargeRate = 0.5f;
    float boostCooldownMax = 1;
    float boostCooldownRemaining = 0;

    public Transform model;

    public bool isBoosting { get; private set; }

    void Start()
    {
        boostAmountRemaining = boostAmountMax;
    }

    public float GetBoostPercent() {
        return boostAmountRemaining / boostAmountMax;
    }

    void Update() {
        Boost();
        MoveAndTurn();
    }

    private void MoveAndTurn() {
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);

        model.localRotation = Quaternion.Slerp(model.localRotation, Quaternion.Euler(0, 0, -h * 30), Time.deltaTime * 5);

        float v = Input.GetAxis("Vertical");
        float p = 0;
        if(v < 0) {
            if(velocity < 0) {
                p = velocity / minVelocity;
            }
        } else {
            if (velocity > 0) {
                p = velocity / maxVelocity;
            }
        }

        float mult = throttleMultiplier;
        if (isBoosting) {
            v = 1;
            p *= .2f;
            mult *= 4;
        }

        float throttle = throttleFalloff.Evaluate(p);
        velocity += throttle * v * mult * Time.deltaTime;
        transform.position += (velocity * transform.forward) * Time.deltaTime;
        velocity = Mathf.Lerp(velocity, 0, Time.deltaTime * 2);
    }

    private void Boost() {
        bool isTryingToBoost = Input.GetButton("Jump");
        isBoosting = false;
        if (isTryingToBoost) {
            if (boostAmountRemaining > 0) {
                isBoosting = true;
                boostAmountRemaining -= Time.deltaTime;
            }
            boostCooldownRemaining = boostCooldownMax;
        } else {
            if (boostCooldownRemaining > 0) boostCooldownRemaining -= Time.deltaTime;
            if (boostCooldownRemaining <= 0 && boostAmountRemaining < boostAmountMax) {
                boostAmountRemaining += Time.deltaTime * boostRechargeRate;
            }
        }
    }
}
