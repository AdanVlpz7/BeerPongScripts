using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidDetector : MonoBehaviour
{
    [Tooltip("The GameObject parent, in this case, the cup model.")][SerializeField] private GameObject parentObj;
    [Tooltip("The GameObject parent, in this case, the cup model.")][SerializeField] private ParticleSystem gotasCerveza;
    [Tooltip("The GameObject parent, in this case, the cup model.")][SerializeField] private AudioSource audioScoring;
    private IslaMechanic islaMechanic;
    
    public static bool playerScored = false;
    public static bool secondPlayerScored = false;
    public static bool imAnIsla = false;

    private void OnEnable()
    {
        audioScoring = this.GetComponent<AudioSource>();
        islaMechanic = GameObject.Find("IslaAdmin").GetComponent<IslaMechanic>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gotasCerveza.Stop();
            if (gotasCerveza.isStopped) gotasCerveza.Play(true);
            audioScoring.Play();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            //Destroy the ball clone and deactivate the glass.
            StartCoroutine(DestroyingGlass(other.gameObject,  GameManager.isFirstPlayerTurn));
        }
    }
    private IEnumerator DestroyingGlass(GameObject ballClone,bool turn)
    {
        if (turn)
        {
            playerScored = true;
            GameManager.isFirstPlayerTurn = false;
            GameManager.secondPlayerGlasses -= 1;
            if(islaMechanic.youScoredAnIsla)
            {
                imAnIsla = true;
                islaMechanic.canShootIsla = false;
                islaMechanic.youScoredAnIsla = false;
            }
            else
            {
                imAnIsla = false;
            }
        }
        if(!turn)
        {
            secondPlayerScored = true;
            GameManager.isFirstPlayerTurn = true;
            GameManager.firstPlayerGlasses -= 1;
        }
        Destroy(ballClone);
        yield return new WaitForSeconds(1.5f);
        //Destroy(ballClone);
        parentObj.SetActive(false);
        playerScored = false;

    }
}
