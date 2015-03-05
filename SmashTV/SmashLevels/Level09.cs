using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level09 : Arena {

        public Level09() {
            _secondsLeft = 60;
            Level = 9;

            WaveSpawner wave;
            for (int i = 0; i < 60; i++) {
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(6, 2 + (int)(i * 0.39f)));
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(4, 2 + (int)(i * 0.39f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 1.77f);
                criteria.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            for (int i = 0; i < 5;++i ) {
                wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 7);
                var wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 9);
                var criteria = new SpawnCriteria();
                var criteria2 = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 10 + 9.23f);
                criteria2.MinSecondsInArena = (int)(i * 10 + 10.23f);
                criteria.MaxEnemiesAlive = criteria2.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria2);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 6;
            _spawners[5].SpawnCount = 5;

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 49, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 49, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}

var waves = [];
for(var i=0; i < 40; i++) 
{
    var wave = 
    {
        EnemyType: 0,
        SpawnCount: 0,
        Criteria: {
            MaxEnemiesAlive: 50,
            MinSecondsInArena: Math.floor(i * 1.77),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };

if(i %2 == 0) {
    wave.EnemyType = 2;
    wave.SpawnCount = Math.floor(Math.min(6, 2 + i * 0.39));
} else {
    wave.EnemyType = 0;
    wave.SpawnCount = Math.floor(Math.min(4, 2 + i * 0.39));
}
    waves.push(wave);
}

for (var i = 0; i < 5;++i ) {
    var wave = 
    {
        EnemyType: 0,
        SpawnCount: 0,
        Criteria: {
            MaxEnemiesAlive: 50,
            MinSecondsInArena: Math.floor(i * 10 + 9.23),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };

    var wave2 = 
    {
        EnemyType: 0,
        SpawnCount: 0,
        Criteria: {
            MaxEnemiesAlive: 50,
            MinSecondsInArena: Math.floor(i * 10 + 10.23),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };

    wave.EnemyType = 1;
    wave.SpawnCount = 7;
    wave2.EnemyType = 1;
    wave2.SpawnCount = 9;

}


waves[0].SpawnCount = 9;
waves[1].SpawnCount = 9;
waves[4].SpawnCount = 6;
waves[5].SpawnCount = 5;

waves.push({
            EnemyType: 1,
            SpawnCount: 4,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 49,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 
waves.push({
            EnemyType: 2,
            SpawnCount: 4,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 49,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 

var container = {
    Level: 9,
    Time: 60,
    Waves: waves
}
document.write(JSON.stringify(container));

{"Level":9,"Time":60,"Waves":[{"EnemyType":2,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":1,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":3,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":5,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":7,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":8,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":10,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":12,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":14,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":15,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":17,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":19,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":21,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":24,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":26,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":28,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":30,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":31,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":35,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":37,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":38,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":40,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":42,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":44,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":47,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":51,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":53,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":54,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":56,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":58,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":60,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":61,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":63,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":65,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":67,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":50,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}

