using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.SceneManagement
{
    [RequireComponent(typeof(SavingSystem))]
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        SavingSystem saveSystem;
        Fader fader;

        private void Awake()
        {
            saveSystem = GetComponent<SavingSystem>();
            fader = FindObjectOfType<Fader>();
            fader.SetAlpha(1);
        }

        IEnumerator Start()
        {
            yield return saveSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(1f);
        }

        public void Save()
        {
            saveSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            saveSystem.Load(defaultSaveFile);
        }

        public void QuickSave(InputAction.CallbackContext ctx)
        {
            print("quickSave");
            if (ctx.performed)
            {
                print("quickSave");
                Save();
            }
            
        }
        public void QuickLoad(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                print("quickLoad");
                Load();
            }
            

        }
    }

}