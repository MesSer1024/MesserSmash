using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MesserSmash.Modules {
	public static class DataDefines {
		private static float getValue(int hash) { return (float)SmashDb.get(hash); }
		private const int _ID_MELEE_ENEMY_DAMAGE = -1054125204;
		public static float ID_MELEE_ENEMY_DAMAGE { get { return getValue(_ID_MELEE_ENEMY_DAMAGE); } }
		private const int _ID_MELEE_ENEMY_RADIUS = 1496791282;
		public static float ID_MELEE_ENEMY_RADIUS { get { return getValue(_ID_MELEE_ENEMY_RADIUS); } }
		private const int _ID_RUSHER_DAMAGE = -1847579432;
		public static float ID_RUSHER_DAMAGE { get { return getValue(_ID_RUSHER_DAMAGE); } }
		private const int _ID_RUSHER_RADIUS = -1895701796;
		public static float ID_RUSHER_RADIUS { get { return getValue(_ID_RUSHER_RADIUS); } }
		private const int _ID_RUSHER_ATTACK_RADIUS = -1561555024;
		public static float ID_RUSHER_ATTACK_RADIUS { get { return getValue(_ID_RUSHER_ATTACK_RADIUS); } }
		private const int _ID_RANGE_DAMAGE = -1261140252;
		public static float ID_RANGE_DAMAGE { get { return getValue(_ID_RANGE_DAMAGE); } }
		private const int _ID_RANGE_RADIUS = 346120256;
		public static float ID_RANGE_RADIUS { get { return getValue(_ID_RANGE_RADIUS); } }
		private const int _ID_RANGE_ATTACK_RADIUS = -525028114;
		public static float ID_RANGE_ATTACK_RADIUS { get { return getValue(_ID_RANGE_ATTACK_RADIUS); } }
		private const int _ID_RANGE2_DAMAGE = -1300324153;
		public static float ID_RANGE2_DAMAGE { get { return getValue(_ID_RANGE2_DAMAGE); } }
		private const int _ID_RANGE2_RADIUS = -1029815875;
		public static float ID_RANGE2_RADIUS { get { return getValue(_ID_RANGE2_RADIUS); } }
		private const int _ID_RANGE2_ATTACK_RADIUS = 1549272162;
		public static float ID_RANGE2_ATTACK_RADIUS { get { return getValue(_ID_RANGE2_ATTACK_RADIUS); } }
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