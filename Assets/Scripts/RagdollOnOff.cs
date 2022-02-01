using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    [SerializeField]
    private BoxCollider mainCollider;
    [SerializeField]
    private GameObject playerRig;

    void Start()
    {
        getRagdollBits();
        RagdollModeOn();
        //RagdollModeOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision activator) {
        if(activator.gameObject.tag == "Activator"){
            RagdollModeOn();
        }
    }

    Collider [] ragdollColliders;
    Rigidbody [] limbsRigidbody;
    void getRagdollBits(){
        ragdollColliders = playerRig.GetComponentsInChildren<Collider>();
        limbsRigidbody = playerRig.GetComponentsInChildren<Rigidbody>();
    }

    void RagdollModeOn(){

        foreach(Collider col in ragdollColliders){
            col.enabled = true;
        }

        foreach(Rigidbody rigid in limbsRigidbody){
            rigid.isKinematic = false;
        }
    
        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void RagdollModeOff(){
        foreach(Collider col in ragdollColliders){
            col.enabled = false;
        }

        foreach(Rigidbody rigid in limbsRigidbody){
            rigid.isKinematic = true;
        }

        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
