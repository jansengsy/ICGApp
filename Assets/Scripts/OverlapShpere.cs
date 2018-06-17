using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlapShpere : MonoBehaviour {

    private float radius = 50;
    private float targetThreshold = 2;

    private static readonly System.Random random = new System.Random();

    [SerializeField]
    private List<Collider> colliders;

    [SerializeField]
    private GameObject galaxyCanvas;

    [SerializeField]
    private GameObject hud;

    [SerializeField]
    private Text morph;

    [SerializeField]
    private Text size;

    [SerializeField]
    private Text age;

    [SerializeField]
    private Text lbt;

    [SerializeField]
    private Text quitMessage;

    [SerializeField]
    private Text moveMessage;

    [SerializeField]
    private Collider target = null; //gameobject?

    [SerializeField]
    public UniverseManager Manager;

    private GameObject c = null;

    private bool targetHUD;

    // public Vector3 dif;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("PopulateColliders", 0, 1.0f);
        InvokeRepeating("UpdateTarget", 0, 1.0f);

        hud.transform.position = transform.position + transform.forward * 600;
    }

    private void Update()
    {
        Fade();
        //c.transform.rotation = Quaternion.LookRotation(c.transform.position - transform.position);
    }

    private void PopulateColliders()
    {
        // Populate colliders array
        colliders.Clear();
        colliders.AddRange(Physics.OverlapSphere(transform.position, radius));
        colliders.Remove(GetComponent<Collider>());
    }

    private void UpdateTarget()
    {
        if (colliders.Count == 0)
        {
            target = null;
            c = null;
        }

        for (int i = 0; i < colliders.Count; ++i)
        {
            Collider currentCollider = colliders[i];

            // Skip over player object
            if(currentCollider.transform.position == transform.position ||
                currentCollider == target)
            {
                continue;
            }
            else
            {
                Vector3 newTargDiff = (currentCollider.transform.position - transform.position).normalized;
                float newTargDirection = Vector3.Dot(newTargDiff, transform.forward);
                float newTargAngle = Mathf.Rad2Deg * Mathf.Acos(newTargDirection);

                if (newTargAngle < targetThreshold)
                {
                    target = currentCollider;
                    targetHUD = false;
                    GalaxuHUD();
                }
            }
        }
    }

    public void GalaxuHUD()
    {
        if (target && !targetHUD)
        {
            if (c != null)
            {
                Destroy(c);
            }

            Universe u = Manager.GetComponent<UniverseManager>().universe;

            for (int i = 0; i < UniverseManager.everyGalaxyCanvas.Length; i++)
            {
                if (u.spawnedGalaxies[i])
                {
                    Collider targetCollider = UniverseManager.everyGalaxyCanvas[i].GetComponent<Collider>();

                    if (targetCollider == target)
                    {
                        float a = u.galaxyData[i].ageInBillions;

                        if(a == 0)
                        {
                            double r = random.NextDouble();
                            a = 1 + ((float)r * (6 - 1));
                            a = (float)System.Math.Round(a, 2);
                        }

                        // Top
                        morph.text = "Morphology: " + u.galaxyData[i].GetMorph;

                        // Middle a
                        age.text = a + " billion years old";

                        // Middle b
                        size.text = u.galaxyData[i].MWfactor + " the mass of our Galaxy";

                        // Bottom
                        lbt.text = System.Math.Round(u.galaxyData[i].lookBackTime, 2) + " billion years back in time";
                    }
                }
            }

            galaxyCanvas.transform.position = target.transform.position + new Vector3(0, -1, 0);
            galaxyCanvas.transform.LookAt(transform);
            galaxyCanvas.transform.rotation = transform.rotation;
            galaxyCanvas.transform.rotation *= Quaternion.Euler(0, 180f, 0);
            c = Instantiate(galaxyCanvas);
            c.transform.localScale = c.transform.localScale / 170;
            c.transform.LookAt(transform);

            targetHUD = true;

            c.transform.rotation = Quaternion.LookRotation(c.transform.position - transform.position);
        }
    }

    private void Fade()
    { 
        quitMessage.CrossFadeAlpha(0.0f, 10f, false);
        moveMessage.CrossFadeAlpha(0.0f, 10f, false);
    }
}

