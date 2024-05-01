/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameData : MonoBehaviour
{
    public void SaveGame(string playerName, DateTime saveTime, List<string> collectedArtifacts)
    {
        GameData gameData = new GameData(
            GameManager.instance.isPaused,
            AudioManager.instance.bgMusic.time,
            GameManager.instance.currentLevel,
            GameManager.instance.player.currentHealth,
            ScoreManager.instance.GetCurrentScore(),
            ScoreManager.instance.GetCurrentMisses(),
            GameManager.instance.teleMoveEnabled,
            GameManager.instance.contMoveEnabled,
            GameManager.instance.snapRotationEnabled,
            GameManager.instance.contRotationEnabled,
            Settings.instance.masterVolume,
            Settings.instance.offsetValue,
            Settings.instance.cameraShakeEnabled,
            Settings.instance.hapticFeedbackEnabled,
            Settings.instance.panelsEnabled,
            playerName,
            saveTime,
            collectedArtifacts
        );
        SaveManager.instance.SaveGame(gameData);
    }
}
*/