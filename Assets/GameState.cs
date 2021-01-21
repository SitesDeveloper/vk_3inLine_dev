using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameState : MonoBehaviour
{ 
    
    private SocialManager SM { get { return SocialManager.Instance; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnGUI()
    {
        if (SM.SocialData.ContainsKey(SM.UserId))
        {
            var data = SM.SocialData[SM.UserId];
            GUI.Label(new Rect(Screen.width / 2 - 100, 10, Screen.width, 20), "NAME: " + data.FormatName);
        }

        if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2,100,20), "Invite"))
        {
            SocialManager.InviteFriends();
        }

        if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2+30,100,20),"Post"))
        {
            SocialManager.PostToWall("Hello");
        }

    }
    */


    public void OnButtonsClick( string strBtn )
    {
        if (strBtn=="vk_init") 
        {
            if (!SM.IsVkInited)
            {
                SM.VK_initialize();
                SM.LogWrite("VK попытка запуска инициализации");
            }
            else
            {
                SM.LogWrite("VK уже инициализирован");
            }
        }
        else if (SM.IsVkInited)
        {
            switch (strBtn)
            {
                case "vk_user":
                    {
                        SM.VK_get_user(SM.UserId);
                        break;
                    }

                case "vk_invite":
                    {
                        SM.VK_call_method("showInviteBox");
                        break;
                    }

                case "vk_post_to_wall":
                    {
                        SM.VK_postToWall("Публикация на стене из игры vk_3inLine. ABCDE & АБВГД");
                        break;
                    }
            }
        }
        else
        {
            SM.LogWrite("Сначала надо выполнить VK_init");
        }



    }

}
