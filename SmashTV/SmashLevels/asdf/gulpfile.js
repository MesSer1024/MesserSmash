var gulp = require('gulp');
var handlebars = require('gulp-handlebars');
var concat = require('gulp-concat');
var declare = require('gulp-declare');
var path = require('path');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');

var OutputTemplateFiles = 'foobar_templates.js';
var foo = "main.js";
//'js/myJs.js'
var FILES_JS = [
	"obj/" + OutputTemplateFiles,
	'js/**/*.js'
];

var FILES_TEMPLATE = [
	"template/**/*.html"
];

gulp.task('default', ["buildJS"], function() {
	console.log("Hello world!");
});

gulp.task('handlebars', function() {
	return gulp.src(['template/*.html'])
		.pipe(handlebars())
		.pipe(declare({
			namespace: 'MesserEntertainment',
            processName: function (filePath) {
                var ret = path.join(path.basename(__dirname), filePath.replace(/^.*[\\\/]/, '')).replace('.js', '');
                return ret;
            }			
		}))
		.pipe(concat(OutputTemplateFiles))
		.pipe(gulp.dest("obj/"));
});

gulp.task('buildJS', ['handlebars'], function() {
	return gulp.src(FILES_JS)
		.pipe(concat(foo))
		.pipe(gulp.dest("bin/"))
		.pipe(uglify())
		.pipe(rename({suffix: '.min'}))
		.pipe(gulp.dest("bin/"))
});

//this makes it possible to autobuild whenever a file has been saved, just write "gulp watch" in terminal
gulp.task('watch', function() {
	gulp.watch(FILES_JS, ['buildJS']);
	gulp.watch(FILES_TEMPLATE, ['buildJS']); //rebuild template files
	//gulp.watch(FILES_CSS, ['copyCSS']);
});