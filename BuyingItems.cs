using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingItems : MonoBehaviour
{
    [SerializeField] private GameObject UseBtn;
    [SerializeField] private int price;
    [SerializeField] private int id;
    [SerializeField] private UserManager userManager;

    // Start is called before the first frame update
    void Start()
    {
        userManager = GameObject.FindGameObjectWithTag("UserManager").GetComponent<UserManager>();
        this.GetComponent<Button>().onClick.AddListener(UpdateBtn);
        if (userManager.ballArrayAdded.Contains(id))
        {
            this.gameObject.SetActive(false);
            UseBtn.SetActive(true);
        }
    }
    public void UpdateBtn()
    {
        if(userManager.coins > price)
        {
            this.gameObject.SetActive(false);
            UseBtn.SetActive(true);
            userManager.UpdateAfterBuying(price,id);
        }
    }
}
