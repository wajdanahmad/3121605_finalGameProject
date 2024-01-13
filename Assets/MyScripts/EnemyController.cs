using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private FieldOfView12 fov;
    public GameObject pos1;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(pos1.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //fov.SetOrigin(transform.GetChild(0).transform.position);
        //fov.SetAimDirection(transform.up);
        

    }
}
