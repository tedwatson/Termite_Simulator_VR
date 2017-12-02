using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleCubeController : MonoBehaviour {

    public float upperLimit = 1.5f;

    private GameController gameController;
    private MeshRenderer mr;
    private BoxCollider bc;
    private GameObject cameraRig;

    private ViveGrip_Grabbable grabbableScript;
    private ViveGrip_Highlighter highlighterScript;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        cameraRig = GameObject.Find("[CameraRig]");
        grabbableScript = GetComponent<ViveGrip_Grabbable>();
        highlighterScript = GetComponent<ViveGrip_Highlighter>();
        grabbableScript.enabled = false;
        highlighterScript.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter for " + name + " and " + other.name);
        upperLimit = 2f;
        print("average(bc.size) = " + average(bc.size));
        print("cameraRig.transform.localScale.y = " + cameraRig.transform.localScale.y);
        float sizeRatio = average(bc.size) / cameraRig.transform.localScale.y;
        if (sizeRatio < upperLimit)
        {
            print("sizeRatio (" + sizeRatio + ") < upperLimit (" + upperLimit + ")");
            
            if (other.gameObject.tag.Equals("MainCamera"))
            {
                print("other is MainCamera");
                GetComponent<Rigidbody>().isKinematic = false;
                gameController.Grow(mr.bounds.size);
                gameObject.SetActive(false);
            }
            else if (other.gameObject.tag.Equals("MotionController"))
            {
                print("other is MotionController");
                GetComponent<Rigidbody>().isKinematic = false;
                grabbableScript.enabled = true;
                highlighterScript.enabled = true;
            }

            if (!other.gameObject.tag.Equals("MainCamera"))
            {
                /*
                if (sizeRatio < .25f * upperLimit)
                {
                    // Light
                    print("Object is Light");
                    grabbableScript.anchor.enabled = true;
                    grabbableScript.rotation.mode = ViveGrip_Grabbable.RotationMode.ApplyGrip;
                }
                
                else */ if (sizeRatio < .5f * upperLimit)
                {
                    // Medium
                    print("Object is Medium");
                    grabbableScript.anchor.enabled = false;
                    grabbableScript.rotation.mode = ViveGrip_Grabbable.RotationMode.ApplyGrip;
                }
                else if (sizeRatio < upperLimit)
                {
                    // Heavy
                    print("Object is Heavy");
                    grabbableScript.anchor.enabled = false;
                    grabbableScript.rotation.mode = ViveGrip_Grabbable.RotationMode.Disabled;
                }
            }
        }
        else print("sizeRatio (" + sizeRatio + ") >= upperLimit (" + upperLimit + ")");
    }

    private float average(Vector3 vector3)
    {
        float sum = vector3.x + vector3.y + vector3.z;
        return sum / 3;
    }
    
}
