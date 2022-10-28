using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgeConfirmation : MonoBehaviour
{
    int year, month, day;
    public TMP_Dropdown tmp_year, tmp_month, tmp_day;
    public Button confirmBtn;
    public Toggle toggle;
    SplashScreen ss;
    void Start()
    {
        ss = GetComponent<SplashScreen>();
    }

    public void ConfirmBTN()
    {

        year = tmp_year.value + 1999;
        month = tmp_month.value;
        day = tmp_day.value;
        int age;
        try
        {
            var GetDateNow = DateTime.Now;
            var playerBday = new DateTime(year, month, day);
            var playerAge = GetDateNow.Subtract(playerBday);
            age = (int)((playerAge.TotalDays / 365));
        }
        catch
        {
            return;
        }
        if (age >=16)
        {
            ss.GoToMainMenu();
        }
        else
        {
            toggle.onValueChanged.RemoveAllListeners();
            confirmBtn.gameObject.SetActive(false);
            StartCoroutine(ExitApplication());
        }

        IEnumerator ExitApplication()
        {
            yield return new WaitForSeconds(3);
            Application.Quit();
        }
    }
}
