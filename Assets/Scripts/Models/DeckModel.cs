using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckModel {

	private Queue<CardModel> deck;

	public DeckModel(){
		deck = new Queue<CardModel> ();
	}
}