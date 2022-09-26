using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePooling : MonoBehaviour
{
    private Queue<GameObject> pooledObjects;
    public GameObject objectPrefab;
    [SerializeField] private int poolSize;

    public float timer = 10f;

    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pooledObjects.Enqueue(obj);
        }
    }

    private void Start()
    {
        StartCoroutine(spawn());
    }


    private IEnumerator spawn()
    {
        while(true)
        {
            var obj = GetPoolObject();
            obj.transform.position = new Vector3(70, 26, -105);
            yield return new WaitForSeconds(timer);
        }
    }

    public GameObject GetPoolObject()
    {
        GameObject obj = pooledObjects.Dequeue();
        obj.SetActive(true);
        pooledObjects.Enqueue(obj);
        return obj;
    }

}
