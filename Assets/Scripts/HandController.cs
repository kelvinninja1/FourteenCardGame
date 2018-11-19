using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public string Name { get; private set; }
	
	// Use this for initialization
	void Start () {
		Name = this.gameObject.name;
	}
	
	//Add lane to card's collider list
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
