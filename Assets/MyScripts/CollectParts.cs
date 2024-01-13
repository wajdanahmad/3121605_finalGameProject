using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectParts : MonoBehaviour
{
    public int Collectables;
    public GameObject finishPoint;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectables"))
        {
            other.gameObject.tag = "Untagged";
            Collectables++;
            Destroy(other.gameObject);
        } if (other.gameObject.CompareTag("Finish"))
        {
            GameController.changeGameState(GameState.Complete);
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Collectables.ToString();
        if (Collectables >= 4)
        {
            finishPoint.SetActive(true);
        }
    }
}
