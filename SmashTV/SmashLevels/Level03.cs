﻿using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level03 : Arena {

        public Level03() {
            _secondsLeft = 60;
            Level = 3;
            WaveSpawner wave;
            for (int i = 0; i < 60; i++) {
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(4, 3 + (int)(i * 0.109f)));
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(5, 3 + (int)(i * 0.09f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.37f);
                criteria.MaxEnemiesAlive = 35;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 7;
            _spawners[5].SpawnCount = 8;

            var middle = new WaveSpawner((int)EnemyTypes.Types.Melee, 6);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 37 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 3);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.Melee, 2);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 3);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}


var waves = [];
for(var i=0; i < 60; i++) 
{
    waves.push( {
            EnemyType: 0,
            SpawnCount: 0,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: Math.floor(i * 2.37),
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });
    
   	var isCharger = i % 2 == 0 ? true : false;
   	if(isCharger) {
      waves[i].EnemyType = 2;
      waves[i].SpawnCount = Math.floor(Math.min(4, 3 + i * 0.109));
   	} else {
	waves[i].EnemyType = 0;
	waves[i].SpawnCount = Math.floor(Math.min(4, 3 + i * 0.09));
   	}
}



waves[0].SpawnCount = 9;
waves[1].SpawnCount = 9;
waves[4].SpawnCount = 7;
waves[5].SpawnCount = 8;


waves.push({
            EnemyType: 0,
            SpawnCount: 6,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 37,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });	
waves.push({
            EnemyType: 1,
            SpawnCount: 3,
            Criteria: {
                MaxEnemiesAlive: -1,
                MinSecondsInArena: 36,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });	


waves.push({
            EnemyType: 0,
            SpawnCount: 2,
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
            SpawnCount: 3,
            Criteria: {
                MaxEnemiesAlive: 35,
                MinSecondsInArena: 46,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });	





var container = {
    Level: 3,
    Time: 60,
    Waves: waves
}
document.write(JSON.stringify(container));


{"Level":3,"Time":60,"Waves":[{"EnemyType":2,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":2,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":4,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":7,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":9,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":11,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":14,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":16,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":18,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":21,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":26,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":28,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":30,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":33,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":35,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":37,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":40,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":42,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":45,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":47,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":54,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":56,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":61,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":63,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":66,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":68,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":71,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":73,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":75,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":78,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":80,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":82,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":85,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":87,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":90,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":92,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":94,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":97,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":99,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":101,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":104,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":106,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":109,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":111,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":113,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":116,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":118,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":120,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":123,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":125,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":127,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":130,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":132,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":135,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":137,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":139,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":37,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":-1,"MinSecondsInArena":36,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":51,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":35,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}
