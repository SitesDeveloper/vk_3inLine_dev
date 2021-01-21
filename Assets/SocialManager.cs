using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class SocialManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string _GetURLParams();

    [DllImport("__Internal")]
    private static extern void _VK_init();

    [DllImport("__Internal")]
    private static extern void _VK_get_user(string vk_user_id);

    [DllImport("__Internal")]
    private static extern void _VK_call_method(string method);

    [DllImport("__Internal")]
    private static extern void _VK_postToWall(string txt);



    public TMP_Text userText;
    public TMP_Text urlParamsText;
    public TMP_Text debugText;

    private static SocialManager _instance;
    public static SocialManager Instance
    {
        get { return _instance; }
    }

    public bool IsVkInited { get; private set; } = false;
    public bool IsUserLoaded { get; private set; } = false;
    public string UserId { get; private set; }
    public string AuthKey { get; private set; }
    public string Protocol { get; private set; }

    public readonly Dictionary<string, SocialData> SocialData = new Dictionary<string, SocialData>(32);

    void Awake()
    {
        _instance = this;
        LogWrite("Create SocialManager instance.");
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //Application.ExternalCall("GetParams");
        GetParams();
        //VK_init();
    }

    public void LogWrite( string mes )
    {
        debugText.text = debugText.text + "\n" + mes;
        //Debug.Log("SM->" + mes);
    }


    public void GetParams()
    {
        string a = _GetURLParams();
        urlParamsText.text = a;

        //LogWrite("UrlParams:" + a);
        if (a.StartsWith("https"))
            Protocol = "https";
        else
            Protocol = "http";
        a = a.Split('?')[1];
        string[] mas = a.Split('&');
        foreach (string s in mas)
        {
            string[] k = s.Split('=');
            switch( k[0] )
            {
                case "viewer_id":
                    UserId = k[1];
                    LogWrite("UserID set to " + UserId);
                    break;

                case "auth_key":
                    AuthKey = k[1];
                    LogWrite("AuthKey set to " + AuthKey);
                    break;
            }
        }

        //GetUserInfo(UserId);
        //IsLoaded = true;
    }



    #region CALLBACKS
    public void OnVkError(string errStr)
    {
        LogWrite("Error: " + errStr);
    }

    public void OnVkInit(string resStr)
    {
        if (resStr=="NORMAL")
        {
            IsVkInited = true;
            LogWrite("VK init OK.");
        }
        else //if (resStr == "FAIL")
        {
            IsVkInited = false;
            LogWrite("VK init FAIL.");
        }
    }

    public void OnVkGetUser(string str)
    {
        var mas = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        var socData = new SocialData
        {
            ViewerId = mas[0],
            FirstName = mas[1],
            LastName = mas[2],
            Photo = mas[3]
        };

        if (SocialData.ContainsKey(socData.ViewerId))
        {
            SocialData[socData.ViewerId] = socData;
        }
        else
        {
            SocialData.Add(socData.ViewerId, socData);
        }
        LogWrite("OnVkGetUser OK");
        LogWrite(" resultStr: " + str);
        LogWrite(" uid: " + socData.ViewerId);
        LogWrite(" FName: " + socData.FirstName);
        LogWrite(" LName: " + socData.LastName);
        LogWrite(" Photo: " + socData.Photo);
    }

    #endregion



    public void VK_initialize()
    {
        _VK_init();
    }

    //загрузка юзера с данным vk_user_id, и помещение в dictionary
    public void VK_get_user(string vk_user_id)
    {
        if (Instance.SocialData.ContainsKey(vk_user_id))
        {
            LogWrite("Данные юзера " + vk_user_id.ToString() + "  уже загружены");
            return;
        }
        _VK_get_user(vk_user_id);
        LogWrite("VK get user " + vk_user_id + " start");
    }

    public void VK_call_method( string method )
    {
        _VK_call_method(method);
    }

    public void VK_postToWall(string txt )
    {
        _VK_postToWall(txt);
    }


    /*
    public static void PostToWall(string text)
    {
        Application.ExternalCall("PostToWall", text);
    }

    public static void GetUserInfo(string viewer_id)
    {
        if (Instance.SocialData.ContainsKey(viewer_id))
            return;
        Application.ExternalCall("GetProfile", viewer_id);
    }

    public static void InviteFriends() {
        Application.ExternalCall("ShowInvite");
    }
    */


}
