using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    public int Count;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectables"))
        {
            Destroy(other.gameObject);
            Count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (text)
        {
            text.text = Count.ToString();
        }

        if (Count == 3)
        {
            GameController.changeGameState(GameState.Complete);
        }
    }
}
