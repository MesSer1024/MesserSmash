using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level07 : Arena {

        public Level07() {
            _secondsLeft = 60;
            Level = 7;

            WaveSpawner wave, wave2;
            for (int i = 0; i < 40; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 3.47f);
                criteria.MaxEnemiesAlive = 30;
                wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(4, 2 + (int)(i * 0.09f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, Math.Min(8, 5 + (int)(i * 0.09f)));
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 5;
            _spawners[5].SpawnCount = 4;
            _spawners[8].SpawnCount = 10;
            _spawners[9].SpawnCount = 11;

            var middle = new WaveSpawner((int)EnemyTypes.Types.Melee, 17);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 32 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 43 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 8);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 55, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 8);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 54, MaxEnemiesAlive = 35 });

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
            MaxEnemiesAlive: 30,
            MinSecondsInArena: Math.floor(i * 3.47),
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
            MaxEnemiesAlive: 30,
            MinSecondsInArena: Math.floor(i * 3.47),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };
    
    wave.EnemyType = 2;
    wave.SpawnCount = Math.floor(Math.min(4, 2 + i * 0.09));
    waves.push(wave);
	wave2.EnemyType = 1;
	wave2.SpawnCount = Math.floor(Math.min(8, 5 + i * 0.09));
    	waves.push(wave2);
}



waves[0].SpawnCount = 9;
waves[1].SpawnCount = 9;
waves[4].SpawnCount = 5;
waves[5].SpawnCount = 4;
waves[8].SpawnCount = 10;
waves[9].SpawnCount = 11;

waves.push({
            EnemyType: 0,
            SpawnCount: 17,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 32,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 
waves.push({
            EnemyType: 2,
            SpawnCount: 6,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 43,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 


waves.push({
            EnemyType: 1,
            SpawnCount: 8,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 55,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 
waves.push({
            EnemyType: 2,
            SpawnCount: 8,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 54,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    }); 

var container = {
    Level: 7,
    Time: 60,
    Waves: waves
}
document.write(JSON.stringify(container));

{"Level":7,"Time":60,"Waves":[{"EnemyType":2,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":3,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":3,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":6,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":6,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":10,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":10,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":10,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":13,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":11,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":13,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":17,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":17,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":20,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":20,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":24,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":24,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":27,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":27,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":31,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":31,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":34,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":34,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":38,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":38,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":41,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":41,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":45,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":45,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":48,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":48,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":58,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":58,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":65,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":65,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":76,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":76,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":79,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":79,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":83,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":83,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":86,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":86,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":90,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":90,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":93,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":93,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":97,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":97,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":100,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":100,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":104,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":104,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":107,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":107,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":111,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":111,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":114,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":114,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":117,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":117,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":121,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":121,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":124,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":124,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":128,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":128,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":131,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":131,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":135,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":135,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":17,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":43,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":54,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}
