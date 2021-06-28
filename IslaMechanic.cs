using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslaMechanic : MonoBehaviour
{
    [Tooltip("The array of the instanciated glasses for First Player.")][SerializeField] private GameObject[] firstPlayerGlasses;
    [Tooltip("The array of the instanciated glasses for Second Player.")][SerializeField] private GameObject[] secondPlayerGlasses;
    [Tooltip("This list is updated if someone score and the script find an Isla.")][SerializeField] private List<GameObject> islaGlasses = new List<GameObject>();

    private int islaCount = 0;
    public static bool canShootIsla = false;
    public void OnEnablingIslaMechanicScript()
    {
        //method used on Button which start the game.
        islaGlasses.Clear();
        canShootIsla = false;
        islaCount = 0;
        secondPlayerGlasses = GameObject.FindGameObjectsWithTag("Glasses2");
        firstPlayerGlasses = GameObject.FindGameObjectsWithTag("Glasses");
    }
    private void FixedUpdate()
    {
        if (LiquidDetector.playerScored && !GameManager.GameFinished)
        {
            UpdatingIslas();
        } 
        if (LiquidDetector.secondPlayerScored && !GameManager.GameFinished && UIManager.onV2Game)
        {
            UpdatingIslas();
        }
    }
    private void OnDisable()
    {
        DestroyingAllGlasses();
    }
    public void UpdatingIslas()
    {
        if (!GameManager.GameFinished)
        {
            if(UIManager.onV2Game)
                StartCoroutine(UpdatingSecondPlayerIslands());
            StartCoroutine(UpdatingFirstPlayerIslands());
        }
    }

    private IEnumerator UpdatingFirstPlayerIslands()
    {
        //the islands of the first player are refered by the second player glasses.
        yield return new WaitForSeconds(0.1f);
        if (!(secondPlayerGlasses[1].activeInHierarchy) && !(secondPlayerGlasses[3].activeInHierarchy))
        {
            islaGlasses.Add(secondPlayerGlasses[0]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
            }
        }
        if (!(secondPlayerGlasses[0].activeInHierarchy) && !(secondPlayerGlasses[2].activeInHierarchy) && !(secondPlayerGlasses[3].activeInHierarchy) && !(secondPlayerGlasses[4].activeInHierarchy))
        {
            islaGlasses.Add(secondPlayerGlasses[1]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
            }
        }
        if (!secondPlayerGlasses[1].activeInHierarchy && !secondPlayerGlasses[4].activeInHierarchy)
        {
            islaGlasses.Add(secondPlayerGlasses[2]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
            }
        }
        if (!secondPlayerGlasses[0].activeInHierarchy && !secondPlayerGlasses[1].activeInHierarchy && !secondPlayerGlasses[4].activeInHierarchy && !secondPlayerGlasses[5].activeInHierarchy)
        {
            islaGlasses.Add(secondPlayerGlasses[3]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
            }
        }
        if (!secondPlayerGlasses[1].activeInHierarchy && !secondPlayerGlasses[2].activeInHierarchy && !secondPlayerGlasses[3].activeInHierarchy && !secondPlayerGlasses[5].activeInHierarchy)
        {
            islaGlasses.Add(secondPlayerGlasses[4]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }

        }
        if (!secondPlayerGlasses[3].activeInHierarchy && !secondPlayerGlasses[4].activeInHierarchy)
        {
            islaGlasses.Add(secondPlayerGlasses[5]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }
        }
    }
    private IEnumerator UpdatingSecondPlayerIslands()
    {
        //the islands of the second player are refered by the first player glasses.
        yield return new WaitForSeconds(0.5f);
        if (!(firstPlayerGlasses[1].activeInHierarchy) && !(firstPlayerGlasses[3].activeInHierarchy))
        {
            islaGlasses.Add(firstPlayerGlasses[0]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }
        }
        if (!(firstPlayerGlasses[0].activeInHierarchy) && !(firstPlayerGlasses[2].activeInHierarchy) && !(firstPlayerGlasses[3].activeInHierarchy) && !(firstPlayerGlasses[4].activeInHierarchy))
        {
            islaGlasses.Add(firstPlayerGlasses[1]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }
        }
        if (!firstPlayerGlasses[1].activeInHierarchy && !firstPlayerGlasses[4].activeInHierarchy)
        {
            islaGlasses.Add(firstPlayerGlasses[2]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }
        }
        if (!firstPlayerGlasses[0].activeInHierarchy && !firstPlayerGlasses[1].activeInHierarchy && !firstPlayerGlasses[4].activeInHierarchy && !firstPlayerGlasses[5].activeInHierarchy)
        {
            islaGlasses.Add(firstPlayerGlasses[3]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }
        }
        if (!firstPlayerGlasses[1].activeInHierarchy && !firstPlayerGlasses[2].activeInHierarchy && !firstPlayerGlasses[3].activeInHierarchy && !firstPlayerGlasses[5].activeInHierarchy)
        {
            islaGlasses.Add(firstPlayerGlasses[4]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }

        }
        if (!firstPlayerGlasses[3].activeInHierarchy && !firstPlayerGlasses[4].activeInHierarchy)
        {
            islaGlasses.Add(firstPlayerGlasses[5]);
            if (islaCount == 0 && islaGlasses.Count > 0)
            {
                ActivatingIsla(1);
                ShowingIslasAvaility();
                //islaBtn.SetActive(true);
            }
        }
    }
    private void ActivatingIsla(int activating)
    {
        if(islaCount == activating)
        {
            return;
        }
        else
        {
            canShootIsla = true;
            islaCount = 1;
            //islaBtn.gameObject.SetActive(false);
        }
    }
    private void ShowingIslasAvaility()
    {
        if(islaCount == 1)
        {
            foreach(var Islas in islaGlasses)
            {
                //Islas.GetComponent<MeshRenderer>().material.color = Color.black;
                if(Islas.activeInHierarchy)
                    Islas.GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
                else
                {
                    islaCount = 0;
                }
            }
        }
    }
    private void DestroyingAllGlasses()
    {
        for(int i = 0; i < firstPlayerGlasses.Length; i++)
            Destroy(firstPlayerGlasses[i]);
        for (int i = 0; i < secondPlayerGlasses.Length; i++)
            Destroy(secondPlayerGlasses[i]);
    }
}
