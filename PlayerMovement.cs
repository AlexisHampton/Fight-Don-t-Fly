using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : Singleton<PlayerMovement>
{
    public NavMeshAgent agent;
    public LayerMask movementMask;
    public Interactable focus;
    public float stoppingDistance = 2.5f;
    public bool canMove;
    Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                MoveToPoint(hit.point);
                if (focus != null) focus = null;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //check if hit interactable,
                //set as focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    focus = interactable;
                    agent.SetDestination(focus.gameObject.transform.position);
                }
            }
        }

        Focus();
    }

    private void Focus() //if item else if 
    {
        if (focus != null)
        {
            if ((transform.position - focus.transform.position).magnitude <= stoppingDistance)
            {
                switch (focus.interactableType)
                {
                    case InteractableType.NPC:
                        NPC npc = focus.GetComponent<NPC>();
                        DialogueManager.Instance.IsOn(npc);
                        break;
                    case InteractableType.ITEM:
                        focus.Dialogue();
                        break;
                }

                focus = null;
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
