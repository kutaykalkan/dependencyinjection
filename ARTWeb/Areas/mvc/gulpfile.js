fs = require("fs");

// -------------------------------------
//   Modules
// -------------------------------------
//
// gulp              : The streaming build system
// gulp-autoprefixer : Prefix CSS
// gulp-concat       : Concatenate files
// gulp-csscss       : CSS redundancy analyzer
// gulp-ejs          : EJS Template compiler
// gulp-imagemin     : Optimize images
// gulp-jshint       : JavaScript code quality tool
// gulp-livereload   : Reload browser on save
// gulp-load-plugins : Automatically load Gulp plugins
// gulp-minify-css   : Minify CSS
// gulp-notify       : Notify when pipe is complete
// gulp-html-prettify: Prettify HTML
// gulp-jshint       : JS linter
// gulp-rename       : Rename files
// gulp-sass         : Compile Sass
// gulp-uglify       : Minify JavaScript with UglifyJS
// gulp-util         : Utility functions
// gulp-watch        : Watch stream
// run-sequence      : Run a series of dependent Gulp tasks in order
//
// -------------------------------------

// Load plugins
var gulp = require('gulp'),
  plugins = require( 'gulp-load-plugins' )({
    scope: ['dependencies','devDependencies'],
    pattern: ['gulp-*', 'gulp.*'],
    replaceString: /\bgulp[\-.]/, // Remove gulp-prefix
    camelize: true, // Convert plugin names with hyphens to camelCase
    lazy: false // Don't lazy load
  });

// Vendors
// =============================================================
// Collect necessary files from vendor modules and pipe to prod
// =============================================================

gulp.task('vendor-styles', function() {
  return gulp.src([
    'wwwroot/lib/bootstrap/dist/css/bootstrap.min.css',
    'wwwroot/lib/bxslider-4/dist/jquery.bxslider.min.css',
    'wwwroot/lib/components-font-awesome/css/font-awesome.min.css'])
    .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('vendor-scripts', function() {
  return gulp.src([
    'wwwroot/lib/jquery/dist/jquery.min.js',
    'wwwroot/lib/bxslider-4/dist/jquery.bxslider.min.js'])
    .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('vendor-fonts', function() {
  return gulp.src([
    'wwwroot/lib/components-font-awesome/fonts/**/*'])
    .pipe(gulp.dest('wwwroot/fonts'));
});

// Gulp Build Tasks
// =============================================================
// =============================================================

// Templates
gulp.task('templates', function() {
  var slides = fs.readdirSync("src/templates/slides"); // Get slides list
  slides = slides.map(function(slide) {
    return "slides/" + slide;
  });
  console.log(slides);
  return gulp.src('src/templates/base.ejs')
    .pipe(plugins.ejs({
      root: "wwwroot/",
      slides: slides
    }, {ext: '.html'}))
    .pipe(plugins.htmlPrettify({indent_char: '  ', indent_size: 1}))
    .pipe(plugins.rename( "Login.html" ))
    .pipe(gulp.dest('.'))
    .pipe(plugins.notify({ message: 'EJS Templates processed. Login.html created.' }));
});

// Styles
gulp.task('styles', function() {
  return gulp.src('src/scss/**/*.scss')
    .pipe(plugins.sass({ style: 'expanded' }))
    .pipe(plugins.autoprefixer('last 2 version'))
    .pipe(plugins.rename( "site.css" ))
    .pipe(gulp.dest('wwwroot/css'))
    .pipe(plugins.rename({ suffix: '.min' }))
    .pipe(plugins.minifyCss())
    .pipe(gulp.dest('wwwroot/css'))
    .pipe(plugins.notify({ message: 'SASS Compiled' }));
});

// Scripts
gulp.task('scripts', function() {
  return gulp.src('src/js/**/*.js')
    .pipe(plugins.plumber())
    .pipe(plugins.jshint())
    .pipe(plugins.jshint.reporter('jshint-stylish'))
    .pipe(plugins.concat('site.js'))
    .pipe(gulp.dest('wwwroot/js'))
    .pipe(plugins.rename({ suffix: '.min' }))
    .pipe(plugins.uglify())
    .pipe(gulp.dest('wwwroot/js'))
    .pipe(plugins.notify({ message: 'Scripts have been processesd.' }));
});

// Images
gulp.task('images', function() {
  return gulp.src('src/images/*')
    .pipe(plugins.imagemin({ optimizationLevel: 3, progressive: true, interlaced: true }))
    .pipe(gulp.dest('wwwroot/images'))
    .pipe(plugins.notify({ message: 'Images have been optimized' }));
});

// Multi-task Builds
// =============================================================
// =============================================================

// Default
gulp.task('default', function() {
  gulp.start('styles','scripts','images','templates');
});

// Copy vendor files
gulp.task('vendors',['vendor-styles','vendor-scripts','vendor-fonts']);

// Watch and Live Reload
gulp.task('watch', function() {

  plugins.watch('src/scss/**/*.scss', function() {
    gulp.start('styles');
  });

  plugins.watch('src/templates/**/*.ejs', function() {
    gulp.start('templates');
  });

  plugins.watch('src/js/**/*.js', function() {
    gulp.start('scripts');
  });

  plugins.watch('src/images/**/*', function() {
    gulp.start('images');
  });

  plugins.watch('src/images/**/*', function() {
    gulp.start('images');
  });


});