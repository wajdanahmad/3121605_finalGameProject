using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier1 : MonoBehaviour
{
    public LevelManager levelManager;
    bool Pass1;
    // Start is called before the first frame update
    void Start()
    {
        Pass1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Car"))
        {
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(100, gameObject.transform.position, 2, 7);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
         
            
        }
    }
   
}
