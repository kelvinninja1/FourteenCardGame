using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HandModel : CardList {
	
	//private IDictionary<GameObject, int> instanceMap;
	private Vector3 centerPosition;
	private float leftBound, rightBound;
	private string owner;

	private GameObject currentFocus = null;
	
	private const float startZPos = -0.99f;
	
	public HandModel(string player) : base(){
		owner = player;
		//instanceMap = new Dictionary<GameObject, int> ();
		
		//calculate hand bounds
		var handRenderer = GameObject.Find (player + "Hand").GetComponent<MeshRenderer>();
		centerPosition = handRenderer.bounds.center;
		leftBound = centerPosition.x - (handRenderer.bounds.size.x / 2);
		rightBound = centerPosition.x + (handRenderer.bounds.size.x / 2);
	}
	
	public bool InHand(GameObject obj){
		foreach(CardModel m in cards){
			if(m.Instance == obj){
				return true;
			}
		}
		return false;
	}

	public void AddCard(CardModel card){
		int index = cards.Count;
		if (card.Instance == null)
			card.CreateCardInstance (owner);
		else if (cards.Count == 0) //handle empty hand
			index = 0;
		else { //calculate index of closest card
			int closest = 0;
			float closestDist = Vector3.Magnitude(cards[0].Instance.transform.position - card.Instance.transform.position);
			for(int i=1; i<cards.Count; i++){
				float curDist = Vector3.Magnitude(cards[i].Instance.transform.position - card.Instance.transform.position);
				if(closestDist > curDist){
					closestDist = curDist;
					closest = i;
				}
			}

			if(card.Instance.transform.position.x < cards[closest].Instance.transform.position.x)
				index = closest;
			else
				index = closest + 1;
		}

		cards.Insert(index, card);

		
		//space out cards and focus the new card
		BalanceWidth ();
		SetFocus (cards.Count - 1);
	}
	
	public void PlayCard(GameObject obj, string lane){
		CardModel card = GameModel.Instance.GetCardModel (obj);

		GameModel.Instance.Lanes.AddCard (card, lane);

		RemoveCard (obj);
	}

	public bool IsFocus(GameObject obj){
		return currentFocus == obj;
	}

	private void RemoveCard(GameObject obj){		
		//remove card from hand and list of instances
		cards.Remove (GameModel.Instance.GetCardModel (obj));
		
		//balance hand width visually
		BalanceWidth ();
		SetFocus (cards.Count - 1);
	}
	
	public void SetFocus(GameObject obj){
		currentFocus = obj;
		SetFocus (cards.IndexOf(GameModel.Instance.GetCardModel(obj)));
	}
	
	private void SetFocus(int cardIndex){
		if (cardIndex < 0)
			return;

		float spacing = 1.0f / cards.Count;
		cards [cardIndex].MoveCardInstance (null, null, startZPos, Position.InHand);
		
		float zPos = startZPos;
		for (int i=cardIndex+1; i<cards.Count; i++) {
			zPos += spacing;
			cards[i].MoveCardInstance(null, null, zPos, Position.InHand);
		}
		
		zPos = startZPos;
		for (int i=cardIndex-1; i>=0; i--) {
			zPos += spacing;
			cards[i].MoveCardInstance(null, null, zPos, Position.InHand);
		}
	}
	
	private void BalanceWidth(){
		int count = cards.Count;

		if (count == 0)
			return;

		int midIndex = (count-1) / 2;
		float cardWidth = cards [0].Instance.GetComponent<Collider> ().bounds.size.x;
		
		if (cardWidth * count <= rightBound - leftBound) { //cards fit into hand box
			if (count % 2 == 0) { //even number of cards
				//left side
				Vector3 leftPos = centerPosition - new Vector3(cardWidth/2, 0, 0);
				cards[midIndex].MoveCardInstance(leftPos.x, leftPos.y, leftPos.z, Position.InHand);
				for(int i=midIndex-1; i>=0; i--){
					leftPos -= new Vector3(cardWidth, 0, 0);
					cards[i].MoveCardInstance(leftPos.x, leftPos.y, leftPos.z, Position.InHand);
				}
				
				//right side
				Vector3 rightPos = centerPosition + new Vector3(cardWidth/2, 0, 0);
				cards[midIndex+1].MoveCardInstance(rightPos.x, rightPos.y, rightPos.z, Position.InHand);
				for(int i=midIndex+2; i<count; i++){
					rightPos += new Vector3(cardWidth, 0, 0);
					cards[i].MoveCardInstance(rightPos.x, rightPos.y, rightPos.z, Position.InHand);
				}
				
			} else { //odd number of cards
				//move middle card
				cards[midIndex].MoveCardInstance(centerPosition.x, centerPosition.y, centerPosition.z, Position.InHand);
				
				//left side
				Vector3 leftPos = centerPosition;
				for(int i=midIndex-1; i>=0; i--){
					leftPos -= new Vector3(cardWidth, 0, 0);
					cards[i].MoveCardInstance(leftPos.x, leftPos.y, leftPos.z, Position.InHand);
				}
				
				//right side
				Vector3 rightPos = centerPosition;
				for(int i=midIndex+1; i<count; i++){
					rightPos += new Vector3(cardWidth, 0, 0);
					cards[i].MoveCardInstance(rightPos.x, rightPos.y, rightPos.z, Position.InHand);
				}
			}
		} else { //cards do not fit into hand box
			float start = leftBound + (cardWidth/2);
			float end = rightBound - (cardWidth/2);
			float spacing = (end - start) / (count-1);
			
			float xPos = start;
			for(int i=0; i<count; i++){
				cards[i].MoveCardInstance(xPos, centerPosition.y, centerPosition.z, Position.InHand);
				xPos += spacing;
			}
		}
		
	}
}
