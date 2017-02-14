const HW_PROJECT_DIR = '../../../../hw/';
const GRP_PROJECT_DIR = HW_PROJECT_DIR + 'src/hw.grp/';

module.exports = function(grunt) {
  grunt.initConfig({
    jade: {
      compile: {
        options: {
          data: { debug: false },
          pretty: true,
        },
        files: [{
          expand: true,
          cwd: 'src',
          src: ['*.jade'],
          dest: 'dist', ext: '.html'
        }]
      }
    },
    sass: {
      dist: {
        options: { style: 'expanded' },
        files: {
          'dist/assets/exercises/css/styles.css': 'src/scss/styles.scss',
          'dist/assets/exercises/css/exercises.css': 'src/scss/exercises.scss',
          'dist/assets/exercises/css/main.css': 'src/scss/main.scss',
        }
      }
    },
    copy: {
      html: {
        files: [{
          expand: true,
          cwd: 'dist',
          src: ['*.html'],
          dest: GRP_PROJECT_DIR
        }]
      },
      js: {
        files: [{
          expand: true,
          cwd: 'src/scripts/',
          src: ['*.js'],
          dest: 'dist/assets/exercises/js'
        // }, {
        //   expand: true,
        //   cwd: 'dist/assets/exercises/js/',
        //   src: ['*.js'],
        //   dest: GRP_PROJECT_DIR + 'assets/exercises/js'
        }]
      },
      css: {
        files: [{
          expand: true,
          cwd: 'dist/assets/exercises/css/',
          src: '*.css',
          dest: GRP_PROJECT_DIR + 'assets/exercises/css'
        }]
      }
    },
    watch: {
      jade: {
        files: ['src/**/*.jade'],
        // tasks: ['jade', 'sass', 'purifycss', 'copy']
        tasks: ['jade', 'sass', 'copy']
      },
      css: {
        files: ['src/scss/**/*.scss'],
        // tasks: ['sass', 'purifycss', 'copy']
        tasks: ['sass', 'copy']
      },
      js: {
        files: ['src/scripts/**/*.js', 'dist/assets/exercises/js/**/*.js'],
        // tasks: ['copy', 'qunit']
        tasks: ['copy']
      },
      // typescript: {
      //   files: ['source/scripts/**/*.ts'],
      //   tasks: ['typescript']
      // },
      qunit: {
        files: ['tests/**/*.js'],
        tasks: ['qunit'],
      }
    },
    qunit: {
      all: ['tests/**/*.html']
    }
  });

  grunt.loadNpmTasks('grunt-contrib-jade');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-sass');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-qunit');
  grunt.loadNpmTasks('grunt-typescript');
  // grunt.loadNpmTasks('grunt-purifycss');

  grunt.registerTask('default', ['jade', 'sass', 'copy', 'watch']);
};
