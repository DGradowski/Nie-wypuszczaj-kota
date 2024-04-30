/*
The MIT License

Copyright (c) 2020 DoublSB
https://github.com/DoublSB/UnityDialogAsset

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Doublsb.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        //================================================
        //Public Variable
        //================================================
        [Header("Game Objects")]
        public GameObject Printer;
        public GameObject CharactersLeft;
        public GameObject CharactersRight;
        public GameObject nameWindowLeft;
        public GameObject nameWindowRight;


        [Header("UI Objects")]
        public Text Printer_Text;
        public TMP_Text nameTextLeft;
        public TMP_Text nameTextRight;

        [Header("Audio Objects")]
        public AudioSource SEAudio;
        public AudioSource CallAudio;

        [Header("Preference")]
        public float Delay = 0.1f;

        [Header("Selector")]
        public GameObject Selector;
        public GameObject SelectorItem;
        public Text SelectorItemText;

        [HideInInspector]
        public State state;

        [HideInInspector]
        public string Result;

        //================================================
        //Private Method
        //================================================
        private Character _current_Character_Left;
        private Character _current_Character_Right;
        private DialogData _current_Data;

        private float _currentDelay;
        private float _lastDelay;
        private Coroutine _textingRoutine;
        private Coroutine _printingRoutine;

        //================================================
        //Public Method
        //================================================
        #region Show & Hide
        public void Show(DialogData Data)
        {
            _current_Data = Data;
            _hide_characters();
            _find_character_left(Data.CharacterLeft);
            _find_character_right(Data.CharacterRight);

            Printer.GetComponent<Button>().Select();

            if (_current_Character_Left != null)
                _emote("Normal", _current_Character_Left);

            if (_current_Character_Right != null)
                _emote("Normal", _current_Character_Right);

            nameWindowLeft.gameObject.SetActive(false);
            nameWindowRight.gameObject.SetActive(false);

            if (_current_Data.isLeftTalking == true)
            {
                nameWindowLeft.gameObject.SetActive(true);
                nameTextLeft.text = _current_Data.CharacterLeft;
            }

            if (_current_Data.isRightTalking == true)
            {
                nameWindowRight.gameObject.SetActive(true);
                nameTextRight.text = _current_Data.CharacterRight;
            }

            _textingRoutine = StartCoroutine(Activate());
        }

        public void Show(List<DialogData> Data)
        {
            StartCoroutine(Activate_List(Data));
        }

        public void Click_Window()
        {
            switch (state)
            {
                case State.Active:
                    StartCoroutine(_skip()); break;

                case State.Wait:
                    if(_current_Data.SelectList.Count <= 0) Hide(); break;
            }
        }

        public void Hide()
        {
            if(_textingRoutine != null)
                StopCoroutine(_textingRoutine);

            if(_printingRoutine != null)
                StopCoroutine(_printingRoutine);

            Printer.SetActive(false);
            CharactersLeft.SetActive(false);
            CharactersRight.SetActive(false);
            Selector.SetActive(false);
            nameWindowLeft.gameObject.SetActive(false);
            nameWindowRight.gameObject.SetActive(false);

            state = State.Deactivate;

            if (_current_Data.Callback != null)
            {
                _current_Data.Callback.Invoke();
                _current_Data.Callback = null;
            }
        }
        #endregion

        #region Selector

        public void Select(int index)
        {
            Result = _current_Data.SelectList.GetByIndex(index).Key;
            Hide();
        }

        #endregion

        #region Sound

        public void Play_ChatSE()
        {
            if (_current_Character_Left != null && _current_Data.isLeftTalking)
            {
                SEAudio.clip = _current_Character_Left.ChatSE[UnityEngine.Random.Range(0, _current_Character_Left.ChatSE.Length)];
                SEAudio.Play();
            }
            if (_current_Character_Right != null && _current_Data.isRightTalking)
            {
                SEAudio.clip = _current_Character_Right.ChatSE[UnityEngine.Random.Range(0, _current_Character_Right.ChatSE.Length)];
                SEAudio.Play();
            }
        }

        public void Play_CallSE(string SEname)
        {
            if (_current_Character_Left != null)
            {
                var FindSE
                    = Array.Find(_current_Character_Left.CallSE, (SE) => SE.name == SEname);

                CallAudio.clip = FindSE;
                CallAudio.Play();
            }
        }

        #endregion

        #region Speed

        public void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    _currentDelay -= 0.25f;
                    if (_currentDelay <= 0) _currentDelay = 0.001f;
                    break;

                case "down":
                    _currentDelay += 0.25f;
                    break;

                case "init":
                    _currentDelay = Delay;
                    break;

                default:
                    // For some reason it doesn't work for float values. Int values seems fine.
                    _currentDelay = float.Parse(speed);
                    break;
            }

            _lastDelay = _currentDelay;
        }

        #endregion

        //================================================
        //Private Method
        //================================================

        private void _hide_characters()
        {
            _current_Character_Left = null;
            _current_Character_Right = null;

            foreach(Transform character in CharactersLeft.transform)
            {
                character.gameObject.SetActive(false);
            }
            foreach (Transform character in CharactersRight.transform)
            {
                character.gameObject.SetActive(false);
            }
        }

        private void _find_character_left(string name)
        {
            if (name != string.Empty)
            {
                Transform Child = CharactersLeft.transform.Find(name);
                if (Child != null) _current_Character_Left = Child.GetComponent<Character>();
            }
        }
        private void _find_character_right(string name)
        {
            if (name != string.Empty)
            {
                Transform Child = CharactersRight.transform.Find(name);
                if (Child != null) _current_Character_Right = Child.GetComponent<Character>();
            }
        }

        private void _initialize_left()
        {
            _currentDelay = Delay;
            _lastDelay = 0.1f;
            Printer_Text.text = string.Empty;

            Printer.SetActive(true);

            CharactersLeft.SetActive(_current_Character_Left != null);
            foreach (Transform item in CharactersLeft.transform) item.gameObject.SetActive(false);
            if(_current_Character_Left != null) _current_Character_Left.gameObject.SetActive(true);
        }

        private void _initialize_right()
        {
            _currentDelay = Delay;
            _lastDelay = 0.1f;
            Printer_Text.text = string.Empty;

            Printer.SetActive(true);

            CharactersRight.SetActive(_current_Character_Right != null);
            foreach (Transform item in CharactersRight.transform) item.gameObject.SetActive(false);
            if (_current_Character_Right != null) _current_Character_Right.gameObject.SetActive(true);
        }

        private void _init_selector()
        {
            _clear_selector();

            if (_current_Data.SelectList.Count > 0)
            {
                Selector.SetActive(true);

                for (int i = 0; i < _current_Data.SelectList.Count; i++)
                {
                    _add_selectorItem(i);
                }
            }
                
            else Selector.SetActive(false);
        }

        private void _clear_selector()
        {
            for (int i = 1; i < Selector.transform.childCount; i++)
            {
                Destroy(Selector.transform.GetChild(i).gameObject);
            }
        }

        private void _add_selectorItem(int index)
        {
            SelectorItemText.text = _current_Data.SelectList.GetByIndex(index).Value;

            var NewItem = Instantiate(SelectorItem, Selector.transform);
            NewItem.GetComponent<Button>().onClick.AddListener(() => Select(index));
            NewItem.SetActive(true);
            if (index == 0)
            {
                NewItem.GetComponent<Button>().Select();
            }
        }

        #region Show Text

        private IEnumerator Activate_List(List<DialogData> DataList)
        {
            state = State.Active;

            foreach (var Data in DataList)
            {
                Show(Data);
                _init_selector();

                while (state != State.Deactivate) { yield return null; }
            }
        }

        private IEnumerator Activate()
        {
            _initialize_left();
            _initialize_right();

            state = State.Active;

            foreach (var item in _current_Data.Commands)
            {
                switch (item.Command)
                {
                    case Command.print:
                        yield return _printingRoutine = StartCoroutine(_print(item.Context));
                        break;

                    case Command.color:
                        _current_Data.Format.Color = item.Context;
                        break;

                    case Command.emoteleft:
                        _emote(item.Context, _current_Character_Left);
                        break;

                    case Command.emoteright:
                        _emote(item.Context, _current_Character_Right);
                        break;

                    case Command.size:
                        _current_Data.Format.Resize(item.Context);
                        break;

                    case Command.sound:
                        Play_CallSE(item.Context);
                        break;

                    case Command.speed:
                        Set_Speed(item.Context);
                        break;

                    case Command.click:
                        yield return _waitInput();
                        break;

                    case Command.close:
                        Hide();
                        yield break;

                    case Command.wait:
                        yield return new WaitForSeconds(float.Parse(item.Context));
                        break;
                }
            }

            state = State.Wait;
        }

        private IEnumerator _waitInput()
        {
            while (!Input.GetMouseButtonDown(0)) yield return null;
            _currentDelay = _lastDelay;
        }

        private IEnumerator _print(string Text)
        {
            _current_Data.PrintText += _current_Data.Format.OpenTagger;

            for (int i = 0; i < Text.Length; i++)
            {
                _current_Data.PrintText += Text[i];
                Printer_Text.text = _current_Data.PrintText + _current_Data.Format.CloseTagger;

                if (Text[i] != ' ') Play_ChatSE();
                if (_currentDelay != 0) yield return new WaitForSeconds(_currentDelay);
            }

            _current_Data.PrintText += _current_Data.Format.CloseTagger;
        }

        public void _emote(string Text, Character _current_character)
        {
            _current_character.GetComponent<Image>().sprite = _current_character.Emotion.Data[Text];
        }

        private IEnumerator _skip()
        {
            if (_current_Data.isSkippable)
            {
                _currentDelay = 0;
                while (state != State.Wait) yield return null;
                _currentDelay = Delay;
            }
        }

        #endregion

    }
}