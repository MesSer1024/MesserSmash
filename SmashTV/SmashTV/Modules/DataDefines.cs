using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MesserSmash.Modules {
	public static class DataDefines {
		private static float getValue(int hash) { return (float)SmashDb.get(hash); }
		private static void setValue(int hash, float value) { SmashDb.set(hash, value); }
		private const int _ID_SETTINGS_DRAW_ATTACK_RADIUS = 2038939980;
		public static float ID_SETTINGS_DRAW_ATTACK_RADIUS { get { return getValue(_ID_SETTINGS_DRAW_ATTACK_RADIUS); } set { setValue(_ID_SETTINGS_DRAW_ATTACK_RADIUS, value); } }
		private const int _ID_SETTINGS_PLAY_MUSIC = -1732770757;
		public static float ID_SETTINGS_PLAY_MUSIC { get { return getValue(_ID_SETTINGS_PLAY_MUSIC); } set { setValue(_ID_SETTINGS_PLAY_MUSIC, value); } }
		private const int _ID_SETTINGS_PLAY_SOUND = -2014414066;
		public static float ID_SETTINGS_PLAY_SOUND { get { return getValue(_ID_SETTINGS_PLAY_SOUND); } set { setValue(_ID_SETTINGS_PLAY_SOUND, value); } }
		private const int _ID_STATE_ENEMIES_ALIVE = 299569211;
		public static float ID_STATE_ENEMIES_ALIVE { get { return getValue(_ID_STATE_ENEMIES_ALIVE); } set { setValue(_ID_STATE_ENEMIES_ALIVE, value); } }
		private const int _ID_STATE_ENEMIES_KILLED = 97071304;
		public static float ID_STATE_ENEMIES_KILLED { get { return getValue(_ID_STATE_ENEMIES_KILLED); } set { setValue(_ID_STATE_ENEMIES_KILLED, value); } }
		private const int _ID_MELEE_ENEMY_DAMAGE = -1054125204;
		public static float ID_MELEE_ENEMY_DAMAGE { get { return getValue(_ID_MELEE_ENEMY_DAMAGE); } set { setValue(_ID_MELEE_ENEMY_DAMAGE, value); } }
		private const int _ID_MELEE_ENEMY_RADIUS = 1496791282;
		public static float ID_MELEE_ENEMY_RADIUS { get { return getValue(_ID_MELEE_ENEMY_RADIUS); } set { setValue(_ID_MELEE_ENEMY_RADIUS, value); } }
		private const int _ID_RUSHER_DAMAGE = -1847579432;
		public static float ID_RUSHER_DAMAGE { get { return getValue(_ID_RUSHER_DAMAGE); } set { setValue(_ID_RUSHER_DAMAGE, value); } }
		private const int _ID_RUSHER_RADIUS = -1895701796;
		public static float ID_RUSHER_RADIUS { get { return getValue(_ID_RUSHER_RADIUS); } set { setValue(_ID_RUSHER_RADIUS, value); } }
		private const int _ID_RUSHER_ATTACK_RADIUS = -1561555024;
		public static float ID_RUSHER_ATTACK_RADIUS { get { return getValue(_ID_RUSHER_ATTACK_RADIUS); } set { setValue(_ID_RUSHER_ATTACK_RADIUS, value); } }
		private const int _ID_RANGE_DAMAGE = -1261140252;
		public static float ID_RANGE_DAMAGE { get { return getValue(_ID_RANGE_DAMAGE); } set { setValue(_ID_RANGE_DAMAGE, value); } }
		private const int _ID_RANGE_RADIUS = 346120256;
		public static float ID_RANGE_RADIUS { get { return getValue(_ID_RANGE_RADIUS); } set { setValue(_ID_RANGE_RADIUS, value); } }
		private const int _ID_RANGE_ATTACK_RADIUS = -525028114;
		public static float ID_RANGE_ATTACK_RADIUS { get { return getValue(_ID_RANGE_ATTACK_RADIUS); } set { setValue(_ID_RANGE_ATTACK_RADIUS, value); } }
		private const int _ID_RANGE2_DAMAGE = -1300324153;
		public static float ID_RANGE2_DAMAGE { get { return getValue(_ID_RANGE2_DAMAGE); } set { setValue(_ID_RANGE2_DAMAGE, value); } }
		private const int _ID_RANGE2_RADIUS = -1029815875;
		public static float ID_RANGE2_RADIUS { get { return getValue(_ID_RANGE2_RADIUS); } set { setValue(_ID_RANGE2_RADIUS, value); } }
		private const int _ID_RANGE2_ATTACK_RADIUS = 1549272162;
		public static float ID_RANGE2_ATTACK_RADIUS { get { return getValue(_ID_RANGE2_ATTACK_RADIUS); } set { setValue(_ID_RANGE2_ATTACK_RADIUS, value); } }
	}
}