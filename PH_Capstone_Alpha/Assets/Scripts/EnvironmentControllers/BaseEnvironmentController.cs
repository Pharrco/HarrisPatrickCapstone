﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnvironmentController : MonoBehaviour {

    public virtual void GetMove() { }

    public bool MoveComplete { get; protected set; }
}