using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public bool isholding = false;
    public ObjectType objType;
    public DishType dishType;
    public GameObject sponge;
    public GameObject plate;
    public GameObject glass;
    public GameObject fork;
    public GameObject knife;
    public GameObject spoon;
    public bool dishClean = false;
    public Material dishMaterial;
    private Animator anim;
    private StarterAssetsInputs _input;
    [SerializeField] private GameObject islookingAt = null;
    RaycastHit HitInfo;
    private bool startedClean = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Action()
    {
        if(_input.action && isholding && objType == ObjectType.DISH && !dishClean && !startedClean)
        {
            startedClean = true;
            anim.SetTrigger("Clean");
        }
    }

    private void Interact()
    {
        if(islookingAt != null && _input.interact && !isholding)
        {

            if(!islookingAt.CompareTag("placeble"))
                isholding = true;
            
            if (islookingAt.GetComponent<Dish>() != null)
            {
                objType = ObjectType.DISH;
                islookingAt.SetActive(false);
                dishType = islookingAt.GetComponent<Dish>().type;
                dishClean = false;
                sponge.SetActive(true);
                switch (dishType)
                {
                    case DishType.PLATE:
                        plate.SetActive(true);
                        break;
                    case DishType.GLASS:
                        glass.SetActive(true);
                        break;
                    case DishType.FORK:
                        fork.SetActive(true);
                        break;
                    case DishType.KNIFE:
                        knife.SetActive(true);
                        break;
                    case DishType.SPOON:
                        spoon.SetActive(true);
                        break;
                }
            }
        }
        if(islookingAt != null && _input.interact && isholding)
        {
            if (islookingAt.CompareTag("placeble"))
            {
                if(objType == ObjectType.DISH && dishClean)
                {
                    isholding = false;
                    switch (dishType)
                    {
                        case DishType.PLATE:
                            plate.SetActive(false);
                            break;
                        case DishType.GLASS:
                            glass.SetActive(false);
                            break;
                        case DishType.FORK:
                            fork.SetActive(false);
                            break;
                        case DishType.KNIFE:
                            knife.SetActive(false);
                            break;
                        case DishType.SPOON:
                            spoon.SetActive(false);
                            break;
                    }
                    sponge.SetActive(false);
                    islookingAt.GetComponent<SinkManager>().AddCleanDish(dishType);
                    objType = ObjectType.NONE;
                    dishType = DishType.NONE;
                    anim.SetTrigger("NewPlate");
                }

            }
        }
    }

    private void Update()
    {
        Transform cameraTransform = Camera.main.transform;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 5.0f))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 5.0f, Color.yellow);
            if (islookingAt != null)
                if (islookingAt != HitInfo.collider.gameObject)
                    if (islookingAt.CompareTag("Interactable"))
                        islookingAt.GetComponent<Outline>().enabled = false;


            if (!isholding && HitInfo.collider.CompareTag("Interactable") && islookingAt != HitInfo.collider.gameObject)
            {
                islookingAt = HitInfo.collider.gameObject;
                islookingAt.GetComponent<Outline>().enabled = true;
            }

            if(isholding && HitInfo.collider.CompareTag("placeble") && islookingAt != HitInfo.collider.gameObject)
            {
                islookingAt = HitInfo.collider.gameObject;
            }

            if(!HitInfo.collider.CompareTag("Interactable") && !HitInfo.collider.CompareTag("placeble"))
                islookingAt = null;

        }
        else
        {
            if(islookingAt!= null)
            {
                if (islookingAt.CompareTag("Interactable"))
                    islookingAt.GetComponent<Outline>().enabled = false;
                islookingAt = null;
            }
                
        }

        Interact();
        Action();
    }



    public void FinishClean()
    {
        dishClean = true;
        startedClean = false;
    }
}
