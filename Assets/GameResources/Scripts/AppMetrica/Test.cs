using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        AppMetrica.Instance.SetLocationTracking(true); //Application.systemLanguage

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();

        userProfile = userProfile.Apply(YandexAppMetricaStringAttribute.WithValue($"lang - {Application.systemLanguage}"));
        AppMetrica.Instance.ReportUserProfile();
    }

}
