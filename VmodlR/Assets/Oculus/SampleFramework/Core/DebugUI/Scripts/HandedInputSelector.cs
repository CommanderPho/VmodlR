/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided �AS IS� WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class HandedInputSelector : MonoBehaviour
{
    OVRCameraRig m_CameraRig;
    OVRInputModule m_InputModule;

    void Start()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        m_InputModule = FindObjectOfType<OVRInputModule>();
    }

    void Update()
    {
        if(OVRInput.GetActiveController() == OVRInput.Controller.RTouch)
        {
            SetActiveController(OVRInput.Controller.RTouch);
        }
        else
        {
            SetActiveController(OVRInput.Controller.LTouch);
        }

    }

    void SetActiveController(OVRInput.Controller c)
    {
        Transform t;
        if(c == OVRInput.Controller.LTouch)
        {
            t = m_CameraRig.leftHandAnchor;
            m_InputModule.rayTransform = t;
        }
        else
        {
            //use predefined raytransform for right controller
            //t = m_CameraRig.rightHandAnchor;
        }
    }
}
