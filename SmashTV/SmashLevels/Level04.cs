using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level04 : Arena {

        public Level04() {
            _secondsLeft = 60;
            Level = 4;

            for (int i = 0; i < 30; i++) {
                var wave = new WaveSpawner((int)EnemyTypes.Types.Range, 2 + Math.Min(2, (int)(i * 0.45f)));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.483f);
                criteria.MaxEnemiesAlive = 13;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 12;
            _spawners[1].SpawnCount = 3;
            _spawners[4].SpawnCount = 9;
            _spawners[7].SpawnCount = 6;

            var end3 = new WaveSpawner((int)EnemyTypes.Types.Range, 8);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 18 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 52, MaxEnemiesAlive = 30 });

            _spawners.Add(end3);
            _spawners.Add(end4);
        }
    }
}


var waves = [];
for(var i=0; i < 30; i++) 
{
    waves.push( {
            EnemyType: 2,
            SpawnCount: 2 + Math.min(2, Math.floor(i*0.45)),
            Criteria: {
                MaxEnemiesAlive: 13,
                MinSecondsInArena: Math.floor(i * 2.483),
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });
}



waves[0].SpawnCount = 12;
waves[1].SpawnCount = 3;
waves[4].SpawnCount = 9;
waves[7].SpawnCount = 6;


waves.push({
            EnemyType: 2,
            SpawnCount: 8,
            Criteria: {
                MaxEnemiesAlive: 18,
                MinSecondsInArena: 43,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });	
waves.push({
            EnemyType: 2,
            SpawnCount: 6,
            Criteria: {
                MaxEnemiesAlive: 30,
                MinSecondsInArena: 52,
                MinTotalEnemiesKilled: -1,
                WaveRepeatableCount: 0,
                SecondsBetweenRepeat: 1                
            }
    });	





var container = {
    Level: 4,
    Time: 60,
    Waves: waves
}
document.write(JSON.stringify(container));


{"Level":4,"Time":60,"Waves":[{"EnemyType":2,"SpawnCount":12,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":2,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":4,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":7,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":9,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":12,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":14,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":17,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":19,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":22,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":24,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":27,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":29,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":34,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":37,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":39,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":42,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":44,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":47,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":54,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":57,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":64,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":67,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":13,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":18,"MinSecondsInArena":43,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}
