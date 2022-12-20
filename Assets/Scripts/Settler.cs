using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SettlerJob
{
    Scientist,
    Worker,
    Farmer,
}

[System.Serializable]
public class SettlerData
{
    public string Name;
    public SettlerJob Job;
    public int Health = 50;

    public void Randomize()
    {
        Name = ColonyManagement.Instance.RandomSettlerName();
        Job = GameDevHelper.RandomEnum<SettlerJob>();
    }
}

public class Settler : Entity
{
    public SettlerData Data;
    [SerializeField] bool selected;
    [SerializeField] LayerMask roomLayer;
    [SerializeField] NavMeshAgent agent;
    ColonyRoom currentRoom;
    Animator anim;

    public bool Selected 
    { 
        get => selected; 
        set
        {

            selected = value;
        }
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Data.Randomize();
    }

    public void Move(Vector3 newPosition)
    {
        agent.SetDestination(newPosition);
    }

    private void ManageRotation()
    {
        Vector3 newRotation = new Vector3();
        if (agent.destination.x < transform.position.x)
            newRotation.y = 0;
        else
            newRotation.y = -180;

        transform.rotation = Quaternion.Euler(newRotation);
    }

    private void Animation()
    {
        float velocity = Mathf.Clamp01(agent.velocity.sqrMagnitude);
        anim.SetFloat("velocity", velocity);
    }

    public override void Death()
    {
        anim.SetTrigger("Death");
        Destroy(this);
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        if (Selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, roomLayer);
                if (hit)
                {
                    ColonyRoom room = hitInfo.transform.GetComponent<ColonyRoom>();
                    if (room)
                    {
                        if (currentRoom)
                        {
                            if (currentRoom == room)
                                return;

                            currentRoom.RemoveSettler(this);
                            currentRoom = null;
                        }

                        if (room.AddSettler(this, out Vector3 newPosition))
                        {
                            Move(newPosition);
                            currentRoom = room;
                        }
                    }
                }
            }
        }

        ManageRotation();
        Animation();
    }
}
