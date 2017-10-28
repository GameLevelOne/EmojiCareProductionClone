using UnityEngine;

public static class PlayerPrefKeys {

	public static class Emoji
	{
		public const string HUNGER = "Emoji/Hunger";
		public const string HYGENE = "Emoji/Hygiene";
		public const string HAPPINESS = "Emoji/Happiness";
		public const string STAMINA = "Emoji/Stamina";
		public const string HEALTH = "Emoji/Health";

		public const string UNLOCKED_EXPRESSIONS = "Emoji/UnlockedExpressions";

		public const string EMOJI_NAME = "Emoji/EmojiName";
	}

	public static class Player
	{
		public const string PLAYER_AUTH_TOKEN = "Player/AuthToken";

		public const string PLAYER_COIN = "Player/Coin";
		public const string PLAYER_GEM = "Player/Gem";

		public const string LAST_CURRENT_ROOM = "Player/LastCurrentRoom";

		//example
		public const string PLAYER_EMOJI_TYPE = "Player/EmojiType";

		public const string LAST_TIME_PLAYED = "Player/LastLogin";
		public const string TIME_ON_PAUSE = "Player/TimeOnPause";

		public const string EMOJI_RECORD_COUNT = "Player/EmojiRecordCount";

		public const string FIRST_PLAY = "Player/FirstPlay";

		public static class Inventory
		{
			public const string INGREDIENT = "Player/Inventory/Ingredient/";
		}
	}

	public static class Tutorial{
		public const string FIRST_VISIT = "Tutorial/FirstVisit";
		public const string IDLE_LIVING_ROOM = "Tutorial/IdleLivingRoom";
		public const string FIRST_BEDROOM = "Tutorial/FirstBedroom";
		public const string FIRST_BATHROOM = "Tutorial/FirstBathroom";
		public const string FIRST_KITCHEN = "Tutorial/FirstKitchen";
		public const string FIRST_PLAYROOM = "Tutorial/FirstPlayroom";
		public const string FIRST_GARDEN = "Tutorial/FirstGarden";
		public const string FIRST_PROGRESS_UI = "Tutorial/FirstProgressUI";
		public const string FIRST_EDITROOM_UI = "Tutorial/FirstEditRoomUI";

		public const string FIRST_HUNGER_RED = "Tutorial/FirstHungerRed";
		public const string FIRST_HYGIENE_RED = "Tutorial/FirstHygieneRed";
		public const string FIRST_HAPPINESS_RED = "Tutorial/FirstHappinessRed";
		public const string FIRST_STAMINA_RED = "Tutorial/FirstStaminaRed";
		public const string FIRST_HEALTH_RED = "Tutorial/FirstHealthRed";
		public const string FIRST_HEALTH_ORANGE = "Tutorial/FirstHealthOrange";
		public const string FIRST_EXPRESSION_FULL = "Tutorial/FirstExpressionFull";
		public const string FIRST_EMOJI_DEAD = "Tutorial/FirstEmojiDead";
	}

	public static class Sound{
		public const string BGM_VOLUME = "Sound/BGMVolume";
		public const string SFX_VOLUME = "Sound/SFXVolume";
		public const string VOICES_VOLUME = "Sound/VoicesVolume";
	}
}
