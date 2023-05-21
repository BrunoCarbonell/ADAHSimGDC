using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public ObjectType type;
    public GameObject puzzleApear;



    public void StartPuzzle()
    {
        puzzleApear.SetActive(true);
        gameObject.SetActive(false);
    }
}
