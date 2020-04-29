using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
     // Imports C++ DLL
     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern int GetTimeInSeconds();

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern IntPtr LoadCorrect();

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern IntPtr LoadWrong();

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern bool CheckAnswer(int a);

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
     public static extern IntPtr GetTrigger(char button);

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern int GetCount();

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern int Increment(int value);

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern int UpdateScoreInc(int score);

     [DllImport("UnmanagedCode", CallingConvention = CallingConvention.Cdecl)]
     public static extern int UpdateScoreDec(int score);

     // This is here for initial testing purposes until we implement a dynamic question/answer work-flow. 
     private static List<Question> UnansweredQuestions = new List<Question>
     {
        new Question
        {
            Fact = "Which is the largest ocean?",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "Pacific Ocean",
                    Result = 1
                },
                new Answer
                {
                    Response = "Atlantic Ocean",
                    Result = 0
                },
                new Answer
                {
                    Response = "Indian Ocean",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "When did World War II end?",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "1948",
                    Result = 0
                },
                new Answer
                {
                    Response = "1942",
                    Result = 0
                },
                new Answer
                {
                    Response = "1945",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "What is the hardest metal on Earth?",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "Chromium",
                    Result = 1
                },
                new Answer
                {
                    Response = "Iridium",
                    Result = 0
                },
                new Answer
                {
                    Response = "Titanium",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which of these is NOT a valid register?",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "HD",
                    Result = 1
                },
                new Answer
                {
                    Response = "EAX",
                    Result = 0
                },
                new Answer
                {
                    Response = "AH",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Who is the Chancellor of Indiana University Southeast?",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Response = "Dr. Roy Tibbs",
                    Result = 0
                },
                new Answer
                {
                    Response = "Dr. Victoria Watson",
                    Result = 0
                },
                new Answer
                {
                    Response = "Dr. Ray Wallace",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Which of these cities is NOT in Europe?",
            Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Prague",
                    Result = 0
                },
                new Answer
                {
                    Response = "Tyre",
                    Result = 1
                },
                new Answer
                {
                    Response = "Reykjavík",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "What is the architectural style of Notre Dame Cathedral in Paris?",
            Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Gothic",
                    Result = 1
                },
                new Answer
                {
                    Response = "Baroque",
                    Result = 0
                },
                new Answer
                {
                    Response = "Romanesque",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which Turkish sultan conquered Constantinople?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Murad I",
                    Result = 0
                },
                new Answer
                {
                    Response = "Osman III",
                    Result = 0
                },
                new Answer
                {
                    Response = "Mehmed II",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "How many toes does a hippopotamus have?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "20",
                    Result = 0
                },
                new Answer
                {
                    Response = "16",
                    Result = 1
                },
                new Answer
                {
                    Response = "24",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "What was the name of the original drummer in the Beatles?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Robert Plant",
                    Result = 0
                },
                new Answer
                {
                    Response = "Brian Jones",
                    Result = 0
                },
                new Answer
                {
                    Response = "Pete Best",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Which country does not have a red, white, and blue flag?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Lithuania",
                    Result = 1
                },
                new Answer
                {
                    Response = "Laos",
                    Result = 0
                },
                new Answer
                {
                    Response = "Slovenia",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "On 'The Office', what is Dwight Schrute's secondary occupation?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Bear Hunter",
                    Result = 0
                },
                new Answer
                {
                    Response = "Recyclops",
                    Result = 0
                },
                new Answer
                {
                    Response = "Beet Farmer",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Which element has the highest atomic number?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Oganesson",
                    Result = 1
                },
                new Answer
                {
                    Response = "Yttrium",
                    Result = 0
                },
                new Answer
                {
                    Response = "Thallium",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which of these animals is not a mammal?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Sea Lion",
                    Result = 0
                },
                new Answer
                {
                    Response = "Penguin",
                    Result = 1
                },
                new Answer
                {
                    Response = "Dolphin",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which artist has never won a Grammy award?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Lizzo",
                    Result = 0
                },
                new Answer
                {
                    Response = "Katy Perry",
                    Result = 1
                },
                new Answer
                {
                    Response = "Billie Eilish",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "What type of diet allows fish but no other meat?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Pescetarian",
                    Result = 1
                },
                new Answer
                {
                    Response = "Pictotarian",
                    Result = 0
                },
                new Answer
                {
                    Response = "Lactotarian",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Where is the capital of Brazil?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Sao Paulo",
                    Result = 0
                },
                new Answer
                {
                    Response = "Salvador",
                    Result = 0
                },
                new Answer
                {
                    Response = "Brasilia",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Which is the highest honor?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Vectordictorian",
                    Result = 0
                },
                new Answer
                {
                    Response = "Valedictorian",
                    Result = 1
                },
                new Answer
                {
                    Response = "Salutatorian",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "What year was the first Thunder Over Louisville held?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "1998",
                    Result = 0
                },
                new Answer
                {
                    Response = "1987",
                    Result = 0
                },
                new Answer
                {
                    Response = "1990",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Who is the leader of S.H.I.E.L.D.?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Nick Fury",
                    Result = 1
                },
                new Answer
                {
                    Response = "Bruce Banner",
                    Result = 0
                },
                new Answer
                {
                    Response = "Steve Rogers",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which is the highest poker hand?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Four of a Kind",
                    Result = 0
                },
                new Answer
                {
                    Response = "Full House",
                    Result = 0
                },
                new Answer
                {
                    Response = "Royal Flush",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Which is the longest mountain range?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Appalachians",
                    Result = 0
                },
                new Answer
                {
                    Response = "Andes",
                    Result = 1
                },
                new Answer
                {
                    Response = "Himalayas",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "The co-founder of which app married Serena Williams?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Reddit",
                    Result = 1
                },
                new Answer
                {
                    Response = "Twitter",
                    Result = 0
                },
                new Answer
                {
                    Response = "LinkedIn",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which is NOT a real fruit?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Tangeline",
                    Result = 1
                },
                new Answer
                {
                    Response = "Cherimoya",
                    Result = 0
                },
                new Answer
                {
                    Response = "Ackee",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "What is the term for half of a byte?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "byttle",
                    Result = 0
                },
                new Answer
                {
                    Response = "niblet",
                    Result = 0
                },
                new Answer
                {
                    Response = "nibble",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "How many IU campuses are there?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "9",
                    Result = 1
                },
                new Answer
                {
                    Response = "7",
                    Result = 0
                },
                new Answer
                {
                    Response = "6",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which is the name of a Greek god?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Hyteus",
                    Result = 0
                },
                new Answer
                {
                    Response = "Pollux",
                    Result = 1
                },
                new Answer
                {
                    Response = "Opatia",
                    Result = 0
                }
            },
        },
        new Question
        {
            Fact = "Which is the best selling video game?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Call of Duty",
                    Result = 0
                },
                new Answer
                {
                    Response = "Tetris",
                    Result = 0
                },
                new Answer
                {
                    Response = "Pokemon",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "How many justices sit on the US Supreme Court?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "12",
                    Result = 0
                },
                new Answer
                {
                    Response = "7",
                    Result = 0
                },
                new Answer
                {
                    Response = "9",
                    Result = 1
                }
            },
        },
        new Question
        {
            Fact = "Who was the creator of The Last Supper painting?",
             Answers = new List<Answer>
              {
                new Answer
                {
                    Response = "Michelangelo",
                    Result = 0
                },
                new Answer
                {
                    Response = "Leonardo da Vinci",
                    Result = 1
                },
                new Answer
                {
                    Response = "Vincent Van Gogh",
                    Result = 0
                }
            },
        }
    };
     private Question CurrentQuestion;
     private float TimeBetweenQuestions;
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
     public static int scoreValue;
     public Text score;
     public static int count = 0;
     public Text counter;
     public AudioSource[] sounds;
     public AudioSource QuestionAudio;
     public AudioSource CorrectAudio;
     public AudioSource WrongAudio;

     //Constructor
     void Awake()
     {
          TimeBetweenQuestions = GetTimeInSeconds();
          Debug.Log($"Time between questions: {TimeBetweenQuestions}");

          string correct = Marshal.PtrToStringAnsi(LoadCorrect());
          string wrong = Marshal.PtrToStringAnsi(LoadWrong());
          Debug.Log($"Correct Text: {correct}");
          Debug.Log($"Wrong Text: {wrong}");

          CorrectTexture = Resources.Load<Texture2D>(correct);
          WrongTexture = Resources.Load<Texture2D>(wrong);
     }

     // Start is called before the first frame update
     void Start()
     {
          SetCurrentQuestion();

          sounds = GetComponents<AudioSource>();
          QuestionAudio = sounds[0];
          CorrectAudio = sounds[1];
          WrongAudio = sounds[2];
          QuestionAudio.Play();
     }

     // Update is called once per frame
     void Update()
     {
          if (score)
          {
               score.text = "Score: " + scoreValue;
          }

          if (counter)
          {
               counter.text = (count) + " of 15";
          }
     }

     public void UserSelectA()
     {
          QuestionAudio.Stop();

          if (CheckAnswer(CurrentQuestion.Answers[0].Result))
          {
               CorrectAudio.Play();
               FirstResponseImg.texture = CorrectTexture;
               scoreValue = UpdateScoreInc(scoreValue);
          }
          else
          {
               WrongAudio.Play();
               FirstResponseImg.texture = WrongTexture;
               scoreValue = UpdateScoreDec(scoreValue);
          }

          Animator.SetTrigger(Marshal.PtrToStringAnsi(GetTrigger('A')));
          StartCoroutine(TransitionToNextQuestion());
     }

     public void UserSelectB()
     {
          QuestionAudio.Stop();

          if (CheckAnswer(CurrentQuestion.Answers[1].Result))
          {
               CorrectAudio.Play();
               SecondResponseImg.texture = CorrectTexture;
               scoreValue = UpdateScoreInc(scoreValue);
          }
          else
          {
               WrongAudio.Play();
               SecondResponseImg.texture = WrongTexture;
               scoreValue = UpdateScoreDec(scoreValue);
          }

          Animator.SetTrigger(Marshal.PtrToStringAnsi(GetTrigger('B')));
          StartCoroutine(TransitionToNextQuestion());
     }

     public void UserSelectC()
     {
          QuestionAudio.Stop();

          if (CheckAnswer(CurrentQuestion.Answers[2].Result))
          {
               CorrectAudio.Play();
               ThirdResponseImg.texture = CorrectTexture;
               scoreValue = UpdateScoreInc(scoreValue);
          }
          else
          {
               WrongAudio.Play();
               ThirdResponseImg.texture = WrongTexture;
               scoreValue = UpdateScoreDec(scoreValue);
          }

          Animator.SetTrigger(Marshal.PtrToStringAnsi(GetTrigger('C')));
          StartCoroutine(TransitionToNextQuestion());
     }

     void SetCurrentQuestion()
     {
          int index = Random.Range(0, UnansweredQuestions.Count);
          CurrentQuestion = UnansweredQuestions[index];
          Debug.Log($"{CurrentQuestion.Fact}");

          if (FactText)
          {
               FactText.text = CurrentQuestion.Fact;
          }

          if (FirstAnswerText)
          {
               FirstAnswerText.text = $"a) {CurrentQuestion.Answers[0].Response}";
          }

          if (SecondAnswerText)
          {
               SecondAnswerText.text = $"b) {CurrentQuestion.Answers[1].Response}";
          }

          if (ThirdAnswerText)
          {
               ThirdAnswerText.text = $"c) {CurrentQuestion.Answers[2].Response}";
          }
     }

     IEnumerator TransitionToNextQuestion()
     {
          count = Increment(count);

          UnansweredQuestions.Remove(CurrentQuestion);
          yield return new WaitForSeconds(TimeBetweenQuestions);
          if (count < 15)
          {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          }
          else
          {
               SceneManager.LoadScene("EndScreen");
               count = 0;
          }
     }
}