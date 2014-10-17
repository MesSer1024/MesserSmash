//From codebase
var EnemyTypes = {
	Melee : 0,
	Range : 1,
	Charger : 2
}

//'global' variables
var waves = [];

function WebSocketTest() {
	var SERVER_URL = "ws://localhost:6677";
	
	if ("WebSocket" in window) {
		// Let us open a web socket
		console.log("Attempting to connect to websocket server on URL:" + SERVER_URL);
		var ws = new WebSocket(SERVER_URL);
		ws.onopen = function() {
			// Web Socket is connected, send data using send()
			ws.send("Message to send");
			alert("Message is sent...");
		};
		ws.onmessage = function (evt) { 
			var received_msg = evt.data;
			alert("Message is received...");
		};
		ws.onclose = function() { 
			// websocket is closed.
			alert("Connection is closed..."); 
		};
	} else {
		// The browser doesn't support WebSocket
		alert("WebSocket NOT supported by your Browser!");
	}
}

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
		
		$(".enemy_type").change(function() {
			var idx = this.getAttribute("data-index");
			waves[idx].EnemyType = this.selectedIndex;
		});
		$(".spawn_count").change(function() {
			var idx = this.getAttribute("data-index");
			var value = $(this).val();
			waves[idx].SpawnCount = value;
		});
		$(".max_enemies").change(function() {
			var idx = this.getAttribute("data-index");
			var value = $(this).val();
			waves[idx].Criteria.MaxEnemiesAlive = value;
		});
		$(".min_seconds").change(function() {
			var idx = this.getAttribute("data-index");
			var value = $(this).val();
			console.log(value);
			waves[idx].Criteria.MinSecondsInArena = value;
			console.log(waves[idx].Criteria.MinSecondsInArena);
		});
		$(".min_kills").change(function() {
			var idx = this.getAttribute("data-index");
			var value = $(this).val();
			console.log(this);
			waves[idx].Criteria.MinTotalEnemiesKilled = value;
			console.log(this);
		});
	};
	
	this.removeAllVisualWaves = function() {
		$("#levels").find("tr:gt(2)").remove();
	}
	
	this.visualizeWaves = function(waves) {
		this.removeAllVisualWaves();
	
		function createDropdown(selectedType) {
			var s = "<select class=\"enemy_type\" data-index=\"{index}\">";
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
			var spawnRow = "<tr><td>Spawn count:</td><td><input class=\"spawn_count\" data-index=\"{index}\" value=\"{data}\" /></td></tr>";
			var criteriaMaxEnemies = "<tr><td>Max enemies alive:</td><td><input class=\"max_enemies\" data-index=\"{index}\" value=\"{data}\" /></td></tr>";
			var criteriaMinSeconds = "<tr><td>Min seconds in arena:</td><td><input class=\"min_seconds\" data-index=\"{index}\" value=\"{data}\" /></td></tr>";
			var criteriaMinKills = "<tr><td>Min total enemies killed:</td><td><input class=\"min_kills\" data-index=\"{index}\" value=\"{data}\" /></td></tr>";
			
			waveRow = waveRow.replace("{data}", "Wave" + $('tr.header').length);
			waveRow = waveRow.replace("{index}", index);
			typeRow = typeRow.replace("{data}", createDropdown(waveData.EnemyType));
			typeRow = typeRow.replace("{index}", index);
			spawnRow = spawnRow.replace("{data}", waveData.SpawnCount);
			spawnRow = spawnRow.replace("{index}", index);
			criteriaMaxEnemies = criteriaMaxEnemies.replace("{data}", waveData.Criteria.MaxEnemiesAlive);
			criteriaMaxEnemies = criteriaMaxEnemies.replace("{index}", index);
			criteriaMinSeconds = criteriaMinSeconds.replace("{data}", waveData.Criteria.MinSecondsInArena);
			criteriaMinSeconds = criteriaMinSeconds.replace("{index}", index);
			criteriaMinKills = criteriaMinKills.replace("{data}", waveData.Criteria.MinTotalEnemiesKilled);
			criteriaMinKills = criteriaMinKills.replace("{index}", index);
			
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

$(function() {
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
		$('#output').val(str);
	});

	$('#sendToGame').click(function(){
		var str = $("#output").val();
		console.log(str);
	});

	$('#addWave').click(function(){
		var old = waves[waves.length - 1];
		var clone = new WaveSpawner(old.EnemyType, old.SpawnCount);
		clone.Criteria.MaxEnemiesAlive = old.Criteria.MaxEnemiesAlive;
        clone.Criteria.MinSecondsInArena = old.Criteria.MinSecondsInArena;
        clone.Criteria.MinTotalEnemiesKilled = old.Criteria.MinTotalEnemiesKilled;
        clone.Criteria.WaveRepeatableCount = old.Criteria.WaveRepeatableCount;
        clone.Criteria.SecondsBetweenRepeat = old.Criteria.SecondsBetweenRepeat;
		
		waves.push(clone);
		new Utils().visualizeWaves(waves);
	});
	
	$("#connect").click(function() {
		WebSocketTest();
	});
});
