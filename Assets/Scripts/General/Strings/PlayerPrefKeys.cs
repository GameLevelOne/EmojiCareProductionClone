using UnityEngine;

public static class PlayerPrefKeys {

	public static class Emoji
	{
		public const string HUNGER = "Emoji/Hunger";
		public const string HYGENE = "Emoji/Hygiene";
		public const string HAPPINESS = "Emoji/Happiness";
		public const string STAMINA = "Emoji/Stamina";
		public const string HEALTH = "Emoji/Health";
		public const string CURRENT_SCALE = "Emoji/CurrentScale";

		public const string UNLOCKED_EXPRESSIONS = "Emoji/UnlockedExpressions/";

		public const string EMOJI_NAME = "Emoji/EmojiName";

		public const string EMOJI_SLEEPING = "Emoji/Sleeping";

		public const string EMOJI_EXPRESSION_PROGRESS = "Emoji/ExpressionProgress/";
		public const string EMOJI_EXPRESSION_STATUS = "Emoji/ExpressionStatus/";
		public const string EMOJI_EXPRESSION_PROGRESSRATIO = "Emoji/ExpressionProgressRatio/";
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

		public const string SEND_OFF_COUNT = "Player/SendOffCount";

		public static class Inventory
		{
			public const string INGREDIENT = "Player/Inventory/Ingredient/";
			public const string HAT = "Player/Inventory/Hat/";
			public const string CURRENT_HAT = "Player/Inventory/CurrentHat";
		}

		public static class Progress{
			public const string GARDENFIELD0 = "Player/Progress/GardenField0";
			public const string GARDENFIELD1 = "Player/Progress/GardenField1";
			public const string GARDENFIELD2 = "Player/Progress/GardenField2";

			public const string MINIGAME_PAINTING = "Player/Progress/MiniGamePainting";
			public const string MINIGAME_DANCEMAT = "Player/Progress/MiniGameDanceMat";
			public const string MINIGAME_BLOCKS = "Player/Progress/MiniGameBlocks";

			//caesar salad, ramen, burger, skewer, grilled fish, pizza, sundubu, chicken & fries, Bacon baked potato, steak
			public const string RECIPE_CAESARSALAD = "Player/Progress/RecipeCaesarSalad";
			public const string RECIPE_RAMEN = "Player/Progress/RecipeRamen";
			public const string RECIPE_BURGER = "Player/Progress/RecipeBurger";
			public const string RECIPE_SKEWER = "Player/Progress/RecipeSkewer";
			public const string RECIPE_GRILLEDFISH = "Player/Progress/RecipeGrilledFish";
			public const string RECIPE_PIZZA = "Player/Progress/RecipePizza";
			public const string RECIPE_SUNDUBU = "Player/Progress/RecipeSundubu";
			public const string RECIPE_CHICKENANDFRIES = "Player/Progress/RecipeChickenAndFries";
			public const string RECIPE_BACONBAKEDPOTATO = "Player/Progress/RecipeBaconBakedPotato";
			public const string RECIPE_STEAK = "Player/Progress/RecipeSteak";
		}
		public const string RANDOM_COINGEM_COOLDOWN = "Player/RandomCoinGemCooldown";
	}

	public static class Game
	{
		public const string TIME_FIRST_PLAY = "Game/TimeFirstPlay";

		public const string HAS_INIT_INGREDIENT = "Game/HasInitIngredient";
		public const string SEEDS = "Game/Garden/Seed";
		public const string GOODS = "Game/Garden/Goods";

		public const string HAS_SEED0 = "Game/Garden/HasSeed/0";
		public const string HAS_SEED1 = "Game/Garden/HasSeed/1";
		public const string HAS_SEED2 = "Game/Garden/HasSeed/2";

		public const string SEED_TYPE0 = "Game/Garden/SeedType/0";
		public const string SEED_TYPE1 = "Game/Garden/SeedType/1";
		public const string SEED_TYPE2 = "Game/Garden/SeedType/2";

		public const string SEED_GROW_DURATION0 = "Game/Garden/SeedGrowDuration/0";
		public const string SEED_GROW_DURATION1 = "Game/Garden/SeedGrowDuration/1";
		public const string SEED_GROW_DURATION2 = "Game/Garden/SeedGrowDuration/2";

		public const string SEED_HARVEST_TIME0 = "Game/Garden/SeedHarvestTime/0";
		public const string SEED_HARVEST_TIME1 = "Game/Garden/SeedHarvestTime/1";
		public const string SEED_HARVEST_TIME2 = "Game/Garden/SeedHarvestTime/2";

		public const string DANCE_MAT_TILE_COLOR_DATA = "Game/Playroom/DanceMatTileColorData/TileNo";

		public const string FURNITURE_VARIANT = "Game/Furnitures/";
		public const string FURNITURE_VARIAN_STATUS = "/Variant/";

		public static class Garden{
			public const string SEED_RESTOCK_TIME = "Game/Garden/SeedRestock";
			public const string ITEM_RESTOCK_TIME = "Game/Garden/StallItemRestock";

			public const string ITEM_CURRENT = "Game/Garden/CurrentItem/";
			public const string SEED_CURRENT = "Game/Garden/CurrentSeed/";

			public const string SEED_TYPE = "Game/Garden/SeedType/";
			public const string SEED_WATER_COOLDOWN = "Game/Garden/SeedWaterCooldown/";
			public const string SEED_HARVEST_TIME = "Game/Garden/SeedHarvestTime/";
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
		public const string FIRST_NEW_EXPRESSION = "Tutorial/FirstNewExpression";
		public const string FIRST_EMOJI_DEAD = "Tutorial/FirstEmojiDead";
	}

	public static class Sound{
		public const string BGM_VOLUME = "Sound/BGMVolume";
		public const string SFX_VOLUME = "Sound/SFXVolume";
		public const string VOICES_VOLUME = "Sound/VoicesVolume";
	}

	public static class Album{
		public const string EMOJI_TYPE = "Album/EmojiType/";
		public const string ENTRY_TIME = "Album/EmojiEntryTime/";
		public const string COMPLETION_RATE = "Album/EmojiCompletionRate/";
	}
}
