using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour
{
    public GameObject[] inimigosPrefab;
    private int chance;
    public float tempoSpawn;
    public Transform limiteEsquerdo;
    public Transform limiteDireito;

    private float minY;
    private float maxY;

    private float tempTime;

    // Start is called before the first frame update
    void Start()
    {
        minY = limiteEsquerdo.position.y;
        maxY = limiteDireito.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
        if(tempTime >= tempoSpawn)
        {
            tempTime = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        chance = Random.Range(0, inimigosPrefab.Length);
        GameObject tempPrefab = Instantiate(inimigosPrefab[chance]) as GameObject;
        float posY = Random.Range(minY, maxY);
        tempPrefab.transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
