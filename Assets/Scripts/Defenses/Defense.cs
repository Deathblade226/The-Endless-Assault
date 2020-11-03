using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviourPun {

[SerializeField]string type = "Defense";
[SerializeField]float cost = 0;
public float Cost { get => cost; set => cost =  value ; }
public string Type { get => type; set => type =  value ; }
}
