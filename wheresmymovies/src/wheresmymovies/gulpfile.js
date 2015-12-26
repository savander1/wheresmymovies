/// <binding AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var autoprefixer = require('gulp-autoprefixer');
var tsc = require('gulp-typescript');
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
    	src: 'Content/Typescript/*.ts',
	    dest: 'wwwroot/js/'
    },
    html: {
        src: 'Content/Html/*.html', 
        dest: 'wwwroot/'
    }
}

gulp.task('default', ['sass', 'tsc', 'js', 'min-html']);

gulp.task('sass', function(){
    gulp.src(paths.sass.src)
        .pipe(autoprefixer({
			browsers: ['last 2 versions']
		}))
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write())
	    .pipe(gulp.dest(paths.sass.dest));
});

gulp.task('tsc', function(){
    gulp.src(paths.ts.src)

        .pipe(tsc({
            sourceMap: false,
            out: 'output.js'
        }))
        .pipe(uglify())
        .pipe(gulp.dest(paths.ts.dest));
});

gulp.task('js', function(){
    gulp.src(paths.jq.src)
        .pipe(uglify())
        .pipe(gulp.dest(paths.jq.dest));
        
   gulp.src(paths.typeahead.src)
        .pipe(uglify())
        .pipe(gulp.dest(paths.typeahead.dest));
});

gulp.task('min-html', function() {
    gulp.src(paths.html.src)
        .pipe(minHtml())
        .pipe(gulp.dest(paths.html.dest));
});

