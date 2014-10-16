//From codebase
var EnemyTypes = {
	Melee : 0,
	Range : 1,
	Charger : 2
}

//'global' variables
var waves = [];

function Utils() {
	this.addExpandHandler = function() {
		// bind a click-handler to the 'tr' elements with the 'header' class-name:
		$('tr.header').click(function(){
			/* get all the subsequent 'tr' elements until the next 'tr.header',
			   set the 'display' property to 'none' (if they're visible), to 'table-row'
			   if they're not: */
			$(this).nextUntil('tr.header').css('display', function(i,v){
				return this.style.display === 'table-row' ? 'none' : 'table-row';
			});
		});
	};
	
	this.visualizeWaves = function(waves) {
		function createDropdown(selectedType) {
			var s = "<select name=\"foobar\">";
			for(var i in EnemyTypes) {
				if( EnemyTypes[i] === selectedType )
					s += "<option selected>" + i + "</option>";
				else
					s += "<option>" + i + "</option>";
			}
			s += "</select>";
			return s;
		}
		
		function visualizeWave(waveData) {
			var waveRow = "<tr class=\"header\"><td colspan=\"2\" style=\"background-color: #acacac\">{data}</td></tr>";
			var typeRow = "<tr><td>Enemy Type:</td><td>{data}</td></tr>";
			var spawnRow = "<tr><td>Spawn count:</td><td><input value=\"{data}\" /></td></tr>";
			var criteriaMaxEnemies = "<tr><td>Max enemies alive:</td><td><input value=\"{data}\" /></td></tr>";
			var criteriaMinSeconds = "<tr><td>Min seconds in arena:</td><td><input value=\"{data}\" /></td></tr>";
			var criteriaMinKills = "<tr><td>Min total enemies killed:</td><td><input value=\"{data}\" /></td></tr>";
			
			waveRow = waveRow.replace("{data}", "Wave" + $('tr.header').length);
			typeRow = typeRow.replace("{data}", createDropdown(waveData.EnemyType));
			spawnRow = spawnRow.replace("{data}", waveData.SpawnCount);
			criteriaMaxEnemies = criteriaMaxEnemies.replace("{data}", waveData.Criteria.MaxEnemiesAlive);
			criteriaMinSeconds = criteriaMinSeconds.replace("{data}", waveData.Criteria.MinSecondsInArena);
			criteriaMinKills = criteriaMinKills.replace("{data}", waveData.Criteria.MinTotalEnemiesKilled);
			
			$("#levels").append(waveRow);
			$("#levels").append(typeRow);
			$("#levels").append(spawnRow);
			$("#levels").append(criteriaMaxEnemies);
			$("#levels").append(criteriaMinSeconds);
			$("#levels").append(criteriaMinKills);
		}
		
		for(var i=0; i < waves.length; ++i) {
			visualizeWave(waves[i]);
		}
	};
}

window.onload = function() {
	function SpawnCriteria() {
        this.MaxEnemiesAlive = -1;
        this.MinSecondsInArena = -1;
        this.MinTotalEnemiesKilled = -1;
        this.WaveRepeatableCount = 0;
        this.SecondsBetweenRepeat = 1;
	}; 

	function WaveSpawner(enemyType, amount) {
	   this.EnemyType = enemyType;
	   this.SpawnCount = amount;
	   this.Criteria = new SpawnCriteria();
	};

	waves.push(new WaveSpawner(EnemyTypes.Melee, 37));
	waves.push(new WaveSpawner(EnemyTypes.Range, 21));

	new Utils().addExpandHandler();
	new Utils().visualizeWaves(waves);
}
