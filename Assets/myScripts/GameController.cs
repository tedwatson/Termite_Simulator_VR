using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRInteraction;

public class GameController : MonoBehaviour {

    public float growthMultiplier = 1f;
    public float growthSpeed = 1f;
    public ArcTeleporter l_arc;
    public ArcTeleporter r_arc;

    private GameObject cameraRig;
    private float currentSize;
    private AudioSource chompSound;

    public void Grow(Vector3 size)
    {
        chompSound.Play();
        currentSize += processSize(size);
        print("currentSize = " + currentSize);
    }

	// Use this for initialization
	void Start () {
        chompSound = GetComponent<AudioSource>();
        cameraRig = GameObject.Find("[CameraRig]").gameObject;
        currentSize = cameraRig.transform.localScale.x;
        growthSpeed = 10 - growthSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        float actualSize = cameraRig.transform.localScale.x;
        if (actualSize < currentSize)
        {
            float inc = ((currentSize - actualSize) / growthSpeed) * Time.deltaTime;
            cameraRig.transform.localScale += new Vector3(inc, inc, inc);
        }
    }

    float processSize(Vector3 size)
    {
        // Finds the average of the 3 extents and returns a Vector3 with 3 equal extents
        float ave = ((size.x + size.y + size.z) / 3f) * growthMultiplier;
        return ave;
    }
}
