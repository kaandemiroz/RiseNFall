/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class TrackableEventHandler : MonoBehaviour,
        ITrackableEventHandler {

        #region PUBLIC_MEMBER_VARIABLES

        public int Speed = 50;
        public int upperBound = 10;
        public int lowerBound = 3;

        #endregion //PUBLIC_MEMBER VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private bool started = false;
        private bool lost = false;
        private int id;
        private GameObject nearbyCubeLeft = null;
        private GameObject nearbyCubeRight = null;
        private GameObject nearbyCubeUp = null;
        private GameObject nearbyCubeDown = null;
        private GameObject nearbyCubeNE = null;
        private GameObject nearbyCubeSE = null;
        private GameObject nearbyCubeNW = null;
        private GameObject nearbyCubeSW = null;
        private GameObject[] objects;
        private bool[] rising;
        private bool[] falling;
        private int nRows = 3;
        private int nCols = 4;
        private int nObjects;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start(){
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

            nObjects = nRows * nCols;
            objects = new GameObject[] { nearbyCubeLeft, nearbyCubeRight, nearbyCubeUp, nearbyCubeDown, nearbyCubeNE, nearbyCubeSE, nearbyCubeNW, nearbyCubeSW };
            rising = new bool[] { false, false, false, false, false, false, false, false };
            falling = new bool[] { false, false, false, false, false, false, false, false };
        }

        void Update(){
            for (int i = 0; i < 8; i++){
                if (rising[i]) raiseObject(i);
                else if (falling[i]) lowerObject(i);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS


        #region PUBLIC_METHODS

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


        private void raiseObject(int objectIndex) {
            Transform transform = objects[objectIndex].transform;
            if (transform.GetChild(0).position.y < transform.position.y + upperBound){
                objects[objectIndex].transform.GetChild(0).Translate(Vector3.up * Speed * Time.deltaTime);
            }else rising[objectIndex] = false;
        }

        private void lowerObject(int objectIndex) {
            Transform transform = objects[objectIndex].transform;
            if (transform.GetChild(0).position.y > transform.position.y + lowerBound){
                objects[objectIndex].transform.GetChild(0).Translate(Vector3.down * Speed * Time.deltaTime);
            }else falling[objectIndex] = false;
        }


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            int id = int.Parse(this.name.Substring(this.name.LastIndexOf('r') + 1));

            nearbyCubeLeft = GameObject.Find("FrameMarker" + (id - 1));
            nearbyCubeRight = GameObject.Find("FrameMarker" + (id + 1));
            nearbyCubeUp = GameObject.Find("FrameMarker" + (id - nCols));
            nearbyCubeDown = GameObject.Find("FrameMarker" + (id + nCols));
            nearbyCubeNE = GameObject.Find("FrameMarker" + (id - nCols + 1));
            nearbyCubeSE = GameObject.Find("FrameMarker" + (id + nCols + 1));
            nearbyCubeNW = GameObject.Find("FrameMarker" + (id - nCols - 1));
            nearbyCubeSW = GameObject.Find("FrameMarker" + (id + nCols - 1));

            if (started && lost){

                if (nearbyCubeLeft != null && id % nCols != 0){
                    objects[0].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[0].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[0] = false;
                    falling[0] = true;
                }
                if (nearbyCubeRight != null && (id + 1) % nCols != 0) {
                    objects[1].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[1].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[1] = false;
                    falling[1] = true;
                }
                if (nearbyCubeUp != null && id > 0) {
                    objects[2].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[2].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[2] = false;
                    falling[2] = true;
                }
                if (nearbyCubeDown != null && id < nObjects) {
                    objects[3].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[3].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[3] = false;
                    falling[3] = true;
                }
                if (nearbyCubeNE != null && id > 0 && (id + 1) % nCols != 0) {
                    objects[4].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[4].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[4] = false;
                    falling[4] = true;
                }
                if (nearbyCubeSE != null && id < nObjects && (id + 1) % nCols != 0) {
                    objects[5].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[5].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[5] = false;
                    falling[5] = true;
                }
                if (nearbyCubeNW != null && id > 0 && id % nCols != 0) {
                    objects[6].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[6].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[6] = false;
                    falling[6] = true;
                }
                if (nearbyCubeSW != null && id < nObjects && id % nCols != 0) {
                    objects[7].GetComponent<TrackableEventHandler>().upperBound -= 10;
                    //objects[7].GetComponent<TrackableEventHandler>().lowerBound -= 10;
                    rising[7] = false;
                    falling[7] = true;
                }
                lost = false;
            }

            started = true;

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            string idStr = this.name.Substring(this.name.LastIndexOf('r') + 1);
            int id = int.Parse(idStr);

            nearbyCubeLeft = GameObject.Find("FrameMarker" + (id - 1));
            nearbyCubeRight = GameObject.Find("FrameMarker" + (id + 1));
            nearbyCubeUp = GameObject.Find("FrameMarker" + (id - nCols));
            nearbyCubeDown = GameObject.Find("FrameMarker" + (id + nCols));
            nearbyCubeNE = GameObject.Find("FrameMarker" + (id - nCols + 1));
            nearbyCubeSE = GameObject.Find("FrameMarker" + (id + nCols + 1));
            nearbyCubeNW = GameObject.Find("FrameMarker" + (id - nCols - 1));
            nearbyCubeSW = GameObject.Find("FrameMarker" + (id + nCols - 1));

            if (started){
                if (GetComponent<EnemyCapsule>().enabled) Application.LoadLevel("gameover");
                if (nearbyCubeLeft != null && id % nCols != 0) {
                    objects[0].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[0].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[0] = true;
                    falling[0] = false;
                }
                if (nearbyCubeRight != null && (id + 1) % nCols != 0) {
                    objects[1].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[1].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[1] = true;
                    falling[1] = false;
                }
                if (nearbyCubeUp != null && id > 0) {
                    objects[2].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[2].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[2] = true;
                    falling[2] = false;
                }
                if (nearbyCubeDown != null && id < nObjects) {
                    objects[3].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[3].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[3] = true;
                    falling[3] = false;
                }
                if (nearbyCubeNE != null && id > 0 && (id + 1) % nCols != 0) {
                    objects[4].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[4].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[4] = true;
                    falling[4] = false;
                }
                if (nearbyCubeSE != null && id < nObjects && (id + 1) % nCols != 0) {
                    objects[5].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[5].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[5] = true;
                    falling[5] = false;
                }
                if (nearbyCubeNW != null && id > 0 && id % nCols != 0) {
                    objects[6].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[6].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[6] = true;
                    falling[6] = false;
                }
                if (nearbyCubeSW != null && id < nObjects && id % nCols != 0) {
                    objects[7].GetComponent<TrackableEventHandler>().upperBound += 10;
                    //objects[7].GetComponent<TrackableEventHandler>().lowerBound += 10;
                    rising[7] = true;
                    falling[7] = false;
                }
                lost = true;
            }

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;

            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
