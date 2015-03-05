using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level02 : Arena {

        public Level02() {
            _secondsLeft = 60;
            Level = 2;

            for (int i = 0; i < 40; i++) {
                WaveSpawner wave;
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 3);
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 2 + Math.Min(2, (int)(i * 0.29f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.08f);
                criteria.MaxEnemiesAlive = 26;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 8;
            _spawners[1].SpawnCount = 5;
            _spawners[4].SpawnCount = 5;
            _spawners[5].SpawnCount = 5;
            var end3 = new WaveSpawner((int)EnemyTypes.Types.Melee, 4);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 32, MaxEnemiesAlive = 100 });
            var end3_2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 5);
            end3_2.addCriteria(new SpawnCriteria { MinSecondsInArena = 32, MaxEnemiesAlive = 100 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 3);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 35, MaxEnemiesAlive = 100 });
            var end5 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 7);
            end5.addCriteria(new SpawnCriteria { MinSecondsInArena = 48, MaxEnemiesAlive = 100 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Melee, 6);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 50, MaxEnemiesAlive = 100 });

            _spawners.Add(end2);
            _spawners.Add(end3);
            _spawners.Add(end3_2);
            _spawners.Add(end4);
            _spawners.Add(end5);
        }
    }
}



{"Level":2,"Time":60,"Waves":[{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":2,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":4,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":6,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":8,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":10,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":12,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":14,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":16,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":18,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":20,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":22,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":24,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":27,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":29,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":31,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":35,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":37,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":39,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":41,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":43,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":45,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":47,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":54,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":56,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":58,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":60,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":64,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":66,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":68,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":70,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":74,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":76,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":79,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":26,"MinSecondsInArena":81,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":35,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":48,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":50,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}

