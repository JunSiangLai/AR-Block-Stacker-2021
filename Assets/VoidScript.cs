using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidScript : MonoBehaviour
{
    public TrickyTowers tricky;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        tricky.UpdateScore();
    }
}
