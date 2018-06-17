using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galaxyImageFacer : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<Collider> colliders;

    private int radius = 500;

    // Use this for initialization
    void Start () {
        InvokeRepeating("PopulateColliders", 0, 1.0f);
        InvokeRepeating("PopulateColliders", 0, 1.0f);
    }

    void PopulateColliders()
    {
        colliders.Clear();
        colliders.AddRange(Physics.OverlapSphere(transform.position, radius));
        colliders.Remove(GetComponent<Collider>());
    }

    void face()
    {
        for (int i = 0; i < colliders.Count; ++i)
        {
            Collider currentCollider = colliders[i];

            currentCollider.transform.LookAt(player.transform);
        }
    }
}
