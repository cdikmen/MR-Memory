using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Vuforia;
using System.Linq;

public class FoundPair : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip soundS;
    private TrackableBehaviour mTrackableBehaviour;
    private int Count;
    public string target1;
    public string target2;
    private AudioSource currentAudio;
    public AudioSource rightAudio;
    public string tempPart1;
    public string tempPart2;
    public int NumberOfCurrentTargets;
    public bool Winning;
    public bool MatchFound = false;
    public float Timer1;
    public float FinishTimer = 0f;
    public bool needTimer = false;
    public TextMesh WinningText;
    public AudioSource WinningSound;
    public AudioClip WinningClip;
    public int NumberOfCurrentSounds;
    public float SoundTimer = 0f;
    public bool bPlayingWinningSound = false;
    public float MaxPlaySoundTimer = 3f;


    void Start()
    {
        

    }
    
    // TODO: FIND IMAGE

    public void FoundImage()
    {
        needTimer = false;
        Timer1 = 0f;
    }

    // TODO: LOST IMAGE TIMER

    public void LostImage()
    {
        Debug.Log("Lost Image");
        MatchFound = false;
        needTimer = true;
        Timer1 = 0f;
    }

    // RESET all

    public void Reset()
    {
        target1 = null;
        target2 = null;
        tempPart1 = null;
        tempPart2 = null;
        MatchFound = false;
    }

    // Timer for LostImage
    
    public void Timer()
    {
        if(Timer1 > 5f)
        {
            Reset();
            needTimer = false;
            Timer1 = 0f;
        }
    }

    // TODO: FinishTimer

    public void F_finishTimer()
    {
        if (FinishTimer > 3f)
        {
            WinningSound.Stop();
            FinishTimer = 0f;
            WinningText.GetComponentInChildren<Renderer>().enabled = false;
            Winning = false;
        }
    }

    // WIN!

    public void F_Winning()
    {
        //GO GO WIN!
        MatchFound = true;
        Debug.Log("Yeahaaa we got a match!");
        WinningText.GetComponentInChildren<Renderer>().enabled = true;
        Winning = true;

    }
    
    //TODO: Winning Sound
    public void S_Winning()
    {
           
        if(!bPlayingWinningSound)
        {
            WinningSound.PlayOneShot(WinningClip);
            Debug.Log(WinningSound);
            bPlayingWinningSound = true;
        }
        
    }
    
    // TODO: if pictures are found, set win, if lost, have a timer to find back the image or 2nd image to get to win.
    
    void Update()
    {
        if (bPlayingWinningSound)
        {
            SoundTimer += Time.deltaTime;
            if (SoundTimer >= MaxPlaySoundTimer)
            {
                bPlayingWinningSound = false;
                SoundTimer = 0f;
            }
        }
        if (Winning)
        {
            FinishTimer += Time.deltaTime;
        }

        if (needTimer)
        {
            Timer1 += Time.deltaTime;
        }
        Timer();
        F_finishTimer();
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        NumberOfCurrentTargets = activeTrackables.Count();
        NumberOfCurrentSounds = activeTrackables.Count();

        // Iterate through the list of active trackables
        Debug.Log("List of trackables currently active (tracked): ");
        foreach (TrackableBehaviour tb in activeTrackables)
        {
        
            // REgex IsMatch if image found with 1 set it to target 1 or 2 set to target2
            
            if (Regex.IsMatch(tb.TrackableName, "1", RegexOptions.IgnoreCase))
            {
                target1 = tb.TrackableName;
            }
            else
            {
                target2 = tb.TrackableName;
            }
            
            // put target1 to temppart1 or target2 to temppart 2 and delete the 1 and 2 in name.

            if (target1 != null && target2 != null)
            {
                tempPart1 = Regex.Replace(target1, "1", "");
                tempPart2 = Regex.Replace(target2, "2", "");

                // check if names are same then win

                if (tempPart1 == tempPart2)
                {
                    F_Winning();
                    S_Winning();

                }
            }
        }
        if (NumberOfCurrentTargets == 0)
        {

        }
    }
}
