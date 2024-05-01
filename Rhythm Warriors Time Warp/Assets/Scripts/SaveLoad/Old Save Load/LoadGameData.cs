/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGameData : MonoBehaviour
{
    public void LoadGame()
    {
        GameData savedData = SaveManager.instance.LoadGame();
        if (savedData != null)
        {
            // update game state based on savedData
            GameManager.instance.isPaused = savedData.isPaused;
            AudioManager.instance.bgMusic.time = savedData.audioClipPosition;
            GameManager.instance.currentLevel = savedData.currentLevel;
            GameManager.instance.player.currentHealth = savedData.playerHealth;
            ScoreManager.instance.currentScore = savedData.currentScore;
            ScoreManager.instance.currentMisses = savedData.currentMisses;
            GameManager.instance.teleMoveEnabled = savedData.teleMoveEnabled;
            GameManager.instance.contMoveEnabled = savedData.contMoveEnabled;
            GameManager.instance.snapRotationEnabled = savedData.snapRotationEnabled;
            GameManager.instance.contRotationEnabled = savedData.contRotationEnabled;
            Settings.instance.masterVolume = savedData.masterVolume;
            Settings.instance.offsetValue = savedData.offsetValue;
            Settings.instance.cameraShakeEnabled = savedData.cameraShakeEnabled;
            Settings.instance.hapticFeedbackEnabled = savedData.hapticFeedbackEnabled;
            Settings.instance.panelsEnabled = savedData.panelsEnabled;

            // saved data shown to player on menu user interface
            string playerName = savedData.playerName;
            DateTime saveTime = savedData.saveTime;
            List<string> collectedArtifacts = savedData.collectedArtifacts;
            // process the loaded data as needed
        }
    }
}
*/