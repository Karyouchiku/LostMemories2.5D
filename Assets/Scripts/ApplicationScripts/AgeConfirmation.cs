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

    public TMP_Text message;
    public GameObject inputDate;
    public Toggle toggle;
    public Button confirmBtn;
    SplashScreen ss;
    void Start()
    {
        ss = GetComponent<SplashScreen>();
    }

    public void ConfirmBTN()
    {
        Debug.Log($"This year: {DateTime.Now.Year}");
        year = tmp_year.value;
        month = tmp_month.value;
        day = tmp_day.value;
        int age = 0;
        try
        {
            month = month == 0 ? throw new Exception("Input Month Exception") : month;
            day = day == 0 ? throw new Exception("Input Day Exception") : day;
            year = year == 0 ? throw new Exception("Input Year Exception") : year + 1979;


            var GetDateNow = DateTime.Now;
            var playerBday = new DateTime(year, month, day);
            var playerAge = GetDateNow.Subtract(playerBday);
            age = (int)((playerAge.TotalDays / 365));
        }
        catch(Exception e)
        {
            Debug.Log($"Age: {age}");
            Debug.Log($"Exception: {e}");
            
            return;
        }
        if (age >=16 && age <= 25)
        {
            //Debug.Log($"Age: {age}");
            ss.GoToMainMenu();
        }
        else if (age < 16)
        {
            message.text = "We apologize that your age is not eligible for playing the game because of its explicit content. The game will quit automatically after few seconds.";
            inputDate.SetActive(false);
            toggle.gameObject.SetActive(false);
            confirmBtn.gameObject.SetActive(false);
            StartCoroutine(ExitApplication());
        }
        else if (age > 25)
        {
            message.text = "We apologize that your age is not eligible for playing the game. The game will quit automatically after few seconds.";
            inputDate.SetActive(false);
            toggle.gameObject.SetActive(false);
            confirmBtn.gameObject.SetActive(false);
            StartCoroutine(ExitApplication());
        }

        IEnumerator ExitApplication()
        {
            yield return new WaitForSeconds(10);
            Debug.Log("Game automatically Quitted ");
            Application.Quit();
        }
    }
}
