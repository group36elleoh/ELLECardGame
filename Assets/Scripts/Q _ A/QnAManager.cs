using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class QnAManager : MonoBehaviour
{
    public static List<attemptedAnswer> attemptedAnswers; // List of answers atempted
    public static List<question> currentQuestions;
    public static List<answer> currentAnswers;

    private static bool correct;
    public static int points = 0;

    private static SoundPlayer soundPlayer;
    private static CreditsManager creditsManager;
    private static MatchPopup matchPopup;

    private void Awake()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
        creditsManager = FindObjectOfType<CreditsManager>();
        matchPopup = FindObjectOfType<MatchPopup>();

        attemptedAnswers = new List<attemptedAnswer>();
        currentQuestions = new List<question>();
        currentAnswers = new List<answer>();

        creditsManager.currentStreak = 0;
        creditsManager.creditSalary = creditsManager.startingValue;
        creditsManager.totalCredits = PlayerPrefsManager.GetCredits();
        creditsManager.creditsEarned = 0;

        creditsManager.UpdateCreditDisplay();
    }

    public static bool answerQuestion(int questionID, int answerID)
    {
        //Debug.Log("Attempting to answer questionID " + questionID + " with answerID " + answerID);

        question q = CollectionManager.questionCollection[questionID];

        correct = q.answerQuestion(answerID);
        
        if (correct)
        {
            soundPlayer.PlayCorrectAnswerSFX();

            int creditsEarned = creditsManager.creditSalary;

            creditsManager.creditsEarned += creditsEarned;
            creditsManager.totalCredits += creditsEarned;

            creditsManager.currentStreak++;
            if ((creditsManager.currentStreak)%creditsManager.streakLength == 0)
            {
                creditsManager.creditSalary += creditsManager.incrementValue;
            }

            creditsManager.UpdateCreditDisplay();

            // matchPopup.ActivatePopup(q.getQuestionType(), q.getQuestionPhrase(), CollectionManager.answerCollection[answerID].getAnswerPhrase(), creditsEarned);
            matchPopup.ActivatePopup(q.getQuestionPhrase(), CollectionManager.answerCollection[answerID].getAnswerPhrase(), creditsEarned);

            if (GameManager.timeBwtweenQuestions > 4)
                GameManager.timeBwtweenQuestions -= .04f;
            else if (GameManager.timeBwtweenQuestions > 3)
                GameManager.timeBwtweenQuestions -= .02f;
            else if (GameManager.timeBwtweenQuestions >= 2)
                GameManager.timeBwtweenQuestions -= 0;

            HealthBar.currentPercentHP += .05f;

            points++;

            if (CollectionManager.moduleCollection[GameManager.currentModule.getID()].getPB() < points)
            {
                Debug.Log("NEW PERSONAL RECORD");
                CollectionManager.moduleCollection[GameManager.currentModule.getID()].setPB(points);
            }
        }
        else
        {
            soundPlayer.PlayWrongAnswerSFX();

            creditsManager.creditSalary = creditsManager.startingValue;
            creditsManager.currentStreak = 0;

            creditsManager.UpdateCreditDisplay();

            HealthBar.currentPercentHP -= .25f;
        }

        return correct;
    }
}
