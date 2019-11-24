using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfGoodies : MonoBehaviour
{

    public int numOfThings = 100;

    public float distance = 50;
    public float heightVariance = 10;

    public Transform prefabAsteroid;

    List<Transform> things = new List<Transform>();
    PlayerMovement mover;

    void Start()
    {
        mover = GetComponent<PlayerMovement>();

        while (things.Count < numOfThings) SpawnThing(true);

    }


    void Update()
    {

        float limit = distance * distance;

        for (int i = things.Count - 1; i >= 0; i--) {
            Transform thing = things[i];
            float sqrDis = (thing.transform.position - transform.position).sqrMagnitude;
            if (sqrDis > limit) {
                things.RemoveAt(i);
                Destroy(thing.gameObject);
            }
        }

        if (things.Count < numOfThings) SpawnThing();
    }

    private void SpawnThing(bool inVolume = false) {

        Transform prefab = prefabAsteroid;


        Vector3 pos = transform.position;
        if (inVolume) {
            float dis = Random.Range(distance/2, distance);
            Vector3 dir = Random.insideUnitSphere;
            pos += dir * dis;

        } else {
            Vector3 dir = ((mover.velocity * mover.transform.forward).normalized + Random.onUnitSphere).normalized;
            pos += dir * distance;
        }

        Transform thing = Instantiate(prefab, pos, Quaternion.FromToRotation(Vector3.up, Random.onUnitSphere));

        things.Add(thing);
    }
}
