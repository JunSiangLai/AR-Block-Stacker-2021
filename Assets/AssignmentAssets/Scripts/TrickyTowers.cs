using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TrickyTowers : MonoBehaviour
{
    public List<GameObject> BlockPrefabs = new List<GameObject>();
    public GameObject originBlock;
    public GameObject stagePlane;
    public PlayerInput moveInput;
    public PlayerInput rotInput;
    public Text score;

    private GameObject currentBlock;
    private Vector3 spawnPoint;
    private bool gameStart;


    private void Start()
    {
        gameStart = false;
        currentBlock = null;
    }

    public void OriginSpawned(GameObject origin)
    {
        spawnPoint = originBlock.transform.position + new Vector3(0,2,0);
        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
            return;

        if (currentBlock == null)
        {
            currentBlock = GameObject.Instantiate( BlockPrefabs[ Random.Range(0,BlockPrefabs.Count) ], spawnPoint, stagePlane.transform.rotation, stagePlane.transform );
            currentBlock.GetComponent<Blocks>().moveInput = moveInput;
            currentBlock.GetComponent<Blocks>().rotInput = rotInput;
            currentBlock.GetComponent<Blocks>().tricky = this;
        }

        UpdateScore();
    }

    public void SpawnNextBlock()
    {
        currentBlock = null;
    }

    public void VoidTriggered(GameObject obj)
    {
        if (obj == currentBlock)
        {
            currentBlock = null;
            Destroy(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

    public void UpdateScore()
    {
        if (currentBlock == null)
            score.text = (stagePlane.transform.childCount - 2).ToString();
        else
            score.text = (stagePlane.transform.childCount - 3).ToString();
    }
}
