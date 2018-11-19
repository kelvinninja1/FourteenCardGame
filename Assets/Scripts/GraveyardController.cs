using UnityEngine;
using System.Collections;

public class GraveyardController : MonoBehaviour {

	public string LaneName { get; private set; }

	// Use this for initialization
	void Start () {
		LaneName = this.gameObject.name;
	}

	void OnTriggerEnter(Collider other){
		var cardController = other.gameObject.GetComponent<CardController> ();
		if (cardController != null) {
			cardController.AddToTriggerList(this.gameObject.GetComponent<Collider>());
		}
	}
	void OnTriggerExit(Collider other){
		var cardController = other.gameObject.GetComponent<CardController> ();
		if (cardController != null) {
			cardController.RemoveFromTriggerList(this.gameObject.GetComponent<Collider>());
		}
	}
}
