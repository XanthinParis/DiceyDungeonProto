﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script gère la globalité des références entre les scripts.
/// </summary>

public class Manager : Singleton<Manager>
{
    [Header("Gameplay")]
    public DiceManager diceManager;
    
    [Header("Character")]
    public PlayerManager playerManager;
    
    [Header("Cursor")]    
    public CursorBehaviour cursorBehaviour;
    public GameObject cursor;
}