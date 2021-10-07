using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LicenseVerification : MonoBehaviour
{
    [SerializeField] private MainWork mainWork;
    [SerializeField] private Transform licenseVerification;

    [Header("LicenseVerification")]
    [SerializeField] private InputField VerificationInput;
    [SerializeField] private Text textWorning;

    [Header("CodeGeneration")]
    [SerializeField] private InputField generationInput;
    [SerializeField] private InputField generationOutput;

    private long codeVersion;
    private CdOd cdod;
    private string CdOdPath;


    private void Awake() {
        mainWork.enabled = false;
        licenseVerification.gameObject.SetActive(true);
    }

    void Start() {
        CdOdPath = Application.persistentDataPath + "/gamemanagerplay";
        codeVersion = Convert.ToInt64(Application.version.Replace(".", ""));

        if (File.Exists(CdOdPath)) {
            ReadingCode();
            CheckingTheDate();
        } 
    }


    public void CheckCode() {
        long date = 0;
        try {
            date = InDecimal(InDecimal(Convert.ToInt64(VerificationInput.text, 16) / codeVersion, 8) / codeVersion, 8);
        } catch (Exception e) {
            textWorning.text = "Обратитесь к сооздателю программы: " + e;
            return;
        }

        if (date.ToString().Length > 8) {
            textWorning.text = "Обратитесь к сооздателю программы: >8";
        } else if (date.ToString().Length < 8) {
            textWorning.text = "Обратитесь к сооздателю программы: <8";
        }

        WritingCode(Convert.ToInt64(VerificationInput.text, 16));
        CheckingTheDate();
    }

    public void CodeGeneration() {
        try {
            generationOutput.text = InHexadecimal(InOctal(codeVersion * InOctal(Convert.ToInt64(generationInput.text))) * codeVersion);
        } catch (Exception e) {
            generationOutput.text = "Ошибка: " + e;
        }
    }

    private string InHexadecimal(long number) {
        return Convert.ToString(number, 16);
    }

    private long InOctal(long number) {
        return Convert.ToInt64(Convert.ToString(number, 8));
    }

    private long InDecimal(long number, int system) {
        return Convert.ToInt64(number.ToString(), system);
    }
    private long InDecimal(string number, int system) {
        return Convert.ToInt64(number, system);
    }

    private void CheckingTheDate() {
        string cdodDate;
        try {
            cdodDate = InDecimal(InDecimal(cdod.number / codeVersion, 8) / codeVersion, 8).ToString();
        } catch (Exception e) {
            textWorning.text = "Обратитесь к сооздателю программы: " + e;
            return;
        }
        if(cdodDate.Length < 8) {
            cdodDate = cdodDate.Insert(0, "0");
            if(cdodDate.Length < 8) {
                textWorning.text = "Обратитесь к сооздателю программы: <8'2'";
                return;
            }
        }

        DateTime registeredDate = new DateTime(
            Convert.ToInt32(cdodDate.Remove(0, 4)),
            Convert.ToInt32(cdodDate.Remove(0, 2).Remove(2, 4)),
            Convert.ToInt32(cdodDate.Remove(2, 6))
            );
        DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        TimeSpan difference = registeredDate.Subtract(currentDate);

        if (difference > TimeSpan.Zero) {
            mainWork.enabled = true;
            licenseVerification.gameObject.SetActive(false);

        } else if (difference <= TimeSpan.Zero) {
            textWorning.text = "Код устарел";
        }
    }


    private void ReadingCode() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(CdOdPath, FileMode.Open);
        cdod = (CdOd)bf.Deserialize(fs);
        fs.Close();
    } 
    private void WritingCode(long code) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(CdOdPath, FileMode.Create);

        cdod = new CdOd();
        cdod.number = code;
        
        bf.Serialize(fs, cdod);
        fs.Close();
    }
}
