using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Handman : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private int hp = 7;
    [SerializeField] private TextMeshProUGUI _textFieldTwo;
    [SerializeField] private TextMeshProUGUI _textFieldG;
    [SerializeField] private TextMeshProUGUI _HP;

    [SerializeField] private TextMeshProUGUI _textFieldAny;
        
        private List<char> guessedLetters = new List<char>();
        private List<char> wrongTriedLetter = new List<char>();

        private string[] words =
        {
            "Yellow",
            "Black",
            "Red",
            "Green",
            "Grey"
        };

        private string wordToGuess = "";
        
        private KeyCode lastKeyPressed;

        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);

            wordToGuess = words[randomIndex];
        }


        void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                // Debug.Log("Detected key code: " + e.keyCode);

                if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
                {
                    ProcessKey(e.keyCode);
                    
                    lastKeyPressed = e.keyCode;

                    // (lastKeyPressed == KeyCode.G)
                    //{
                    //    _textFieldG.color = new Color(0, 25, 0);
                    //}
                }
            }
        }

        private void ProcessKey(KeyCode key)
        {
            print("Key Pressed: " + key);

            char pressedKeyString = key.ToString()[0];

            string wordUppercase = wordToGuess.ToUpper();
            
            bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
            bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

            if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
            {
                wrongTriedLetter.Add(pressedKeyString);
                hp -= 1;

                if (hp <= 0)
                {
                    print("You Lose!");
                    string Lose = "You Lose";
                    _textFieldTwo.text = Lose;
                }
                else
                {
                    print("Wrong letter! Hp left = " + hp);
                }
            }
            
            if (wordContainsPressedKey && !letterWasGuessed)
            {
                guessedLetters.Add(pressedKeyString);
            }

            string stringToPrint = "";
            for (int i = 0; i < wordUppercase.Length; i++)
            {
                char letterInWord = wordUppercase[i];

                if (guessedLetters.Contains(letterInWord))
                {
                    stringToPrint += letterInWord;
                }
                else
                {
                    stringToPrint += "_";
                }
            }

            if (wordUppercase == stringToPrint)
            {
                string Win = "You win";
                print("You win!");
                _textFieldTwo.text = Win;
            }

            // print(string.Join(", ", guessedLetters));
            print(stringToPrint);
            _textField.text = stringToPrint;

            if (Input.GetKeyDown(KeyCode.G))
            {
                _textFieldG.color = Color.red;
            }

        }
}
