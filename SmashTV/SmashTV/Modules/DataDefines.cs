using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MesserSmash.Modules {
	public static class DataDefines {
		private static float getValue(int hash) { return (float)SmashDb.get(hash); }
		private const int _ID_FOO = -947836360;
		public static float ID_FOO { get { return getValue(_ID_FOO); } }
		private const int _ID_FOOBAR_TEST = -1556897297;
		public static float ID_FOOBAR_TEST { get { return getValue(_ID_FOOBAR_TEST); } }
		private const int _ID_LEVEL1_MAX_ENEMIES_PER_WAVE = -1846210081;
		public static float ID_LEVEL1_MAX_ENEMIES_PER_WAVE { get { return getValue(_ID_LEVEL1_MAX_ENEMIES_PER_WAVE); } }
		private const int _ID_LEVEL1_TIME_BETWEEN_WAVES = -1883733526;
		public static float ID_LEVEL1_TIME_BETWEEN_WAVES { get { return getValue(_ID_LEVEL1_TIME_BETWEEN_WAVES); } }
		private const int _ID_LEVEL1_BETWEEN_EACH_UNIQUE_SPAWN_CD = 149620343;
		public static float ID_LEVEL1_BETWEEN_EACH_UNIQUE_SPAWN_CD { get { return getValue(_ID_LEVEL1_BETWEEN_EACH_UNIQUE_SPAWN_CD); } }
		private const int _ID_LEVEL2_MAX_ENEMIES_PER_WAVE = 213824306;
		public static float ID_LEVEL2_MAX_ENEMIES_PER_WAVE { get { return getValue(_ID_LEVEL2_MAX_ENEMIES_PER_WAVE); } }
		private const int _ID_LEVEL2_TIME_BETWEEN_WAVES = -1947473861;
		public static float ID_LEVEL2_TIME_BETWEEN_WAVES { get { return getValue(_ID_LEVEL2_TIME_BETWEEN_WAVES); } }
		private const int _ID_LEVEL2_BETWEEN_EACH_UNIQUE_SPAWN_CD = 1960400978;
		public static float ID_LEVEL2_BETWEEN_EACH_UNIQUE_SPAWN_CD { get { return getValue(_ID_LEVEL2_BETWEEN_EACH_UNIQUE_SPAWN_CD); } }
		private const int _ID_LEVEL3_MAX_ENEMIES_PER_WAVE = 1689031491;
		public static float ID_LEVEL3_MAX_ENEMIES_PER_WAVE { get { return getValue(_ID_LEVEL3_MAX_ENEMIES_PER_WAVE); } }
		private const int _ID_LEVEL3_TIME_BETWEEN_WAVES = 1332734548;
		public static float ID_LEVEL3_TIME_BETWEEN_WAVES { get { return getValue(_ID_LEVEL3_TIME_BETWEEN_WAVES); } }
		private const int _ID_LEVEL3_BETWEEN_EACH_UNIQUE_SPAWN_CD = -255433978;
		public static float ID_LEVEL3_BETWEEN_EACH_UNIQUE_SPAWN_CD { get { return getValue(_ID_LEVEL3_BETWEEN_EACH_UNIQUE_SPAWN_CD); } }
		private const int _ID_LEVEL4_MAX_ENEMIES_PER_WAVE = 1657447483;
		public static float ID_LEVEL4_MAX_ENEMIES_PER_WAVE { get { return getValue(_ID_LEVEL4_MAX_ENEMIES_PER_WAVE); } }
		private const int _ID_LEVEL4_TIME_BETWEEN_WAVES = -1098738207;
		public static float ID_LEVEL4_TIME_BETWEEN_WAVES { get { return getValue(_ID_LEVEL4_TIME_BETWEEN_WAVES); } }
		private const int _ID_LEVEL4_BETWEEN_EACH_UNIQUE_SPAWN_CD = -829135215;
		public static float ID_LEVEL4_BETWEEN_EACH_UNIQUE_SPAWN_CD { get { return getValue(_ID_LEVEL4_BETWEEN_EACH_UNIQUE_SPAWN_CD); } }
		private const int _ID_LEVEL5_MAX_ENEMIES_PER_WAVE = 2081418686;
		public static float ID_LEVEL5_MAX_ENEMIES_PER_WAVE { get { return getValue(_ID_LEVEL5_MAX_ENEMIES_PER_WAVE); } }
		private const int _ID_LEVEL5_TIME_BETWEEN_WAVES = -222004833;
		public static float ID_LEVEL5_TIME_BETWEEN_WAVES { get { return getValue(_ID_LEVEL5_TIME_BETWEEN_WAVES); } }
		private const int _ID_LEVEL5_BETWEEN_EACH_UNIQUE_SPAWN_CD = -7039306;
		public static float ID_LEVEL5_BETWEEN_EACH_UNIQUE_SPAWN_CD { get { return getValue(_ID_LEVEL5_BETWEEN_EACH_UNIQUE_SPAWN_CD); } }
	}
}