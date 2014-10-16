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
		$('tr.header').mousedown(function(event) {
			switch (event.which) {
				case 1:
					$(this).nextUntil('tr.header').css('display', function(i,v){
						return this.style.display === 'table-row' ? 'none' : 'table-row';
					});
					break;
				case 2:
					alert('Middle Mouse button pressed.');
					break;
				case 3:
					var idx = this.getAttribute("data-index");
					console.log("index=" + idx + ", isNaN?" + isNaN(idx));
					if(!isNaN(idx)) {
						waves.splice(idx, 1);
						console.log("removed item with index=" + idx);
					}
					new Utils().visualizeWaves(waves);
					break;
				default:
					alert('You have a strange Mouse!');
					break;
			}
		});
	};
	
	this.removeAllVisualWaves = function() {
		$("#levels").find("tr:gt(2)").remove();
	}
	
	this.visualizeWaves = function(waves) {
		this.removeAllVisualWaves();
	
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
		
		function visualizeWave(waveData, index) {
			var waveRow = "<tr class=\"header\" data-index=\"{index}\"><td class=\"header\" colspan=2>{data}</td></tr>";
			var typeRow = "<tr><td>Enemy Type:</td><td>{data}</td></tr>";
			var spawnRow = "<tr><td>Spawn count:</td><td><input value=\"{data}\" /></td></tr>";
			var criteriaMaxEnemies = "<tr><td>Max enemies alive:</td><td><input value=\"{data}\" /></td></tr>";
			var criteriaMinSeconds = "<tr><td>Min seconds in arena:</td><td><input value=\"{data}\" /></td></tr>";
			var criteriaMinKills = "<tr><td>Min total enemies killed:</td><td><input value=\"{data}\" /></td></tr>";
			
			waveRow = waveRow.replace("{data}", "Wave" + $('tr.header').length);
			waveRow = waveRow.replace("{index}", index);
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
			visualizeWave(waves[i], i);
		}
		this.addExpandHandler();
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

	new Utils().visualizeWaves(waves);
	new Utils().addExpandHandler();
	
	$('#generate').click(function(){
		var str = JSON.stringify(waves);
		console.log(str);
		$('#output').val(str);
	});

	$('#addWave').click(function(){
		var old = waves[waves.length - 1];
		var clone = new WaveSpawner(old.EnemyType, old.SpawnCount);
		clone.Criteria.MaxEnemiesAlive = old.Criteria.MaxEnemiesAlive;
        clone.Criteria.MinSecondsInArena = clone.Criteria.MinSecondsInArena;
        clone.Criteria.MinTotalEnemiesKilled = clone.Criteria.MinTotalEnemiesKilled;
        clone.Criteria.WaveRepeatableCount = clone.Criteria.WaveRepeatableCount;
        clone.Criteria.SecondsBetweenRepeat = clone.Criteria.SecondsBetweenRepeat;
		
		waves.push(clone);
		new Utils().visualizeWaves(waves);
	});
}
