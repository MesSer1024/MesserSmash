//From codebase
var EnemyTypes = {
	Melee : 0,
	Range : 1,
	Charger : 2
}

//'global' variables
var waves = [];
var waveViews = [];

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
			console.log("asdfsadfsadf");
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

function onWaveClicked(idx) {
	console.log("wave clicked, wave=" + idx);
	var view = waveViews[idx];
	var model = waves[idx];
	view.hide();
}

function onAddWave() {
	var old = waves[waves.length - 1];
	var clone;
	if(old) {
		clone = new WaveSpawner(old.EnemyType, old.SpawnCount);
		clone.Criteria.MaxEnemiesAlive = old.Criteria.MaxEnemiesAlive;
		clone.Criteria.MinSecondsInArena = old.Criteria.MinSecondsInArena;
		clone.Criteria.MinTotalEnemiesKilled = old.Criteria.MinTotalEnemiesKilled;
		clone.Criteria.WaveRepeatableCount = old.Criteria.WaveRepeatableCount;
		clone.Criteria.SecondsBetweenRepeat = old.Criteria.SecondsBetweenRepeat;
	} else {
		clone = new WaveSpawner(EnemyTypes.Melee, 10);
	}

	var idx = waves.length;
	waves.push(clone);
	var wave = new WaveView(waves[idx], idx);
	waveViews.push(wave);
	wave.getContent().appendTo("#waves");
	
	wave.getButton().click(function() {
		onWaveClicked(idx);
	});
}

$(function() {
	$('#generate').click(function(){
		var str = JSON.stringify(waves);
		$('#output').val(str);
	});

	$('#sendToGame').click(function(){
		var str = $("#output").val();
		console.log(str);
	});

	$('#addWave').click(function(){
		onAddWave();
	});
	
	$("#connect").click(function() {
		WebSocketTest();
	});

	var json = {
		SpawnCount: "12",
		MaxEnemiesAlive: "14"
	};

	var template = Handlebars.template(JST["asdf\\WaveSpawnView"])(json);
	console.log(template);

});