using UnityEngine;

public static class PlayerPrefKeys {

	public static class Emoji{
		public const string HUNGER = "Emoji/Hunger";
		public const string HYGENE = "Emoji/Hygiene";
		public const string HAPPINESS = "Emoji/Happiness";
		public const string STAMINA = "Emoji/Stamina";
		public const string HEALTH = "Emoji/Health";
	}

	public static class Player{
		public const string PLAYER_AUTH_TOKEN = "Player/AuthToken";

		public const string PLAYER_COIN = "Player/Coin";
		public const string PLAYER_GEM = "Player/Gem";

		//example
		public const string PLAYER_EMOJI_TYPE = "Player/EmojiType";

		public const string LAST_TIME_PLAYED = "Player/LastLogin";
		public const string TIME_ON_PAUSE = "Player/TimeOnPause";

		public const string EMOJI_RECORD_COUNT = "Player?EmojiRecordCount";
	}
}
