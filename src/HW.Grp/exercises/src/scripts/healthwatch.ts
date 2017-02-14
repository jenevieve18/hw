namespace healthwatch.grp {
  class Exercise {
    id: number;
    inputs: Array<ExerciseDataInput>;
  }

  class ExerciseDataInput {
    valueText: string;
    valueInt: number;
    type: number;
    sortOrder: number;
    components: Array<ExerciseDataInputComponent>;
  }

  class ExerciseDataInputComponent {
    valueText: string;
    valueInt: number;
    type: number;
    sortOrder: number;
    
  }

  interface IGrpService {
    saveManagerExercise(exercise: Exercise): void;
    readManagerExercise(id: number): Exercise;
  }

  class GrpService implements IGrpService {
    saveManagerExercise(exercise: Exercise) {
      $.ajax();
    }
    readManagerExercise(id: number): Exercise {
      var e: Exercise = null;
      return e;
    }
  }

  class _GrpService implements IGrpService {
    saveManagerExercise(exercise: Exercise) {
    }
    readManagerExercise(id: number): Exercise {
      var e: Exercise = null;
      return e;
    }
  }
}

// healthwatch.grp(new healthwatch.GrpService()).saveManagerExercise(null);
