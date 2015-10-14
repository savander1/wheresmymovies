/// <binding AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var tsc = require('gulp-tsc');
var uglify = require('gulp-uglify');
var minHtml = require('gulp-minify-html');

var paths = {
    js: {
        src: 'wwwroot/js/*.js',
        dest: 'wwwroot/js'
    },
    jq: {
        src: 'Content/Libraries/jquery/dist/jquery.js',
        dest: 'wwwroot/js/'
    },
    typeahead: {
        src: 'Content/Libraries/typeahead.js/dist/typeahead.bundle.js',
        dest: 'wwwroot/js/'
    },
    sass: {
    	src: 'Content/Sass/*.scss',
	    dest: 'wwwroot/css/'
    },
    ts: {
    	src: 'Content/Typescript/wheresmymovies.ts',
	    dest: 'wwwroot/js/'
    }
}

gulp.task('default', ['sass', 'tsc', 'js', 'min-html']);

gulp.task('sass', function(){
    gulp.src(paths.sass.src)
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write())
	    .pipe(gulp.dest(paths.sass.dest));
});

gulp.task('tsc', function(){
    gulp.src(paths.ts.src)

        .pipe(tsc({
            sourceMap: true
        }))
        .pipe(gulp.dest(paths.ts.dest));
});

gulp.task('js', function(){
    gulp.src(paths.jq.src)
        .pipe(gulp.dest(paths.jq.dest));
    gulp.src(paths.typeahead.src)
        .pipe(gulp.dest(paths.typeahead.dest));

    gulp.src(paths.js.src)
        //.pipe(uglify())
        .pipe(gulp.dest(paths.js.dest));
});

gulp.task('min-html', function(){
    gulp.src('wwwroot/default.html')
        .pipe(minHtml())
        .pipe(gulp.dest('wwwroot/'));
})

