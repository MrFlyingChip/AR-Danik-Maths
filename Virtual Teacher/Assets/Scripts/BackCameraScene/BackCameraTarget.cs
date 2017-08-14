
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{
    public class BackCameraTarget : MonoBehaviour,
                                                ITrackableEventHandler
    {

        /// <summary>
        /// A custom handler that implements the ITrackableEventHandler interface.
        /// </summary>


        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        public BackCameraController cardNumberController;
        public int cardID;
        public GameObject child;
        public GameObject[] children;
        public GameObject warning;
        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            warning.SetActive(false);
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            //// Enable rendering:
            //foreach (Renderer component in rendererComponents)
            //{
            //    component.enabled = true;
            //}

            //// Enable colliders:
            //foreach (Collider component in colliderComponents)
            //{
            //    component.enabled = true;
            //}
            cardNumberController.RegisterID(cardID, gameObject);

            if (cardNumberController.images.Count == 1)
            {
                if (gameObject == cardNumberController.images[0])
                {
                    if (cardNumberController.animated)
                    {
                        child.transform.localScale = new Vector3(10f, 10f, 10f);
                        cardNumberController.Visualize();

                    }
                }
            }
            else
            {
                if (gameObject == cardNumberController.images[0] || gameObject == cardNumberController.images[1])
                {
                    if (cardNumberController.animated)
                    {
                        child.transform.localScale = new Vector3(10f, 10f, 10f);
                        cardNumberController.Visualize();

                    }
                }
            }
            
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            warning.SetActive(true);
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            //// Disable rendering:
            //foreach (Renderer component in rendererComponents)
            //{
            //    component.enabled = false;
            //}

            //// Disable colliders:
            //foreach (Collider component in colliderComponents)
            //{
            //    component.enabled = false;
            //}

            if(child != null)
            {
                
                child.GetComponent<BallAnimation>().HideBall();

            }
            GameObject.Find("SoundManager").GetComponent<AudioSource>().Stop();
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}



