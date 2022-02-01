using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RagdollOnOff : MonoBehaviour
{
    [SerializeField]
    private BoxCollider mainCollider;
    [SerializeField]
    private GameObject playerRig;
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private CinemachineFreeLook camFreeLook;
    [SerializeField]
    private Transform newLookPoint;

    [SerializeField]
    private Moving_Kyle moving_Kyle;

    void Start()
    {
        getRagdollBits();
        RagdollModeOff();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKey(KeyCode.X))
        {
            RagdollModeOn();
            lockOnPlayer();
        }
    }

    private void OnCollisionEnter(Collision activator) {

        if(activator.gameObject.tag == "Activator"){
            RagdollModeOn();
            lockOnPlayer();
        }
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "LAZORS"){
            RagdollModeOn();
        }
        lockOnPlayer();
        
    }

    Collider [] ragdollColliders;
    Rigidbody [] limbsRigidbody;
    void getRagdollBits(){
        ragdollColliders = playerRig.GetComponentsInChildren<Collider>();
        limbsRigidbody = playerRig.GetComponentsInChildren<Rigidbody>();
    }

    void RagdollModeOn(){
        Debug.Log("DEAD");

        playerAnimator.enabled = false;

        foreach(Collider col in ragdollColliders){
            col.enabled = true;
        }

        foreach(Rigidbody rigid in limbsRigidbody){
            rigid.isKinematic = false;
        }

        mainCollider.enabled = false;
        mainCollider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void RagdollModeOff(){
        Debug.Log("ALIVE");
        foreach(Collider col in ragdollColliders){
            col.enabled = false;
        }

        foreach(Rigidbody rigid in limbsRigidbody){
            rigid.isKinematic = true;
        }

        playerAnimator.enabled = true;
        mainCollider.enabled = true;
        mainCollider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    void lockOnPlayer(){
        camFreeLook.Follow = newLookPoint;
        camFreeLook.LookAt = newLookPoint;
        moving_Kyle.enabled = false;
    }
}
