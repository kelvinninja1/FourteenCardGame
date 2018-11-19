using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel {
	//Constants
	public const int BaseHealth = 14;

	//Singleton instance
	private static GameModel instance;

	//Game elements
	public GraveyardModel SaintsPile { get; private set; }
	public GraveyardModel SinnersPile { get; private set; }
	public HandModel PlayerHand { get; private set; }
	public HandModel OpponentHand { get; private set; }
	public DeckModel PlayerDeck { get; private set; }
	public DeckModel OpponentDeck { get; private set; }
	public LaneModel Lanes { get; private set; }
	public int PlayerHealth { get; private set; }
	public int OpponentHealth { get; private set; }

	//Associate game objects with their card models for global lookup
	private Dictionary<GameObject, CardModel> modelMap;
	
	private GameModel(){
		//Initialize values
		SaintsPile = new GraveyardModel ();
		SinnersPile = new GraveyardModel ();
		PlayerHand = new HandModel ("Player");
		OpponentHand = new HandModel ("Opponent");
		PlayerDeck = new DeckModel ();
		OpponentDeck = new DeckModel ();
		Lanes = new LaneModel ();
		PlayerHealth = BaseHealth;
		OpponentHealth = BaseHealth;

		modelMap = new Dictionary<GameObject, CardModel> ();

		//Set initial view
	}

	public static GameModel Instance{
		get{
			if(instance == null){
				instance = new GameModel();
			}
			return instance;
		}
	}

	public void CardCreated(GameObject obj, CardModel card){
		modelMap.Add (obj, card);
	}

	public CardModel GetCardModel(GameObject obj){
		return modelMap [obj];
	}
}
