//This class is auto-generated do not modify
namespace k
{
	public static class Scenes
	{
		public const string START = "Start";
		public const string CRYSTAL_DEFENCE = "CrystalDefence";
		public const string HUD = "HUD";
		public const string WIN = "Win";
		public const string LOSE = "Lose";

		public const int TOTAL_SCENES = 5;


		public static int nextSceneIndex()
		{
			var currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			if( currentSceneIndex + 1 == TOTAL_SCENES )
				return 0;
			return currentSceneIndex + 1;
		}
	}
}