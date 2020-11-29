using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapEffect : MonoBehaviour {

[SerializeField] PhotonView pv;

public PhotonView Pv { get => pv; set => pv =  value ; }

public virtual void StartEffect() { }

}
