/// <binding AfterBuild='default' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sourcemaps = require('gulp-sourcemaps');
var sass = require('gulp-sass');
var tsc = require('gulp-typescript');
var uglify = require('gulp-uglify');
var minHtml = require('gulp-minify-html');


var paths = {
    js: {
        src: 'wwwroot/js/*.js',
        dest: 'wwwroot/js/'
    },
    sass: {
        src: 'Content/Sass/**/*.scss',
        dest: 'wwwroot/css/'
    },
    ts: {
        src: 'Content/Typescript/**/*.ts',
        dest: 'wwwroot/js/'
    },
    html: {
        src: 'Content/Html/*.html',
        dest: 'wwwroot/'
    }
};

gulp.task('default', ['ts', 'sass', 'html']);

gulp.task('release', ['ts', 'uglify', 'sass-c', 'min-html']);


gulp.task('sass', function () {
    gulp.src(paths.sass.src)
        .pipe(sass())
        .pipe(gulp.dest(paths.sass.dest));
});

gulp.task('sass-c', function () {
    gulp.src(paths.sass.src)
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(paths.sass.dest));
});

gulp.task('ts', function () {
    gulp.src(paths.ts.src)
        .pipe(tsc({
            sourceMap: false
        }))
        .pipe(gulp.dest(paths.ts.dest));
});

gulp.task('js', function(){
    gulp.src(paths.js.src)
        .pipe(gulp.dest(paths.js.dest));
});

gulp.task('uglify', function () {
    gulp.src(paths.js.src)
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(uglify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(paths.js.dest));
});

gulp.task('jshint', function () {
    gulp.src(paths.js.dest)
        .pipe(jshint())
        .pipe(jshint.reporter('default'))
});

gulp.task('html', function () {
    gulp.src(paths.html.src)
        .pipe(gulp.dest(paths.html.dest));
});

gulp.task('min-html', function () {
    gulp.src(paths.html.src)
        .pipe(minHtml())
        .pipe(gulp.dest(paths.html.dest));
});