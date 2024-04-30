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

        dialogTexts.Add(new DialogData("Teraz mówi kot", "Kot", true));
        dialogTexts.Add(new DialogData("Teraz mówi kot", "Kot", true, "Szczur", false));
        dialogTexts.Add(new DialogData("Teraz mówi kot", "Kot", true));
        dialogTexts.Add(new DialogData("Teraz mówi szczur", "Kot", false, "Szczur", true));
        dialogTexts.Add(new DialogData("Teraz mówi szczur", "", false, "Szczur", true));
        dialogTexts.Add(new DialogData("Teraz mówi¹ oboje", "Kot", true, "Szczur", true));
        dialogTexts.Add(new DialogData("/speed:0/Kto jest prawdziwym sigm¹?", "Kot", true, "", callback: () => Check_Correct()));

        dialogTexts[6].SelectList.Add("Correct", "SigmaRemek");
        dialogTexts[6].SelectList.Add("Wrong", "Bruno Baran");
        dialogTexts[6].SelectList.Add("Jesteœ durny?", "Jacob Coal");

        DialogManager.Show(dialogTexts);
    }

    private void Check_Correct()
    {
        var dialogTexts = new List<DialogData>();

        switch (DialogManager.Result)
        {
            case ("Correct"):
                dialogTexts.Add(new DialogData("No wiadomo, ¿e SigmaRemek. To by³a oczywiœcie jedyna poprawna odpowiedŸ", "Kot", true));
                break;

            case ("Wrong"):
                dialogTexts.Add(new DialogData("Nie no Bruno Baran to nie jest sigma. Musisz siê doedukowaæ.", "Kot", true));
                break;

            case ("Jesteœ durny?"):
                dialogTexts.Add(new DialogData("Przecie¿ Jacob Coal to jakiœ pet. Musisz siê doedukowaæ.", "Kot", true));
                break;
        }

        DialogManager.Show(dialogTexts);
    }
}
