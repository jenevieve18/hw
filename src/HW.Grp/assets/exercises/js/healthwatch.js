var healthwatch = (function() {
  return {
    grp: function(repo) {
      return {
        saveManagerExercise: function(exercise, success, error) {
          repo.saveManagerExercise(exercise, success, error);
        },
        readManagerExercise: function(exerciseId, success, error) {
          return repo.readManagerExercise(exerciseId, success, error);
        },
        hello: function(success, error) {
          return repo.hello(success, error);
        }
      };
    }
  };
}());

function ExerciseRepo() {
  this.saveManagerExercise = function(exercise, success, error) {
    // console.log(exercise);
    ajax.post('Service.aspx/SaveManagerExercise', { exercise: exercise }, success, error);
  };
  this.readManagerExercise = function(exerciseId, success, error) {
    ajax.get('Service.aspx/ReadManagerExercise', { sponsorAdminExerciseID: exerciseId }, success, error);
  };
  this.hello = function(success, error) {
    ajax.get('Service.aspx/Hello', {}, success, error);
  };
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
  accordion: function(element) {
    $(element).css('cursor', 'hand');
    $(element + ' h3').next('div').hide();
    $(element + ' h3').click(function() {
      $(this).next('div').slideToggle(300);
    });
  },
  getData: function(elements) {
    var data = [];
    $(elements).each(function(i, field) {
      data.push(field.value);
    });
    return data;
  },
  getInputs: function(elements) {
    var data = [];
    $(elements).each(function(i, field) {
      if ($(field).is(':checkbox')) {
        if ($(field).is(':checked')) {
          data.push({ valueInt: 1, type: 3 });
        } else {
          data.push({ valueInt: 0, type: 3 });
        }
      } else if ($(field).is(':radio')) {
        if ($(field).is(':checked')) {
          data.push({ valueInt: 1, type: 1 });
        } else {
          data.push({ valueInt: 0, type: 1 });
        }
      } else {
        data.push({ valueText: field.value, type: 2 });
      }
    });
    return data;
  },
  getElementValues: function(elements) {
    var data = [];
    $(elements).each(function(i, element) {
      if ($(element).is(':checkbox')) {
        if ($(element).is(':checked')) {
          data.push({ valueInt: 1, type: 3 });
        } else {
          data.push({ valueInt: 0, type: 3 });
        }
      } else if ($(element).is(':radio')) {
        if ($(element).is(':checked')) {
          data.push({ valueInt: 1, type: 1 });
        } else {
          data.push({ valueInt: 0, type: 1 });
        }
      } else {
        data.push({ valueText: $(element).val(), type: 2 });
      }
    });
    return data;
  },
  getElementTexts: function(elements) {
    var data = [];
    $(elements).each(function(i, element) {
      if ($(element).is(':checkbox')) {
        if ($(element).is(':checked')) {
          data.push({ valueInt: 1, type: 3 });
        } else {
          data.push({ valueInt: 0, type: 3 });
        }
      } else if ($(element).is(':radio')) {
        if ($(element).is(':checked')) {
          data.push({ valueInt: 1, type: 1 });
        } else {
          data.push({ valueInt: 0, type: 1 });
        }
      } else {
        // var $class = $(element).removeClass('ui-draggable').removeClass('ui-draggable-handle').attr('class');
        var $class = $(element).attr('class');
        data.push({ valueText: $(element).text(), type: 2, class: $class });
      }
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

function _ExerciseRepo() {
  this.saveManagerExercise = function(exercise, success, error) {
    if (exercise) {
      $(exercise.inputs).each(function(i, input) {
        console.log(input);
      });
      success('Success');
    } else {
      error('Holy crap, goes boom!', {}, {});
    }
  };
  this.readManagerExercise = function(exerciseId, success, error) {
    if (exerciseId === 0) {
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
  };
}
