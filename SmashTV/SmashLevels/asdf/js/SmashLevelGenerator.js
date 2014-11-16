//From codebase
var EnemyTypes = {
	Melee : 0,
	Range : 1,
	Charger : 2
}

if (!Date.now) {
    Date.now = function() { return new Date().getTime(); };
}

//'global' variables
var data = {
	Level: 1,
	Time: 60,
	Waves : []
};
var waves = [];
var waveViews = [];
var ws;

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
};

function connect() {
	var SERVER_URL = "ws://localhost:6677";
	var status = $("#connection_text");

	if ("WebSocket" in window) {
		// Let us open a web socket
		console.log("Attempting to connect to websocket server on URL:" + SERVER_URL);
		ws = new WebSocket(SERVER_URL);
		ws.onopen = function() {
			// Web Socket is connected, send data using send()
			status.val("Message is sent!");
		};
		ws.onmessage = function (evt) { 
			var received_msg = evt.data;
			status.val("Message received: " + received_msg);
		};
		ws.onclose = function() { 
			// websocket is closed.
			alert("Connection is closed..."); 
			ws = null;
		};
	} else {
		// The browser doesn't support WebSocket
		alert("WebSocket NOT supported by your Browser!");
	}	
};

$(function() {
	$('#addWave').click(function(){
		onAddWave();
	});
	
	$("#connect").click(function() {
		connect();
	});
	
	$("#populate").click(function() {
	/*
		var overlay = $("<div class='overlay'><center></center></div>");
		var item = $("<div></div>");
		console.log(localStorage);
		for (var key in localStorage){
		   console.log(key);
		   item.append("<button>" + key + "</button>");
		}
		
		item.appendTo(overlay);
		overlay.appendTo($('body'));*/
		
		var item = $("#dlg");
		for (var key in localStorage){
		   console.log(key);
		   item.append("<button>" + key + "</button>");
		}
		var foo = document.querySelector('dialog');
		foo.show();
		
		/*var raw = localStorage.getItem("recent");
		var parts = raw.split("|");
		var str = "";
		if(parts.length == 2)
			str = parts[1];
		else
			str = parts[0];
			
		console.log(str);
		var input = JSON.parse(str)[0];
		console.log(input);
		waves = [];
		waveViews = [];
		
		$("#waves").empty();
		$("#level").val(input.Level);
		$("#time").val(input.Time);
		waves = input.Waves;
		
		for(var i=0; i < waves.length; ++i) {
			var wave = new WaveView(waves[i], i);
			wave.getContent().appendTo("#waves");
		}*/
	});
	
	$('#generate').click(function(){
		var levels = [];
		data.Level = Number($("#level").val());
		data.Time = Number($("#time").val());
		data.Waves = waves;
		
		levels[0] = data;
		var str = JSON.stringify(levels);
		$('#output').val("level|" + str);
	});
	
	$("#sendToGame").click(function() {
		var data = $('#output').val();
		var timestamp = Date.now();
		localStorage.setItem(timestamp, data);
		if(!ws)
			connect();
		
		ws.send(data);
	});

	var json = {
		SpawnCount: "12",
		MaxEnemiesAlive: "14"
	};
});