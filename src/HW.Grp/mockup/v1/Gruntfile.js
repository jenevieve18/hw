module.exports = function(grunt) {
  grunt.initConfig({
    jade: {
      compile: {
        options: {
          data: {
            debug: false
          },
          pretty: true,
        },
        files: [{
          expand: true,
          cwd: 'src',
          src: ['*.jade'],
          dest: 'dist',
          ext: '.html'
        }]
      }
    },
    sass: {
      dist: {
        files: {
          'dist/assets/css/styles.css': ['src/scss/styles.scss'],
          'dist/assets/css/main.css': ['src/scss/main.scss']
        }
      }
    },
    purifycss: {
      options: {},
      target: {
        src: ['dist/*.html'],
        css: ['dist/assets/css/styles.css'],
        dest: 'dist/assets/css/styles.purified.css'
      },
    },
    watch: {
      jade: {
        files: ['src/**/*.jade'],
        tasks: ['jade', 'copy']
      },
      sass: {
        files: ['src/scss/**/*.scss'],
        tasks: ['sass', 'purifycss']
      },
      js: {
        files: ['src/scripts/**/*.js'],
        tasks: ['copy']
      }
    },
    qunit: {
      all: ['src/tests/**/*.html']
    },
    copy: {
      css: {
        files: [{
          expand: true,
          cwd: 'src/scss',
          src: ['*.css'],
          dest: 'dist/assets/css'
        }]
      },
      js: {
        files: [{
          expand: true,
          cwd: 'src/scripts',
          src: ['*.js'],
          dest: 'dist/assets/js'
        }]
      },
    },
  });

  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-jade');
  grunt.loadNpmTasks('grunt-contrib-sass');
  grunt.loadNpmTasks('grunt-contrib-qunit');
  grunt.loadNpmTasks('grunt-purifycss');
  grunt.loadNpmTasks('grunt-contrib-copy');

  grunt.registerTask('default', ['watch']);
  grunt.registerTask('build', ['jade', 'sass', 'purifycss', 'qunit', 'copy']);
};
