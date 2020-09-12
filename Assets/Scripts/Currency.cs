using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField]
    public int wallet;
    public Text textDisplay;
    // Start is called before the first frame update
    void Awake()
    {
        ScriptAwoke();
    }

    public void ScriptAwoke()
    {
        wallet = playerData.wallet;
        textDisplay.text = wallet.ToString("f0");
        UpdatePlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToLocalWallet(int addAmount)
    {
        wallet += addAmount;
        textDisplay.text = wallet.ToString("f0");
        UpdatePlayerData();
    }

    public void RemoveFromLocalWallet(int removeAmount)
    {
        wallet -= removeAmount;
        if (wallet - removeAmount < 0)
        {
            wallet = 0;
        }
        textDisplay.text = wallet.ToString("f0");
        UpdatePlayerData();
    }

    public void UpdatePlayerData()
    {
        playerData.wallet = wallet;
    }
}
