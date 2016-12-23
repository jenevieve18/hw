var healthwatch = (function() {
  return {
    grp: function(repo) {
      return {
        saveAdminExercise: function(exercise, success, error) {
          repo.saveAdminExercise(exercise, success, error);
        },
        readAdminExercise: function(exerciseId, success, error) {
          return repo.readAdminExercise(exerciseId, success, error);
        },
        hello: function(success, error) {
          return repo.hello(success, error);
        }
      };
    }
  };
}());

function ExerciseRepo() {
  this.saveAdminExercise = function(exercise, success, error) {
    ajax.post('Service.aspx/SaveManagerExercise', { exercise: exercise }, success, error);
  };
  this.readAdminExercise = function(exerciseId, success, error) {
    ajax.get('Service.aspx/ReadManagerExercise', { sponsorAdminExerciseID: exerciseId }, success, error);
  };
  this.hello = function(success, error) {
    ajax.get('Service.aspx/Hello', {}, success, error);
  };
}

function _ExerciseRepo() {
  this.saveAdminExercise = function(exercise, success, error) {
    if (exercise) {
      $(exercise.inputs).each(function(i, input) {
        console.log(input);
      });
      success('Success');
    } else {
      error('Holy crap, goes boom!', {}, {});
    }
  };
  this.readAdminExercise = function(exerciseId, success, error) {
    if (exerciseId == 0) {
      success(null);
    } else if (exerciseId > 0) {
      success({
        inputs: [{
            valueText: 'Input 1',
            components: [
              { valueText: 'Component 11' },
              { valueText: 'Component 12' },
              { valueText: 'Component 13' },
              { valueText: 'Component 14' },
              { valueText: 'Component 15' },
              { valueText: 'Component 16' },
              { valueText: 'Component 17' },
            ]
          },
          {
            valueText: 'Input 2',
            components: [
              { valueText: 'Component 21' },
              { valueText: 'Component 22' },
              { valueText: 'Component 23' },
              { valueText: 'Component 24' },
              { valueText: 'Component 25' },
            ]
          },
          { valueText: 'Input 3' },
          { valueText: 'Input 4' },
          { valueText: 'Input 5' }
        ]
      });
    } else {
      error('Holy crap, goes boom!', {}, {});
    }
  };
  this.hello = function(success, error) {
    success("Hello, world!");
  }
}

var ajax = {
  post: function(url, data, success, error) {
    $.ajax({
      type: 'POST',
      url: url,
      data: JSON.stringify(data),
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      success: function(response) {
        success(response.d);
      },
      error: function(request, status, error) {
        error(request, status, error);
      }
    });
  },
  get: function(url, data, success, error) {
    $.ajax({
      type: 'GET',
      url: url,
      data: data,
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      success: function(response) {
        success(response.d);
      },
      error: function(request, status, error) {
        error(request, status, error);
      }
    });
  }
};

var html = {
  getData: function(elements) {
    var data = [];
    $(elements).each(function(i, field) {
      data.push(field.value);
    });
    return data;
  },
  hasEmpty: function(elements) {
    var invalid = false;
    $(elements).each(function() {
      if (!$(this).val()) {
        invalid = true;
      }
    });
    return invalid;
  },
  span: function(content, classes) {
    var str = '<span' + this._getClasses(classes) + '>';
    str += '</span>';
    return str;
  },
  li: function(content, classes, prependElements, appendElements) {
    var str = '<li' + this._getClasses(classes) + '>';
    $(prependElements).each(function(i, e) {
      str += e;
    });
    str += content;
    str += '</li>';
    return str;
  },
  _getClasses: function(classes) {
    var _class = '';
    if (Array.isArray(classes) && classes.length > 0) {
      _class = ' class="';
      $(classes).each(function(i, c) {
        _class += c + ' ';
      });
      _class += '"';
    }
    return _class;
  }
};
