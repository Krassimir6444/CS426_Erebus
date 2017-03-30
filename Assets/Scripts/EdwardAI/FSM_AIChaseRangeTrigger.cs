using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AIChaseRangeTrigger : MonoBehaviour
{
    public FSM_AIController AIController;

    private FSM_AIController.AI_States previousAI_State;

    void Start()
    {
        //keep empty
    }

    void Update()
    {
        //keep empty
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            /*if (AIController.AI_State != FSM_AIController.AI_States.Chase) {
                previousAI_State = AIController.AI_State;
            }*/
            AIController.AI_State = FSM_AIController.AI_States.Chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //AIController.AI_State = previousAI_State;
            AIController.AI_State = FSM_AIController.AI_States.Patrol;
        }
    }
}