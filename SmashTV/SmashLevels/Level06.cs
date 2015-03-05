using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
using System.Collections.Generic;
namespace MesserSmash.Arenas {
    public class Level06 : Arena {

        public Level06() {
            _secondsLeft = 60;
            Level = 6;

            WaveSpawner wave;
            WaveSpawner wave2;
            for (int i = 0; i < 40; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.57f);
                criteria.MaxEnemiesAlive = 35;
                wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(3, 3 + (int)(i * 0.09f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 1 + (int)(i * 0.09f));
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 6;
            _spawners[1].SpawnCount = 5;

            for (int i = 0; i < 6; ++i) {
                var every10Second = new WaveSpawner(0, 17);
                every10Second.CustomSpawnCommand = onCustomSpawn;
                every10Second.addCriteria(new SpawnCriteria() { MinSecondsInArena = 11 * i });
                _spawners.Add(every10Second);
            }

            var middle = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 33 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 5);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 5);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }

        private void onCustomSpawn(WaveSpawner spawner, List<Spawnpoint> spawnpoints) {
            spawner.CustomSpawnCommand = null;
            var sp = spawnpoints[Utils.randomInt(spawnpoints.Count)];
            sp.generateSecondaryMeleeUnits(spawner.SpawnCount);
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
            MaxEnemiesAlive: 35,
            MinSecondsInArena: Math.floor(i * 2.57),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };
    
    wave.EnemyType = 0;
    wave.SpawnCount = Math.floor(Math.min(3, 3 + i * 0.09));
    waves.push(wave);

    var wave2 = 
    {
        EnemyType: 0,
        SpawnCount: 0,
        Criteria: {
            MaxEnemiesAlive: 35,
            MinSecondsInArena: Math.floor(i * 2.57),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };
    
    wave2.EnemyType = 1;
    wave2.SpawnCount = Math.floor(Math.min(1 + i * 0.09));
    waves.push(wave2);
}



waves[0].SpawnCount = 6;
waves[1].SpawnCount = 5;

waves.push({
            EnemyType: 2,
            SpawnCount: 4,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 33,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 
waves.push({
            EnemyType: 2,
            SpawnCount: 4,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 36,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 


waves.push({
            EnemyType: 1,
            SpawnCount: 5,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 51,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 
waves.push({
            EnemyType: 2,
            SpawnCount: 5,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 51,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 

for(i=0; i < 6; i++) {
waves.push({
            EnemyType: 0,
            SpawnCount: 17,
            SameSpawnpoint: true,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 11*i,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 
}

var container = {
    Level: 6,
    Time: 60,
    Waves: waves
}
document.write(JSON.stringify(container));

{"Level":6,"Time":60,"Waves":[{"EnemyType":0,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":2,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":2,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":5,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":5,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":7,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":7,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":10,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":10,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":12,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":12,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":15,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":15,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":17,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":17,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":20,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":20,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":25,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":25,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":28,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":1,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":28,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":30,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":30,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":35,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":35,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":38,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":38,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":41,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":41,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":43,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":43,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":48,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":48,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":51,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":51,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":53,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":53,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":56,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":56,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":61,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":61,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":64,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":64,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":66,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":66,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":71,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":71,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":74,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":74,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":77,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":77,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":79,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":79,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":82,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":82,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":84,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":84,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":87,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":87,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":89,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":89,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":92,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":92,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":95,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":95,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":97,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":97,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":100,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":100,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":36,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":51,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":51,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":11,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":22,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":44,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}

