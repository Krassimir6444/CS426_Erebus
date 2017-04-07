//source: https://unity3d.com/learn/tutorials/topics/animation/animate-anything-mecanim?playlist=17099

using UnityEngine;
using System.Collections;

public class DoorActivator : MonoBehaviour
{

    public Animator[] lights;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", true);
            foreach (var light in lights)
            {
                light.SetTrigger("Activate");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", false);
        }
    }
}