//This class is auto-generated do not modify
namespace k
{
	public static class Scenes
	{
		public const string MAIN = "Main";
		public const string UI = "UI";
		public const string LEVEL1 = "Level1";
		public const string START = "Start";

		public const int TOTAL_SCENES = 4;


		public static int nextSceneIndex()
		{
			var currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			if( currentSceneIndex + 1 == TOTAL_SCENES )
				return 0;
			return currentSceneIndex + 1;
		}
	}
}