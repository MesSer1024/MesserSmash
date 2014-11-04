using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MesserSmash.Modules;

namespace MesserSmash.Arenas
{
    class LevelBuilder
    {
        public static string ExampleData = @"[
{
    Level: 1,
    Time: 60,
    Waves: [
        {
            EnemyType: 0,
            SpawnCount: 30,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: -1,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
        },
        {
            EnemyType: 0,
            SpawnCount: 30,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: -1,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
        }
    ]
},
{
    Level: 2,
    Time: 58,
    Waves: [
        {
            EnemyType: 1,
            SpawnCount: 24,
            Criteria: {
                MaxEnemiesAlive: 1,
                MinSecondsInArena: 2,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
        },
        {
            EnemyType: 2,
            SpawnCount: 30,
            Criteria: {
                MaxEnemiesAlive: 3,
                MinSecondsInArena: 41,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
        }
    ]
}
]";


        public static void setLevelData(string rawData) {
            //var s = LevelBuilder.ExampleData();
            //var foo = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(s);
            //var waves = foo[0]["Waves"];
            //Levels = JsonConvert.DeserializeObject<List<jsLevel>>(LevelBuilder.ExampleData());
            Levels = JsonConvert.DeserializeObject<List<jsLevel>>(rawData);
        }

        public class jsLevel
        {
            public int Level;
            public int Time;
            public List<WaveSpawner> Waves;
        }


        public static List<jsLevel> Levels { get; set; }
    }
    /*
			switch (level) {
				case 1:
					arena = new Level01();
					break;
				case 2:
					arena = new Level02();
					break;
				case 3:
					arena = new Level03();
					break;
				case 4:
					arena = new Level04();
					break;
				case 5:
					arena = new Level05();
                    break;
                case 6:
                    arena = new Level06();
                    break;
                case 7:
                    arena = new Level07();
                    break;
                case 8:
                    arena = new Level08();
                    break;
                case 9:
                    arena = new Level09();
                    break;
                case 10:
                    arena = new Level10(); //BOSS LEVEL!
                    break;
                case 11:
                    arena = new Level11();
                    break;
                case 12:
                    arena = new Level12();
                    break;
                case 13:
                    arena = new Level13();
                    break;
                case 14:
                    arena = new Level14();
                    break;
                case 15:
                    arena = new Level15();
                    break;
                case 16:
                    arena = new Level16();
                    break;
                case 17:
                    arena = new Level17();
                    break;
                case 18:
                    arena = new Level18();
                    break;
                case 19:
                    arena = new Level19();
                    break;
                case 20:
                    arena = new Level20(); //BOSS LEVEL!
                    break;
				default:
					arena = new SpecialLevel();
					Logger.error("unknown level={0}", level);
					break;
			}*/
}
