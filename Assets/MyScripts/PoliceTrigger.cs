using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = GameDevUtils.CharacterController2.CharacterController;

public class PoliceTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    GameObject temp;
    public FieldOfView12 fov;
  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
           temp= other.gameObject;
         //  fov.canSeePlayer = false;
            Destroy(gameObject.GetComponent<NavMeshAgent>());
            gameObject.GetComponent<Animator>().SetTrigger("Hit");
            other.gameObject.transform.GetComponentInParent<CharacterController>().character.CanControl = false;
           
            StartCoroutine("Anim");
            StartCoroutine("Fail");



        }
        
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(0.3f);
        temp.gameObject.GetComponent<Animator>().SetTrigger("Knock");
    }
    IEnumerator Fail()
    {
        yield return new WaitForSeconds(2f);
        // levelManager.LevelFail();
        StopGame();
    }
    IEnumerator off()
    {
        yield return new WaitForSeconds(2f);
        // levelManager.LevelFail();
        gameObject.SetActive(false);
    }


    void StopGame()
    {
        GameController.changeGameState(GameState.Fail);
    }
}
