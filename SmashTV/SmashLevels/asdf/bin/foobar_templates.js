this["MesserEntertainment"] = this["MesserEntertainment"] || {};
this["MesserEntertainment"]["asdf\\WaveSpawnView"] = {"compiler":[6,">= 2.0.0-beta.1"],"main":function(depth0,helpers,partials,data) {
  var stack1, helper, functionType="function", helperMissing=helpers.helperMissing, escapeExpression=this.escapeExpression, lambda=this.lambda;
  return "<div class=\"wave-container\">\r\n	<button class='waveheader--button'>+/-</button>\r\n	<div class='waveheader'>\r\n		<label text=\"Wave_"
    + escapeExpression(((helper = (helper = helpers.index || (depth0 != null ? depth0.index : depth0)) != null ? helper : helperMissing),(typeof helper === functionType ? helper.call(depth0, {"name":"index","hash":{},"data":data}) : helper)))
    + "\" />\r\n	</div>\r\n	<div>\r\n		<div id=\"foo\" class=\"keyvaluepair\">\r\n			<label class=\"key\" text=\"EnemyType\" />\r\n		</div>	\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\" text=\"SpawnCount\" />\r\n			<input class=\"value\" type=\"text\" value=\""
    + escapeExpression(((helper = (helper = helpers.SpawnCount || (depth0 != null ? depth0.SpawnCount : depth0)) != null ? helper : helperMissing),(typeof helper === functionType ? helper.call(depth0, {"name":"SpawnCount","hash":{},"data":data}) : helper)))
    + "\" />\r\n		</div>\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\" text=\"MaxEnemiesAlive\" />\r\n			<input class=\"value\" type=\"text\" value=\""
    + escapeExpression(lambda(((stack1 = (depth0 != null ? depth0.Criteria : depth0)) != null ? stack1.MaxEnemiesAlive : stack1), depth0))
    + "\" />	\r\n		</div>\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\" text=\"MinSecondsInArena\" />\r\n			<input class=\"value\" type=\"text\" value=\""
    + escapeExpression(lambda(((stack1 = (depth0 != null ? depth0.Criteria : depth0)) != null ? stack1.MinSecondsInArena : stack1), depth0))
    + "\" />	\r\n		</div>\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\" text=\"MinTotalEnemiesKilled\" />\r\n			<input class=\"value\" type=\"text\" value=\""
    + escapeExpression(lambda(((stack1 = (depth0 != null ? depth0.Criteria : depth0)) != null ? stack1.MinTotalEnemiesKilled : stack1), depth0))
    + "\" />		\r\n		</div>\r\n	</div>\r\n</div>";
},"useData":true};