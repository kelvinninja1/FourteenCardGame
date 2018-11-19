using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LaneModel {	
	private Dictionary<string, SingleLaneModel> lanes;
	
	public LaneModel(){
		var lpos = GameObject.Find ("Player LeftLane").GetComponent<Collider> ().transform.position;
		var lcpos = GameObject.Find ("Player LeftCenterLane").GetComponent<Collider> ().transform.position;
		var rcpos = GameObject.Find ("Player RightCenterLane").GetComponent<Collider> ().transform.position;
		var rpos = GameObject.Find ("Player RightLane").GetComponent<Collider> ().transform.position;

		lanes = new Dictionary<string, SingleLaneModel> (){
			{"Player LeftLane", new SingleLaneModel(lpos)},
			{"Player LeftCenterLane", new SingleLaneModel(lcpos)},
			{"Player RightCenterLane", new SingleLaneModel(rcpos)},
			{"Player RightLane", new SingleLaneModel(rpos)}
		};
	}

	public CardModel GetCard(string lane){
		return lanes [lane].GetCard ();
	}

	public bool Occupied(string lane){
		return lanes [lane].Occupied ();
	}
	
	public bool AddCard(CardModel card, string lane){
		if (lanes [lane].Occupied()) {
			return false; //fail to add card to occupied lane
		}
		
		lanes [lane].AddCard (card);
		
		return true;
	}

	public void MoveCard(CardModel card, string lane){
		RemoveCard (card);
		GameModel.Instance.Lanes.AddCard(card, lane);
	}

	public void ReturnToHand(CardModel card){
		RemoveCard (card);
		GameModel.Instance.PlayerHand.AddCard(card);
	}

	public void RemoveCard(CardModel card){
		foreach(string key in lanes.Keys){
			var c = lanes[key].GetCard();
			if(c == card){
				lanes[key].RemoveCard();
				break;
			}
		}
	}
	
	class SingleLaneModel{
		private CardModel card;
		private Vector3 pos;

		public SingleLaneModel(){
			pos = new Vector3(0,0,0);
		}

		public CardModel GetCard(){
			return card;
		}
		
		public SingleLaneModel(Vector3 lanePos){
			pos = lanePos;
		}

		public bool Occupied(){
			return card != null;
		}
		
		public void AddCard(CardModel c){
			this.card = c;
			this.card.MoveCardInstance (pos, Position.InLane);
		}

		public void RemoveCard(){
			this.card = null;
		}
	}
	
}