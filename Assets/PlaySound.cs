
using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

namespace Vuforia
{

    /// <summary>
    ///     A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class PlaySound : MonoBehaviour, ITrackableEventHandler
    {

        public FoundPair FPair;

        //------------Begin Sound----------
        public AudioSource soundTarget;
        public AudioClip clipTarget;
        private AudioSource[] allAudioSources;

        //function to stop all sounds_Nikita
        void StopAllAudio()
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
        }

        //function to play sound _ Níkita
        void playSound(string soundString)
        {
            clipTarget = (AudioClip)Resources.Load(soundString);
            soundTarget.clip = clipTarget;
            soundTarget.loop = false;
            soundTarget.playOnAwake = false;
            soundTarget.Play();

        }

        //-----------End Sound------------

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            FPair = FindObjectOfType<FoundPair>();
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

            //Register / add the AudioSource as object
            soundTarget = (AudioSource)gameObject.AddComponent<AudioSource>();

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS

        /// <summary>
        ///     Implementation of the ITrackableEventHandler function called when the
        ///     tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
            TrackableBehaviour.Status previousStatus,
            TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
                OnTrackingFound();
            }
            else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                     newStatus == TrackableBehaviour.Status.NO_POSE)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
                OnTrackingLost();
            }
            else
            {
                // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
                // Vuforia is starting, but tracking has not been lost or found yet
                // Call OnTrackingLost() to hide the augmentations
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS

        private void OnTrackingFound()
        {
            var rendererComponents = GetComponentsInChildren<Renderer>(true);
            var colliderComponents = GetComponentsInChildren<Collider>(true);
            var canvasComponents = GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
            {
                component.enabled = true;
            }
            // Enable colliders:
            foreach (var component in colliderComponents)
            {
                component.enabled = true;
            }
            // Enable canvas':
            foreach (var component in canvasComponents)
            {
                component.enabled = true;
            }

            //Play Sound, IF detect an Imagetarget
            foundImage_playSound();

        }

        // Method to play Sound, IF an Imagetarget the picture is detected _ Nikita
        public void foundImage_playSound()
        {

            if (mTrackableBehaviour.TrackableName == "bird1" || mTrackableBehaviour.TrackableName == "bird2" ||
                mTrackableBehaviour.TrackableName == "bird1" && mTrackableBehaviour.TrackableName == "bird2")
            {
                playSound("sounds/blueJay");

            }

            if (mTrackableBehaviour.TrackableName == "Butterfly1" || mTrackableBehaviour.TrackableName == "Butterfly2" ||
                mTrackableBehaviour.TrackableName == "Butterfly1" && mTrackableBehaviour.TrackableName == "Butterfly2")
            {
                playSound("sounds/Butterfly");
                soundTarget.loop = true;

            }

            if (mTrackableBehaviour.TrackableName == "gwcat1" || mTrackableBehaviour.TrackableName == "gwcat2"
                || mTrackableBehaviour.TrackableName == "gwcat1" && mTrackableBehaviour.TrackableName == "gwcat2")
            {
                playSound("sounds/Die Meows 1");
            }

            if (mTrackableBehaviour.TrackableName == "corgibrown1" || mTrackableBehaviour.TrackableName == "corgibrown2" ||
                mTrackableBehaviour.TrackableName == "corgibrown1" && mTrackableBehaviour.TrackableName == "corgibrown2")
            {
                playSound("sounds/Dogs Bark #4");
            }

            if (mTrackableBehaviour.TrackableName == "blackcat1" || mTrackableBehaviour.TrackableName == "blackcat2" ||
                mTrackableBehaviour.TrackableName == "blackcat1" && mTrackableBehaviour.TrackableName == "blackcat2")
            {
                playSound("sounds/Meows #6");
            }

            if (mTrackableBehaviour.TrackableName == "gcat1" || mTrackableBehaviour.TrackableName == "gcat2"
                || mTrackableBehaviour.TrackableName == "gcat1" && mTrackableBehaviour.TrackableName == "gcat2")
            {
                playSound("sounds/Meows #4");
            }

            if (mTrackableBehaviour.TrackableName == "husky1" || mTrackableBehaviour.TrackableName == "husky2" ||
                mTrackableBehaviour.TrackableName == "husky1" && mTrackableBehaviour.TrackableName == "husky2")
            {
                playSound("sounds/Dogs Bark #6");
            }

            if (mTrackableBehaviour.TrackableName == "corgiwhite1" || mTrackableBehaviour.TrackableName == "corgiwhite2" ||
                mTrackableBehaviour.TrackableName == "corgiwhite1" && mTrackableBehaviour.TrackableName == "corgiwhite2")
            {
                playSound("sounds/Dogs Bark #6");
            }

            if (mTrackableBehaviour.TrackableName == "whale1" || mTrackableBehaviour.TrackableName == "whale2" ||
                mTrackableBehaviour.TrackableName == "wha1e1" && mTrackableBehaviour.TrackableName == "whale2")
            {
                playSound("sounds/Whale #2");
            }

            if (mTrackableBehaviour.TrackableName == "lizard1" || mTrackableBehaviour.TrackableName == "lizard2" ||
                mTrackableBehaviour.TrackableName == "lizard1" && mTrackableBehaviour.TrackableName == "lizard2")
            {
                playSound("sounds/lizard #2");
            }

            FPair.FoundImage();
        }


        private void OnTrackingLost()
        {
            var rendererComponents = GetComponentsInChildren<Renderer>(true);
            var colliderComponents = GetComponentsInChildren<Collider>(true);
            var canvasComponents = GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
            {
                component.enabled = false;
            }
            // Disable colliders:
            foreach (var component in colliderComponents)
            {
                component.enabled = false;
            }
            // Disable canvas':
            foreach (var component in canvasComponents)
            {
                component.enabled = false;
            }
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

            //Stop All Sounds if Target Lost
            StopAllAudio();

            FPair.LostImage();
        }

        #endregion // PRIVATE_METHODS
    }
}