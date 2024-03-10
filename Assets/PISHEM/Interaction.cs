using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
        public Text indicator;
    
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
                
                
            }
        }
        else
        {
            indicator.enabled = false;
        }
        
    }
}