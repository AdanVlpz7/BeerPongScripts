using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidDetector : MonoBehaviour
{
    [Tooltip("The GameObject parent, in this case, the cup model.")][SerializeField] private GameObject parentObj;

    public static bool playerScored = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            //Destroy the ball clone and deactivate the glass.
            StartCoroutine(DestroyingGlass(other.gameObject,  GameManager.isFirstPlayerTurn));
        }
    }
    IEnumerator DestroyingGlass(GameObject ballClone,bool turn)
    {
        yield return new WaitForSeconds(1f);
        if (turn)
        {
            playerScored = true;
            GameManager.isFirstPlayerTurn = false;
            GameManager.secondPlayerGlasses -= 1;

        }
        else
        {
            GameManager.isFirstPlayerTurn = true;
            GameManager.firstPlayerGlasses -= 1;
        }
        Destroy(ballClone);
        parentObj.SetActive(false);
    }
}
