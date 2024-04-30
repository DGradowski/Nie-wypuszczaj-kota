using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;

public class DialogueTest : MonoBehaviour
{
    public DialogManager DialogManager;

    public void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("Teraz m�wi kot", "Kot", true));
        dialogTexts.Add(new DialogData("Teraz m�wi kot", "Kot", true, "Szczur", false));
        dialogTexts.Add(new DialogData("Teraz m�wi kot", "Kot", true));
        dialogTexts.Add(new DialogData("Teraz m�wi szczur", "Kot", false, "Szczur", true));
        dialogTexts.Add(new DialogData("Teraz m�wi szczur", "", false, "Szczur", true));
        dialogTexts.Add(new DialogData("Teraz m�wi� oboje", "Kot", true, "Szczur", true));
        dialogTexts.Add(new DialogData("/speed:0/Kto jest prawdziwym sigm�?", "Kot", true, "", callback: () => Check_Correct()));

        dialogTexts[6].SelectList.Add("Correct", "SigmaRemek");
        dialogTexts[6].SelectList.Add("Wrong", "Bruno Baran");
        dialogTexts[6].SelectList.Add("Jeste� durny?", "Jacob Coal");

        DialogManager.Show(dialogTexts);
    }

    private void Check_Correct()
    {
        var dialogTexts = new List<DialogData>();

        switch (DialogManager.Result)
        {
            case ("Correct"):
                dialogTexts.Add(new DialogData("No wiadomo, �e SigmaRemek. To by�a oczywi�cie jedyna poprawna odpowied�", "Kot", true));
                break;

            case ("Wrong"):
                dialogTexts.Add(new DialogData("Nie no Bruno Baran to nie jest sigma. Musisz si� doedukowa�.", "Kot", true));
                break;

            case ("Jeste� durny?"):
                dialogTexts.Add(new DialogData("Przecie� Jacob Coal to jaki� pet. Musisz si� doedukowa�.", "Kot", true));
                break;
        }

        DialogManager.Show(dialogTexts);
    }
}
