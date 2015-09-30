/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');

var paths = {
    sass: {
    	src: 'Content/Sass/*.scss',
	dest: 'wwwroot/style/'
    }
}

gulp.task('default', function () {
    gulp.src(paths.sass.src)
        .pipe(sass())
	.pipe(gulp.dest(paths.sass.dest));
});
