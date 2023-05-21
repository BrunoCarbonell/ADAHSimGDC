using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cleanPlates;
    private int platesCleaned = -1;
    [SerializeField] private GameObject[] cleanGlasses;
    private int glassesCleaned = -1;
    [SerializeField] private GameObject[] cleanForks;
    private int forksCleaned = -1;
    [SerializeField] private GameObject[] cleanKnifes;
    private int knifesCleaned = -1;
    [SerializeField] private GameObject[] cleanSpoons;
    private int spoonsCleaned = -1;

    [SerializeField] private GameObject[] dirtyDishes;

    public GameObject shirt;
    [SerializeField]private int count=0;

    private void Start()
    {
        shirt.SetActive(false);
        count = 0;
        

    }

    private void OnEnable()
    {
        shirt.SetActive(false);
        count = 0;
        platesCleaned = -1;
        glassesCleaned = -1;
        forksCleaned = -1;
        knifesCleaned = -1;
        spoonsCleaned = -1;
        foreach (GameObject go in cleanPlates)
            go.SetActive(false);

        foreach (GameObject go in cleanGlasses)
            go.SetActive(false);
        foreach (GameObject go in cleanForks)
            go.SetActive(false);
        foreach (GameObject go in cleanKnifes)
            go.SetActive(false);
        foreach (GameObject go in cleanForks)
            go.SetActive(false);

        foreach (GameObject go in dirtyDishes)
            go.SetActive(true);
    }
    // Start is called before the first frame update
    public void AddCleanDish(DishType type)
    {

        TrySpawningShirt();
        switch (type)
        {
            case DishType.NONE:
                break;
            case DishType.PLATE:
                if (platesCleaned < cleanPlates.Length)
                {
                    platesCleaned++;
                    cleanPlates[platesCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.GLASS:
                if (glassesCleaned < cleanGlasses.Length)
                {
                    glassesCleaned++;
                    cleanGlasses[glassesCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.FORK:
                if (forksCleaned < cleanForks.Length)
                {
                    forksCleaned++;
                    cleanForks[forksCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.KNIFE:
                if (knifesCleaned < cleanKnifes.Length)
                {
                    knifesCleaned++;
                    cleanKnifes[knifesCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.SPOON:
                if (spoonsCleaned < cleanSpoons.Length)
                {
                    spoonsCleaned++;
                    cleanSpoons[spoonsCleaned].gameObject.SetActive(true);
                }
                break;

        }
    }

    public void TrySpawningShirt()
    {
        count++;
        if (count >= 3)
        {
            if (Random.Range(0, 3) == 1)
            {
                shirt.SetActive(true);
            }

            if(count > 7)
                shirt.SetActive(true);
        }
    }
}
