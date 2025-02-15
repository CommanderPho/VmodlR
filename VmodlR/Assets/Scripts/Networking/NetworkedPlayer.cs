﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Voice.PUN;

/// <summary>
/// This class handles the interaction between the player prefab and the photon networking environment.
/// Its primary task is to disable all scripts that have to do with managing a local player like physics, input handling etc. 
/// if and only if this is not a local instance of the player prefab but a remote player. If this is the local player it does nothing
/// </summary>
[RequireComponent(typeof(PhotonView))]

public class NetworkedPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerUI;
    public GameObject UIHelpersGO;
    public GameObject CameraRigGO;
    public GameObject CenterEyeGO;
    public OVRTouchSample.Hand leftHand;
    public OVRTouchSample.Hand rightHand;

    public GameObject ThirdPersonBody;

    private PhotonVoiceView photonVoice;

    void Awake()
    {
        photonVoice = GetComponent<PhotonVoiceView>();

        //enable all needed components if this is the localy controlled player
        if (photonView.IsMine)
        {
            this.tag = TagUtils.localPlayerTag;

            CameraRigGO.GetComponent<OVRCameraRig>().enabled = true;
            CameraRigGO.GetComponent<OVRCameraRig>().disableEyeAnchorCameras = false;
            CameraRigGO.GetComponent<UnityEngine.EventSystems.OVRPhysicsRaycaster>().enabled = true;
            CenterEyeGO.AddComponent<AudioListener>();
            CenterEyeGO.GetComponent<Camera>().enabled = true;
            CenterEyeGO.GetComponent<OVRScreenFade>().enabled = true;

            //enable UI interaction for local players
            UIHelpersGO.SetActive(true);

            //enable camera rig classes
            CameraRigGO.GetComponent<OVRManager>().enabled = true;
            CameraRigGO.GetComponent<OVRHeadsetEmulator>().enabled = true;

            //enable hand controls
            enableHandControl(leftHand);
            enableHandControl(rightHand);

            CameraRigGO.SetActive(true);
            playerUI.SetActive(true);

            //enable player control classes
            GetComponent<CharacterController>().enabled = true;
            GetComponent<OVRPlayerController>().enabled = true;
            GetComponent<OVRDebugInfo>().enabled = true;
            GetComponent<CharacterControllerCameraConstraint>().enabled = true;

            ThirdPersonBody.SetActive(false);
        }
        else
        {
            //if it is a remote player, remove the OVRManager as it may only exist exactly once in a scene
            DestroyImmediate(CameraRigGO.GetComponent<OVRManager>());

            CameraRigGO.GetComponent<OVRCameraRig>().enabled = false;
            CameraRigGO.GetComponent<UnityEngine.EventSystems.OVRPhysicsRaycaster>().enabled = false;
            CenterEyeGO.GetComponent<Camera>().enabled = false;
            CenterEyeGO.GetComponent<OVRScreenFade>().enabled = false;

            //disable UI interaction for local players
            UIHelpersGO.SetActive(false);

            //disble camera rig classes
            CameraRigGO.GetComponent<OVRHeadsetEmulator>().enabled = false;

            playerUI.SetActive(false);

            //disable player control classes
            GetComponent<CharacterController>().enabled = false;
            GetComponent<OVRPlayerController>().enabled = false;
            GetComponent<OVRDebugInfo>().enabled = false;
            GetComponent<CharacterControllerCameraConstraint>().enabled = false;

            //enable the camera rig so the local player can still see the remote player's hands & body
            CameraRigGO.SetActive(true);
            ThirdPersonBody.SetActive(true);

            this.enabled = false;
        }
    }

    private void enableHandControl(OVRTouchSample.Hand hand)
    {
        hand.enabled = true;
        hand.gameObject.GetComponent<OVRGrabber>().enabled = true;
    }

    public override void OnJoinedRoom()
    {
        photonVoice.Init();
    }
}
