using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CardList {

	protected IList<CardModel> cards;
	
	public CardList(){
		cards = new List<CardModel> ();
	}
}
