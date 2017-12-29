using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PlayerData : MonoBehaviour {
	static PlayerData instance;
	public static PlayerData Instance{ get{return instance;} }

	public PlayerInventory inventory = new PlayerInventory();

	int defaultCoin = 1000; //TODO: ADJUST THIS LATER
	int defulatGem = 0; //TODO: ADJUST THIS LATER

	public GameObject babyEmoji;

	public Transform emojiParentTransform;

	Emoji playerEmoji;
	EmojiType selectedEmoji;

	bool hasInitEmojiObject = false;

	List<EmojiType> emojiAlbumData = new List<EmojiType>();
	List<string> emojiAlbumEntryTime = new List<string>();
	List<float> emojiCompletionRate = new List<float>();

	[Header("mau ga mau :(")]
	public HatUIItem[] hatItems;

	public Emoji PlayerEmoji{
		get{return playerEmoji;}
	}

	public EmojiType SelectedEmoji{
		set{selectedEmoji=value;}
		get{return selectedEmoji;}
	}

	public int PlayerCoin{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.PLAYER_COIN,defaultCoin);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.PLAYER_COIN,value);}
	}

	public int PlayerGem{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.PLAYER_GEM,defulatGem);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.PLAYER_GEM,value);}
	}

	public string PlayerAuthToken{
		get{return PlayerPrefs.GetString(PlayerPrefKeys.Player.PLAYER_AUTH_TOKEN);}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.PLAYER_AUTH_TOKEN,value);}
	}

	public int PlayerEmojiType{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.PLAYER_EMOJI_TYPE,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.PLAYER_EMOJI_TYPE,value);}
	}

	public int PlayerFirstPlay{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.FIRST_PLAY,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.FIRST_PLAY,value);}
	}

	public string EmojiName{
		get{return PlayerPrefs.GetString (PlayerPrefKeys.Emoji.EMOJI_NAME, "");}
		set{PlayerPrefs.SetString (PlayerPrefKeys.Emoji.EMOJI_NAME, value);}
	}

	public List<EmojiType> EmojiAlbumData{
		get{return emojiAlbumData;}
	}

	public List<string> EmojiAlbumEntryTime{
		get{return emojiAlbumEntryTime;}
	}

	public List<float> EmojiCompletionRate{
		get{return emojiCompletionRate;}
	}

	public RoomType LastCurrentRoom{
		get{return (RoomType)PlayerPrefs.GetInt(PlayerPrefKeys.Player.LAST_CURRENT_ROOM,2);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.LAST_CURRENT_ROOM,(int)value);}
	}

	public float BGMVolume{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Sound.BGM_VOLUME,1);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Sound.BGM_VOLUME,value);}
	}

	public float SFXVolume{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Sound.SFX_VOLUME,1);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Sound.SFX_VOLUME,value);}
	}

	public float VoicesVolume{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Sound.VOICES_VOLUME,1);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Sound.VOICES_VOLUME,value);}
	}
		
	public int TutorialFirstVisit{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_VISIT, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_VISIT, value);}
	}

	public int TutorialIdleLivingRoom{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.IDLE_LIVING_ROOM, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.IDLE_LIVING_ROOM, value);}
	}

	public int TutorialFirstBedroom{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_BEDROOM, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_BEDROOM, value);}
	}

	public int TutorialFirstBathroom{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_BATHROOM, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_BATHROOM, value);}
	}

	public int TutorialFirstKitchen{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_KITCHEN, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_KITCHEN, value);}
	}

	public int TutorialFirstPlayroom{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_PLAYROOM, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_PLAYROOM, value);}
	}

	public int TutorialFirstGarden{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_GARDEN, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_GARDEN, value);}
	}

	public int TutorialFirstProgressUI{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_PROGRESS_UI, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_PROGRESS_UI, value);}
	}

	public int TutorialFirstEditRoom{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_EDITROOM_UI, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_EDITROOM_UI,value);}
	}

	public int TutorialFirstHungerRed{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_HUNGER_RED, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_HUNGER_RED, value);}
	}

	public int TutorialFirstHygieneRed{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_HYGIENE_RED, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_HYGIENE_RED, value);}
	}

	public int TutorialFirstHappinessRed{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_HAPPINESS_RED, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_HAPPINESS_RED, value);}
	}

	public int TutorialFirstStaminaRed{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_STAMINA_RED, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_STAMINA_RED, value);}
	}

	public int TutorialFirstHealthRed{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_HEALTH_RED, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_HEALTH_RED, value);}
	}

	public int TutorialFirstHealthOrange{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_HEALTH_ORANGE, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_HEALTH_ORANGE, value);}
	}

	public int TutorialFirstExpressionFull{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_EXPRESSION_FULL, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_EXPRESSION_FULL, value);}
	}

	public int TutorialFirstEmojiDead{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_EMOJI_DEAD, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_EMOJI_DEAD, value);}
	}

	public int TutorialFirstNewExpression{
		get{return PlayerPrefs.GetInt (PlayerPrefKeys.Tutorial.FIRST_NEW_EXPRESSION, 0);}
		set{PlayerPrefs.SetInt (PlayerPrefKeys.Tutorial.FIRST_NEW_EXPRESSION,value);}
	}

	public DateTime RandomCoinAndGemCooldown{
		get{
			string temp = PlayerPrefs.GetString(PlayerPrefKeys.Player.RANDOM_COINGEM_COOLDOWN);
			return DateTime.Parse(temp);
		}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.RANDOM_COINGEM_COOLDOWN,value.ToString());}
	}


	public int GardenField0{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.GARDENFIELD0,1);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.GARDENFIELD0,value);}
	}
	public int GardenField1{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.GARDENFIELD1,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.GARDENFIELD1,value);}
	}
	public int GardenField2{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.GARDENFIELD2,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.GARDENFIELD2,value);}
	}

	public int MiniGamePainting{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.MINIGAME_PAINTING,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.MINIGAME_PAINTING,value);}
	}

	public int MiniGameBlocks{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.MINIGAME_BLOCKS,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.MINIGAME_BLOCKS,value);}
	}

	public int MiniGameDanceMat{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.MINIGAME_DANCEMAT,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.MINIGAME_DANCEMAT,value);}
	}

	//caesar salad, ramen, burger, skewer, grilled fish, pizza, sundubu, chicken & fries, Bacon baked potato, steak
	public int RecipeCaesarSalad{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_CAESARSALAD,1);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_CAESARSALAD,value);}
	}

	public int RecipeRamen{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_RAMEN,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_RAMEN,value);}
	}

	public int RecipeBurger{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_BURGER,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_BURGER,value);}
	}

	public int RecipeSkewer{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_SKEWER,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_SKEWER,value);}
	}

	public int RecipeGrilledFish{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_GRILLEDFISH,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_GRILLEDFISH,value);}
	}

	public int RecipePizza{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_PIZZA,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_PIZZA,value);}
	}

	public int RecipeSundubu{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_SUNDUBU,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_SUNDUBU,value);}
	}

	public int RecipeChickenAndFries{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_CHICKENANDFRIES,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_CHICKENANDFRIES,value);}
	}

	public int RecipeBaconBakedPotato{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_BACONBAKEDPOTATO,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_BACONBAKEDPOTATO,value);}
	}

	public int RecipeSteak{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.Progress.RECIPE_STEAK,0);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.Progress.RECIPE_STEAK,value);}
	}


	void Awake()
	{
		if(instance != null && instance != this) Destroy(this.gameObject);
		else instance = this;
		DontDestroyOnLoad(this.gameObject);

		//selectedEmoji = EmojiType.Emoji;
	}

	public void InitPlayerEmoji(GameObject playerEmoji)
	{
		GameObject temp = (GameObject) Instantiate(playerEmoji,emojiParentTransform);

		this.playerEmoji = temp.GetComponent<Emoji>();
//		this.playerEmoji.Init();
		this.playerEmoji.SetBodyCurrentScale(new Vector3(0.8f,0.8f,1));
	}

	public void InitPlayerBabyEmoji(EmojiAgeType type, GameObject adultObject, EmojiSO soData)
	{
		GameObject temp = (GameObject) Instantiate (babyEmoji,emojiParentTransform);

		this.playerEmoji = temp.GetComponent<Emoji>();
		this.playerEmoji.emojiBaseData = soData;
		this.playerEmoji.GetComponent<BabyEmoji>().emojiAdultObject = adultObject;
//		this.playerEmoji.Init();

		if(type == EmojiAgeType.Baby){
			this.playerEmoji.SetBodyCurrentScale(new Vector3(0.5f,0.5f,1));
		}else if(type == EmojiAgeType.Juvenille){
			this.playerEmoji.SetBodyCurrentScale(new Vector3(0.6f,0.65f,1));
		}
	}

	[Header("Do Not Modify")]
	public GameObject babyEmojiObjToDestroy;
	public void DestroyBaby()
	{
		Destroy(babyEmojiObjToDestroy);
	}
}