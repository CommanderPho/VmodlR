﻿namespace Photon.Voice.Unity.Demos
{
    //disable warnings for private (serialized) fields not being assigned to
    #pragma warning disable 649

    using UnityEngine.UI;
    using UnityEngine;

    public class SidebarToggle : MonoBehaviour
    {
        [SerializeField]
        private Button sidebarButton;
        
        [SerializeField]
        private RectTransform panelsHolder;
        private float sidebarWidth = 300f; // todo: get width dynamically at runtime

        private bool sidebarOpen = true;

        private void Awake()
        {
            this.sidebarButton.onClick.RemoveAllListeners();
            this.sidebarButton.onClick.AddListener(this.ToggleSidebar);
            this.ToggleSidebar(this.sidebarOpen);
        }

        [ContextMenu("ToggleSidebar")]
        private void ToggleSidebar()
        {
            this.sidebarOpen = !this.sidebarOpen;
            this.ToggleSidebar(this.sidebarOpen);
        }

        private void ToggleSidebar(bool open)
        {
            if (!open)
            {
                //this.panelsHolder.SetLeft(0);
                this.panelsHolder.SetPosX(0);
            }
            else
            {
                //this.panelsHolder.SetLeft(this.sidebarWidth);
                this.panelsHolder.SetPosX(this.sidebarWidth);
            }
        }
    }
}
