/// <binding AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var tsc = require('gulp-tsc');

var paths = {
    sass: {
    	src: 'Content/Sass/*.scss',
	    dest: 'wwwroot/css/'
    },
    ts: {
    	src: 'Content/Typescript/wheresmymovies.ts',
	    dest: 'wwwroot/js/'
    }
}

gulp.task('default', function (){
    gulp.src(paths.sass.src)
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write())
	    .pipe(gulp.dest(paths.sass.dest));

    gulp.src(paths.ts.src)
        .pipe(tsc({
            sourceMap: true,
            noLib: true
        }))
        .pipe(gulp.dest(paths.ts.dest));
});
