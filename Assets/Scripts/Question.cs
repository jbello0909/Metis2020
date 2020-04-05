using Assets.Scripts;
using System;
using System.Collections.Generic;

[Serializable]
public class Question
{
     public string Fact; // Question text.
     public List<Answer> Answers;
     public int Score;
}