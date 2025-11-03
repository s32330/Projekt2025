using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DoorOpen : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("IsOpen", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("IsOpen", false);
    }
}