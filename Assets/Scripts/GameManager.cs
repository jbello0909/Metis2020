using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
     // Imports C++ DLL
     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern Int32 GetTestNumber();

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern IntPtr GetTestMessage();

     // This is here for initial testing purposes until we implement a dynamic question/answer work-flow. 
     private static List<Question> UnansweredQuestions = new List<Question>
    {
        new Question
        {
            Fact = "2 + 2",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "4",
                    Result = true
                },
                new Answer
                {
                    Response = "6",
                    Result = false
                },
                new Answer
                {
                    Response = "5",
                    Result = false
                }
            },
            Result = true
        },
        new Question
        {
            Fact = "10 + 10",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "19",
                    Result = false
                },
                new Answer
                {
                    Response = "20",
                    Result = true
                },
                new Answer
                {
                    Response = "21",
                    Result = false
                }
            },
            Result = false
        },
        new Question
        {
            Fact = "16 + 4",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "21",
                    Result = false
                },
                new Answer
                {
                    Response = "19",
                    Result = false
                },
                new Answer
                {
                    Response = "20",
                    Result = true
                }
            },
            Result = true
        },
        new Question
        {
            Fact = "25 + 10",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "34",
                    Result = false
                },
                new Answer
                {
                    Response = "35",
                    Result = true
                },
                new Answer
                {
                    Response = "36",
                    Result = false
                }
            },
            Result = false
        },
        new Question
        {
            Fact = "32 - 7",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "26",
                    Result = false
                },
                new Answer
                {
                    Response = "24",
                    Result = false
                },
                new Answer
                {
                    Response = "25",
                    Result = true
                }
            },
            Result = false
        },
        new Question
        {
            Fact = "23 - 4",
            Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "19",
                    Result = true
                },
                new Answer
                {
                    Response = "18",
                    Result = false
                },
                new Answer
                {
                    Response = "20",
                    Result = false
                }
            },
            Result = true
        },
        new Question
        {
            Fact = "13 + 3",
            Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "17",
                    Result = false
                },
                new Answer
                {
                    Response = "15",
                    Result = false
                },
                new Answer
                {
                    Response = "16",
                    Result = true
                }
            },
            Result = true
        },
        new Question
        {
            Fact = "14 + 10",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "23",
                    Result = false
                },
                new Answer
                {
                    Response = "24",
                    Result = true
                },
                new Answer
                {
                    Response = "25",
                    Result = false
                }
            },
            Result = false
        },
        new Question
        {
            Fact = "6 + 5",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "11",
                    Result = true
                },
                new Answer
                {
                    Response = "10",
                    Result = false
                },
                new Answer
                {
                    Response = "12",
                    Result = false
                }
            },
            Result = false
        },
        new Question
        {
            Fact = "19 - 4",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "14",
                    Result = false
                },
                new Answer
                {
                    Response = "15",
                    Result = true
                },
                new Answer
                {
                    Response = "16",
                    Result = false
                }
            },
            Result = true
        }
    };
     private Question CurrentQuestion;
     private readonly float TimeBetweenQuestions = 5;
     private Texture2D CorrectTexture;
     private Texture2D WrongTexture;
     [SerializeField]
     private RawImage FirstResponseImg;
     [SerializeField]
     private RawImage SecondResponseImg;
     [SerializeField]
     private RawImage ThirdResponseImg;
     [SerializeField]
     private Text FactText;
     [SerializeField]
     private Text FirstAnswerText;
     [SerializeField]
     private Text SecondAnswerText;
     [SerializeField]
     private Text ThirdAnswerText;
     [SerializeField]
     private Animator Animator;

     //Constructor
     void Awake()
     {
          var number = GetTestNumber();
          Debug.Log($"Number from Assembly: {number}");

          // We are not actually using this yet, just here to show the flow on strings.
          var message = Marshal.PtrToStringAnsi(GetTestMessage()); ;
          Debug.Log($"String from Assembly: {message}");

          CorrectTexture = Resources.Load<Texture2D>("Textures/Correct");
          WrongTexture = Resources.Load<Texture2D>("Textures/Wrong");
     }

     // Start is called before the first frame update
     void Start()
     {
          SetCurrentQuestion();
     }

     // Update is called once per frame
     void Update()
     {

     }

     public void UserSelectA()
     {
          if (CurrentQuestion.Answers[0].Result)
          {
               FirstResponseImg.texture = CorrectTexture;
               // Add score function. If correct add question score.
          }
          else
          {
               FirstResponseImg.texture = WrongTexture;
               // Add score function. If incorrect subtract half of question score.
          }

          Animator.SetTrigger("ButtonAClicked");
          StartCoroutine(TransitionToNextQuestion());
     }

     public void UserSelectB()
     {
          if (CurrentQuestion.Answers[1].Result)
          {
               SecondResponseImg.texture = CorrectTexture;
               // Add score function. If correct add question score.
          }
          else
          {
               SecondResponseImg.texture = WrongTexture;
               // Add score function. If incorrect subtract half of question score.
          }

          Animator.SetTrigger("ButtonBClicked");
          StartCoroutine(TransitionToNextQuestion());
     }

     public void UserSelectC()
     {
          if (CurrentQuestion.Answers[2].Result)
          {
               ThirdResponseImg.texture = CorrectTexture;
               // Add score function. If correct add question score.
          }
          else
          {
               ThirdResponseImg.texture = WrongTexture;
               // Add score function. If incorrect subtract half of question score.
          }

          Animator.SetTrigger("ButtonCClicked");
          StartCoroutine(TransitionToNextQuestion());
     }

     void SetCurrentQuestion()
     {
          int index = Random.Range(0, UnansweredQuestions.Count);
          CurrentQuestion = UnansweredQuestions[index];
          Debug.Log($"{CurrentQuestion.Fact}");
          FactText.text = CurrentQuestion.Fact;

          FirstAnswerText.text = $"a) {CurrentQuestion.Answers[0].Response}";
          SecondAnswerText.text = $"b) {CurrentQuestion.Answers[1].Response}";
          ThirdAnswerText.text = $"c) {CurrentQuestion.Answers[2].Response}";
     }

     IEnumerator TransitionToNextQuestion()
     {
          UnansweredQuestions.Remove(CurrentQuestion);
          yield return new WaitForSeconds(TimeBetweenQuestions);

          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }
}