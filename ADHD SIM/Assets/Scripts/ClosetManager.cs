using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetManager : MonoBehaviour
{

    public GameObject[] shirts;
    private int shirtsIndex = -1;
    // Start is called before the first frame update

    private void OnEnable()
    {
        shirtsIndex = -1;
        foreach(GameObject go in shirts)
            go.SetActive(false);
    }
    public void PlaceShirt()
    {
        shirtsIndex++;
        shirts[shirtsIndex].gameObject.SetActive(true);
    }
}
