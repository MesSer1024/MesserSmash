function SpawnCriteria() {
	return {
		MaxEnemiesAlive : -1,
		MinSecondsInArena : -1,
		MinTotalEnemiesKilled : -1,
		WaveRepeatableCount : 0,
		SecondsBetweenRepeat : 1
	};
}; 

function WaveSpawner(enemyType, amount) {
	return {
	   EnemyType : enemyType,
	   SpawnCount : amount,
	   Criteria : new SpawnCriteria()
	}
};

function WaveView(model, index) {
	var _model = model;
	var _index = index;
	var _content = $("<div class='wave-container'>");
	var _button = $("<button class='waveheader--button'>+/-</button>").appendTo(_content);
	
	function hide() {
		console.log("hide idx=", _index, this);
		_content.children().not(_button).not(".waveheader").toggle();
	}

	function createDropdown(selectedType) {
		var s = "<select class=\"value\">";
		for(var i in EnemyTypes) {
			if( EnemyTypes[i] === selectedType )
				s += "<option selected>" + i + "</option>";
			else
				s += "<option>" + i + "</option>";
		}
		s += "</select>";
		return s;
	}
	
	function onEnemyType() {
		var value = this.selectedIndex;
		console.log("[" + _index + "]onEnemyType value=" + value);
		_model.EnemyType = value;
		console.log(_model);
	};
	
	function onSpawnCount() {
		var value = $(this).val();
		console.log("[" + _index + "]onSpawnCount value=" + value);
		_model.SpawnCount = value;
		console.log(_model);
	};
	
	function onMaxEnemies() {
		var value = $(this).val();
		console.log("[" + _index + "]onMaxEnemies value=" + value);
		_model.Criteria.MaxEnemiesAlive = value;
		console.log(_model);
	};
	
	function onMinSeconds() {
		var value = $(this).val();
		console.log("[" + _index + "]onMinSeconds value=" + value);
		_model.Criteria.MinSecondsInArena = value;
		console.log(_model);
	};
	
	function onMinKills() {
		var value = $(this).val();
		console.log("[" + _index + "]onMinKills value=" + value);
		_model.Criteria.MinTotalEnemiesKilled = value;
		console.log(_model);
	};
	
	//header
	($("<div>", { class: "waveheader", text:"Wave_" + _index })).appendTo(_content);
	
	//enemy type
	$("<div class='keyvaluepair'>").append(
		$("<div>", {class: "keyvaluepair"}).append(
			$("<label>", {class: "key", text:"EnemyType"}),
			$(createDropdown(_model.EnemyType)).change(
				onEnemyType
			)
		)
	).appendTo(_content);
	
	//spawn count
	$("<div class='keyvaluepair'>").append(
		$("<div>", {class: "keyvaluepair"}).append(
			$("<label>", {class: "key", text:"SpawnCount"}),
			$("<input>", {class: "value", type: "text", value:_model.SpawnCount}).change(
				onSpawnCount
			)
		)
	).appendTo(_content);
	
	//criteria MaxEnemies
	$("<div class='keyvaluepair'>").append(
		$("<div>", {class: "keyvaluepair"}).append(
			$("<label>", {class: "key", text:"MaxEnemiesAlive"}),
			$("<input>", {class: "value", type: "text", value:_model.Criteria.MaxEnemiesAlive}).change(
				onMaxEnemies
			)
		)
	).appendTo(_content);
	
	//criteria MinSeconds
	$("<div class='keyvaluepair'>").append(
		$("<div>", {class: "keyvaluepair"}).append(
			$("<label>", {class: "key", text:"MinSecondsInArena"}),
			$("<input>", {class: "value", type: "text", value:_model.Criteria.MinSecondsInArena}).change(
				onMinSeconds
			)
		)
	).appendTo(_content);
	
	//criteria MinKills
	$("<div class='keyvaluepair'>").append(
		$("<div>", {class: "keyvaluepair"}).append(
			$("<label>", {class: "key", text:"MinTotalKills"}),
			$("<input>", {class: "value", type: "text", value:_model.Criteria.MinTotalEnemiesKilled}).change(
				onMinKills
			)
		)
	).appendTo(_content);
	
	return {
		getContent: function getContent() {
			return _content
		},
		getButton: function getButton() {
			return _button;
		},
		hide: hide
	}
};