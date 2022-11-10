using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        AppMetrica.Instance.SetLocationTracking(true); //Application.systemLanguage   SystemInfo.deviceUniqueIdentifier

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        YandexAppMetricaStringAttribute newAttribute = new YandexAppMetricaStringAttribute("testLang");

        userProfile = userProfile.Apply(newAttribute.WithValue($"lang - {SystemInfo.deviceUniqueIdentifier}"));
   
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }

}
