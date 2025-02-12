﻿using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using SmartFPController;
using In_Silence.game_objects;

namespace In_Silence
{
    class Hacks : MonoBehaviour
    {
        public static Camera MainCamera = null;
        public static float Timer = 0f;

        public static List<Item> eItem = new List<Item>();
        public static List<ActivableItem> eActivableItem = new List<ActivableItem>();
        public static List<ArmoryDoor> eArmoryDoor = new List<ArmoryDoor>();
        public static List<BearTrap> eBearTrap = new List<BearTrap>();
        public static List<Crow> eCrow = new List<Crow>();
        public static List<CrowFlock> eCrowFlock = new List<CrowFlock>();
        public static List<DeadBodyController> eDeadBodies = new List<DeadBodyController>();
        public static List<DogHouse> eDogHouse = new List<DogHouse>();
        public static List<GroundItem> eGroundItem = new List<GroundItem>();
        public static List<JackInBox> eJackInBox = new List<JackInBox>();
        public static List<PlayableCharacter> ePlayableCharacter = new List<PlayableCharacter>();
        public static List<PuzzleButton> ePuzzleButton = new List<PuzzleButton>();
        public static List<PuzzleCabin> ePuzzleCabin = new List<PuzzleCabin>();
        public static List<PuzzleKey> ePuzzleKey = new List<PuzzleKey>();
        public static List<PuzzleTablet> ePuzzleTablet = new List<PuzzleTablet>();
        public static List<Radio> eRadio = new List<Radio>();
        public static List<RabbitController> eRabbitController = new List<RabbitController>();
        public static List<Rifle> eRifle = new List<Rifle>();
        public static List<RifleItem> eRifleItem = new List<RifleItem>();
        public static List<Rocket> eRocket = new List<Rocket>();
        public static List<Stone> eStone = new List<Stone>();
        public static List<StoneThrower> eStoneThrower = new List<StoneThrower>();
        public static List<Telephone> eTelephone = new List<Telephone>();
        public static List<Toy> eToy = new List<Toy>();
        public static List<TruckPart> eTruckPart = new List<TruckPart>();
        public static List<Witch> eWitch = new List<Witch>();
        public static List<Television> eTelevision = new List<Television>();
        public static List<CreatureAIManager> eCreature = new List<CreatureAIManager>();
        public static List<BodyAnimator> eBodyPlayer = new List<BodyAnimator>();
        
        public static List<Photon.Realtime.Player> ePlayers = new List<Photon.Realtime.Player>();

        private static bool runEndOfGame = false;
        public static string dbg = "";
        public void Start()
        {
            
        }
        

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.End)) // Kill hacks on "End" key pressed
            {
                Loader.unload();
            }
            if (Input.GetKeyDown(KeyCode.Insert)) // Kill hacks on "End" key pressed
            {
                modules.UI.main_menu = !modules.UI.main_menu;
            }
            if (Input.GetKey(KeyCode.Mouse1) && modules.UI.t_Aimbot)
            {
                modules.Aimbot.AimAssist();
            }
            
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (game_objects.in_silence.Vehicle != null)
                {
                    foreach (TruckPart p in game_objects.in_silence.Vehicle.missingParts)
                    {
                        game_objects.in_silence.Vehicle.RepairPart(p.partID);
                    }
                }
            }

            if (Input.GetKey(KeyCode.Keypad8))
            {
                foreach (PlayableCharacter entity in Hacks.ePlayableCharacter)
                {
                    if (entity != null)
                    {
                        entity.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y + 10f, entity.transform.position.z);
                        if (Hacks.MainCamera.WorldToScreenPoint(entity.transform.position).z > 0f && Vector3.Distance(entity.transform.position, Hacks.MainCamera.transform.position) <= 0.5f)
                        {
                            entity.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y + 10f, entity.transform.position.z);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                foreach (ActivableItem entity in Hacks.eActivableItem)
                {
                    if (entity != null)
                    {
                        entity.Activate();
                    }
                }
            }

            // 5 Second timer to loop entities and objects to return to lists
            Timer += Time.deltaTime; 
            if (Timer >= 5f) 
            {
                Timer = 0f;
                
                MainCamera = Camera.main;
                updateLists();
                if (!WorldManager.isGameFinished)
                {
                    game_objects.in_silence.update();
                }
                
            }

            if (WorldManager.isGameFinished)
            {
                if (runEndOfGame != true)
                {
                    nullItems();
                    runEndOfGame = true;
                }
            }
            else
            {
                runEndOfGame = false;
            }

        }

        public void OnGUI()
        {
            modules.ESP.performESP();
            modules.UI.displayUI();
            GUI.Label(new Rect(0, 0, (float)Screen.width, 50f), dbg);
        }

        
        private static void updateLists()
        {
            eItem = UnityEngine.GameObject.FindObjectsOfType<Item>().ToList();
            eActivableItem = UnityEngine.GameObject.FindObjectsOfType<ActivableItem>().ToList();
            eArmoryDoor = UnityEngine.GameObject.FindObjectsOfType<ArmoryDoor>().ToList();
            eBearTrap = UnityEngine.GameObject.FindObjectsOfType<BearTrap>().ToList();
            eCrow = UnityEngine.GameObject.FindObjectsOfType<Crow>().ToList();
            eCrowFlock = UnityEngine.GameObject.FindObjectsOfType<CrowFlock>().ToList();
            eDeadBodies = UnityEngine.GameObject.FindObjectsOfType<DeadBodyController>().ToList();
            eDogHouse = UnityEngine.GameObject.FindObjectsOfType<DogHouse>().ToList();
            eGroundItem = UnityEngine.GameObject.FindObjectsOfType<GroundItem>().ToList();
            eJackInBox = UnityEngine.GameObject.FindObjectsOfType<JackInBox>().ToList();
            ePlayableCharacter = UnityEngine.GameObject.FindObjectsOfType<PlayableCharacter>().ToList();
            ePuzzleButton = UnityEngine.GameObject.FindObjectsOfType<PuzzleButton>().ToList();
            ePuzzleCabin = UnityEngine.GameObject.FindObjectsOfType<PuzzleCabin>().ToList();
            ePuzzleKey = UnityEngine.GameObject.FindObjectsOfType<PuzzleKey>().ToList();
            ePuzzleTablet = UnityEngine.GameObject.FindObjectsOfType<PuzzleTablet>().ToList();
            eRadio = UnityEngine.GameObject.FindObjectsOfType<Radio>().ToList();
            eRabbitController = UnityEngine.GameObject.FindObjectsOfType<RabbitController>().ToList();
            eRifle = UnityEngine.GameObject.FindObjectsOfType<Rifle>().ToList();
            eRifleItem = UnityEngine.GameObject.FindObjectsOfType<RifleItem>().ToList();
            eRocket = UnityEngine.GameObject.FindObjectsOfType<Rocket>().ToList();
            eStone = UnityEngine.GameObject.FindObjectsOfType<Stone>().ToList();
            eToy = UnityEngine.GameObject.FindObjectsOfType<Toy>().ToList();
            eTruckPart = UnityEngine.GameObject.FindObjectsOfType<TruckPart>().ToList();
            eWitch = UnityEngine.GameObject.FindObjectsOfType<Witch>().ToList();
            eTelevision = UnityEngine.GameObject.FindObjectsOfType<Television>().ToList();
            eCreature = UnityEngine.GameObject.FindObjectsOfType<CreatureAIManager>().ToList();
            eBodyPlayer = UnityEngine.GameObject.FindObjectsOfType<BodyAnimator>().ToList();
        }

        private static void nullItems()
        {
            if (eItem.Count > 0) { eItem.Clear(); }
            if (eActivableItem.Count > 0) { eActivableItem.Clear(); }
            if (eArmoryDoor.Count > 0) { eArmoryDoor.Clear(); }
            if (eBearTrap.Count > 0) { eBearTrap.Clear(); }
            if (eCrow.Count > 0) { eCrow.Clear(); }
            if (eCrowFlock.Count > 0) { eCrowFlock.Clear(); }
            if (eDeadBodies.Count > 0) { eDeadBodies.Clear(); }
            if (eDogHouse.Count > 0) { eDogHouse.Clear(); }
            if (eGroundItem.Count > 0) { eGroundItem.Clear(); }
            if (eJackInBox.Count > 0) { eJackInBox.Clear(); }
            if (ePlayableCharacter.Count > 0) { ePlayableCharacter.Clear(); }
            if (ePuzzleButton.Count > 0) { ePuzzleButton.Clear(); }
            if (ePuzzleCabin.Count > 0) { ePuzzleCabin.Clear(); }
            if (ePuzzleKey.Count > 0) { ePuzzleKey.Clear(); }
            if (ePuzzleTablet.Count > 0) { ePuzzleTablet.Clear(); }
            if (eRadio.Count > 0) { eRadio.Clear(); }
            if (eRabbitController.Count > 0) { eRabbitController.Clear(); }
            if (eRifle.Count > 0) { eRifle.Clear(); }
            if (eRifleItem.Count > 0) { eRifleItem.Clear(); }
            if (eRocket.Count > 0) { eRocket.Clear(); }
            if (eStone.Count > 0) { eStone.Clear(); }
            if (eStoneThrower.Count > 0) { eStoneThrower.Clear(); }
            if (eToy.Count > 0) { eToy.Clear(); }
            if (eTruckPart.Count > 0) { eTruckPart.Clear(); }
            if (eWitch.Count > 0) { eWitch.Clear(); }
            if (eTelevision.Count > 0) { eTelevision.Clear(); }
            if (eCreature.Count > 0) { eCreature.Clear(); }
            if (eBodyPlayer.Count > 0) { eBodyPlayer.Clear(); }

        }

        public static void DrawBoneLine(Vector3 w2s_objectStart, Vector3 w2s_objectFinish, Color color)
        {
            Render.DrawLine(new Vector2(w2s_objectStart.x, (float)Screen.height - w2s_objectStart.y), new Vector2(w2s_objectFinish.x, (float)Screen.height - w2s_objectFinish.y), color, 1f);
        }
    }
}
