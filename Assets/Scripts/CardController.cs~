using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]

public class CardController : MonoBehaviour 
{
	private Vector3 screenPoint;
	private Vector3 offset;
	private List<Collider> triggerList = new List<Collider>();
	public string Owner { get; set; }

	//Move card on mouse click
	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
		
	}
	void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = new Vector3(curPosition.x, curPosition.y, -1);
	}

	//When mouse is released, determine where card should go
	void OnMouseUp(){
		var attCardModel = GameModel.Instance.GetCardModel (this.gameObject);

		//calculate the position of the closest collider
		float closestDistance = -1;
		Collider targetCollider = null;
		foreach(Collider c in triggerList){
			var position = c.gameObject.transform.position;

			var distance = Vector3.Magnitude(this.transform.position - position);
			if(distance < closestDistance || closestDistance == -1){
				targetCollider = c;
				closestDistance = distance;
			}
		}

		//move card to target
		if (closestDistance != -1) {
			//remove all but targetCollider
			triggerList = new List<Collider>(){
				targetCollider
			};

			var laneController = targetCollider.gameObject.GetComponent<LaneController> ();
			var graveController = targetCollider.gameObject.GetComponent<GraveyardController> ();
			var handController = targetCollider.gameObject.GetComponent<HandController> ();

			if (laneController != null) { //check if target is a lane
				if(attCardModel.PosType == Position.InHand)
					GameModel.Instance.PlayerHand.PlayCard (this.gameObject, laneController.LaneName);
				else if(attCardModel.PosType == Position.InLane)
					GameModel.Instance.Lanes.MoveCard(attCardModel, laneController.LaneName);
			} 
			else if (graveController != null) { //check if target is a graveyard
				//GameModel.Instance.PlayerHand.PlayCard (this.gameObject, laneController.LaneName);
			} 
			else if(handController != null){
				if(attCardModel.PosType == Position.InLane){
					GameModel.Instance.Lanes.ReturnToHand(attCardModel);
				}
				else if(attCardModel.PosType == Position.InHand)
					attCardModel.ResetInstancePosition();
			}
		} else { //return card to last position
			attCardModel.ResetInstancePosition();
		}
	}

	void OnMouseOver(){
		if(GameModel.Instance.PlayerHand.InHand(this.gameObject) && !GameModel.Instance.PlayerHand.IsFocus(this.gameObject))
			GameModel.Instance.PlayerHand.SetFocus (this.gameObject);
	}

	public void AddToTriggerList(Collider other){
		if (!triggerList.Contains (other))
			triggerList.Add (other);
	}

	public void RemoveFromTriggerList(Collider other){
		if (triggerList.Contains (other))
			triggerList.Remove (other);
	}
}
