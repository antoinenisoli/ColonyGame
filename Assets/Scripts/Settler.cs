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
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Material outlineMat;

    Material spriteMaterial;
    SpriteRenderer spriteRenderer;
    ColonyRoom currentRoom;
    Animator anim;

    public bool Selected 
    { 
        get => selected; 
        set
        {
            spriteRenderer.material = value ? outlineMat : spriteMaterial;
            selected = value;
        }
    }

    public ColonyRoom Room { get => currentRoom; set => currentRoom = value; }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteMaterial = spriteRenderer.material;

        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Selected = false;
    }

    private void Start()
    {
        Data.Randomize();
        ColonyManagement.Instance.RegisterNewSettler(this);
    }

    private void OnDestroy()
    {
        ColonyManagement.Instance.RemoveSettler(this);
    }

    public void SetDestination(Vector3 newPosition)
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
        ManageRotation();
        Animation();
    }
}
