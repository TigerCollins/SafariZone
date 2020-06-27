using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using UnityEngine.UI;

public class CommunityMessage : MonoBehaviour
{
    public struct userAttributes { }

    public struct appAttributes { }

    public PlayerData playerData;
    public SplashScreen splashScreen;
    public string messageDate;
    public string messageString;
    public Text dateText;
    public Text messageText;

    // Start is called before the first frame update
    void Awake()
    {
        CallConfigManager();
    }

    public void CallConfigManager()
    {
      
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        messageString = ConfigManager.appConfig.GetString("Community Message");
        messageDate = ConfigManager.appConfig.GetString("Date");
        ConfigManager.FetchCompleted += CheckMessage;
    }



    void CheckMessage(ConfigResponse response)
    {
        messageString = ConfigManager.appConfig.GetString("Community Message");
        messageDate = ConfigManager.appConfig.GetString("Date");
        if (response.status == ConfigRequestStatus.Success)
        {
            if (messageString != playerData.latestCommunityMessage)
            {
                print("oh my");
                playerData.latestCommunityMessage = messageString;
                playerData.latestCommunityDate = messageDate;
                splashScreen.showCommmunityMessage = true;
            }

            else
            {
                print("oh myyyyyyy");
                splashScreen.showCommmunityMessage = false;
            }
            dateText.text = messageDate;
            messageText.text = messageString;

        }
    }
        // Update is called once per frame
        void Update()
        {
            
        }
    
}
