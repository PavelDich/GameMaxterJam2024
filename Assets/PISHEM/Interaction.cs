using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
        public Text indicator;
        public GameObject panel1;
    void Update()
        {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            if (hit.collider.tag == "Item")
            {

                indicator.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Item>().Interaction();
                    
                }

            }
            else
            {
                indicator.enabled = false;
                //GetComponent<Item>().ClosePismo();
                
            }
        }
        else
        {
            indicator.enabled = false;
            panel1.SetActive(false);
        }

    }
}