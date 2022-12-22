using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] GameObject settlerPrefab;
    [SerializeField] float spawnDelay;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float distanceBetweenSettlers = 2f;

    Queue<Settler> pooledSettlers = new Queue<Settler>();
    Queue<Settler> waitingSettlers = new Queue<Settler>();
    float timer;

    private void Start()
    {
        for (int i = 0; i < ColonyManagement.Instance.MaxSettlers; i++)
        {
            GameObject obj = Instantiate(settlerPrefab, transform.position, Quaternion.identity);
            pooledSettlers.Enqueue(obj.GetComponent<Settler>());
            obj.SetActive(false);
        }
    }

    Vector3 SpawnPosition()
    {
        Vector3 finalPos = spawnPoint.position;
        finalPos.x -= waitingSettlers.Count * distanceBetweenSettlers;
        return finalPos;
    }

    public void Spawn()
    {
        if (pooledSettlers.Count <= 0)
            return;

        Settler newSettler = pooledSettlers.Dequeue();
        newSettler.gameObject.SetActive(true);
        newSettler.SetDestination(SpawnPosition());
        waitingSettlers.Enqueue(newSettler);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnDelay)
        {
            timer = 0;
            Spawn();
        }
    }
}
