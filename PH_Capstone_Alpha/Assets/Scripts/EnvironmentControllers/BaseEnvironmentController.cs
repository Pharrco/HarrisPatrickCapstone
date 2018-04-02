using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnvironmentController : MonoBehaviour {

    public virtual void GetMove() { }

    public bool MoveComplete { get; protected set; }

	public virtual void EndTurnUpdate() { }

	public virtual void ForceUpdate() { }

	public virtual void Reset() { }
}
