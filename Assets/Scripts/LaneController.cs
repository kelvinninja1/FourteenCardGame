using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	public string LaneName { get; private set; }
	public string Owner { get; private set; }

	// Use this for initialization
	void Start () {
		LaneName = this.gameObject.name;
		Owner = this.LaneName.Substring (0, LaneName.IndexOf (' '));
	}

	//Add lane to card's collider list
	void OnTriggerEnter(Collider other){
		var cardController = other.gameObject.GetComponent<CardController> ();
		if (cardController != null && cardController.Owner == Owner && !GameModel.Instance.Lanes.Occupied(LaneName)) {
			cardController.AddToTriggerList(this.gameObject.GetComponent<Collider>());
		}
	}
	void OnTriggerExit(Collider other){
		var cardController = other.gameObject.GetComponent<CardController> ();
		if (cardController != null && cardController.Owner == Owner) {
			cardController.RemoveFromTriggerList(this.gameObject.GetComponent<Collider>());
		}
	}
}
