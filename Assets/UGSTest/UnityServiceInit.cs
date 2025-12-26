using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
public class UnityServiceInit : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync(); //깔려있는 유니티 서비스 sdk들 초기화
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    
}
