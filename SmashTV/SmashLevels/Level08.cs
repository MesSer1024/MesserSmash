using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level08 : Arena {

        public Level08() {
            _secondsLeft = 60;
            Level = 8;

            WaveSpawner wave1, wave2, wave3;
            for (int i = 0; i < 25; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 3.29f);
                criteria.MaxEnemiesAlive = 30;
                wave1 = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(8, 2 + (int)(i * 0.49f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, Math.Min(3, 2 + (int)(i * 0.09f)));
                wave3 = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(3, 2 + (int)(i * 0.09f)));
                wave1.addCriteria(criteria);
                wave2.addCriteria(criteria);
                wave3.addCriteria(criteria);
                _spawners.Add(wave1);
                _spawners.Add(wave2);
                _spawners.Add(wave3);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 8;
            _spawners[2].SpawnCount = 8;
            _spawners[9].SpawnCount = 4;
            _spawners[10].SpawnCount = 8;
            _spawners[11].SpawnCount = 3;
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
            MinSecondsInArena: Math.floor(i * 3.29),
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
            MinSecondsInArena: Math.floor(i * 3.29),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };

    var wave3 = 
    {
        EnemyType: 0,
        SpawnCount: 0,
        Criteria: {
            MaxEnemiesAlive: 30,
            MinSecondsInArena: Math.floor(i * 3.29),
            MinTotalEnemiesKilled: -1,
            WaveRepeatableCount: 0,
            SecondsBetweenRepeat: 1                
        }
    };
    
    wave.EnemyType = 0;
    wave.SpawnCount = Math.floor(Math.min(8, 2 + i * 0.49));
    waves.push(wave);
	wave2.EnemyType = 1;
	wave2.SpawnCount = Math.floor(Math.min(3, 2 + i * 0.09));
	waves.push(wave2);
	wave3.EnemyType = 2;
	wave3.SpawnCount = Math.floor(Math.min(3, 2 + i * 0.09));
	waves.push(wave3);
}



waves[0].SpawnCount = 9;
waves[1].SpawnCount = 8;
waves[2].SpawnCount = 8;
waves[9].SpawnCount = 4;
waves[10].SpawnCount = 8;
waves[11].SpawnCount = 3;

/*
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
    }); */

var container = {
    Level: 8,
    Time: 60,
    Waves: waves
}
document.write(JSON.stringify(container));

{"Level":8,"Time":60,"Waves":[{"EnemyType":0,"SpawnCount":9,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":0,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":3,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":3,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":3,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":6,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":6,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":6,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":9,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":9,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":9,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":13,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":13,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":13,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":16,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":16,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":16,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":4,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":19,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":19,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":19,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":23,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":5,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":26,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":26,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":26,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":29,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":29,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":29,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":6,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":32,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":36,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":36,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":2,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":36,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":7,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":39,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":39,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":39,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":42,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":42,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":42,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":46,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":49,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":52,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":55,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":59,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":62,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":65,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":65,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":65,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":69,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":72,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":75,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":75,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":75,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":78,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":78,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":78,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":82,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":82,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":82,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":85,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":85,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":85,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":88,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":88,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":88,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":92,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":92,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":92,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":95,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":95,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":95,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":98,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":98,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":98,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":101,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":101,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":101,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":105,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":105,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":105,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":108,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":108,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":108,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":111,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":111,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":111,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":115,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":115,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":115,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":118,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":118,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":118,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":121,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":121,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":121,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":125,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":125,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":125,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":0,"SpawnCount":8,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":128,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":1,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":128,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}},{"EnemyType":2,"SpawnCount":3,"Criteria":{"MaxEnemiesAlive":30,"MinSecondsInArena":128,"MinTotalEnemiesKilled":-1,"WaveRepeatableCount":0,"SecondsBetweenRepeat":1}}]}
