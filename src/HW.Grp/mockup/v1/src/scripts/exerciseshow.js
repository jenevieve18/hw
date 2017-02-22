var count = 0;
$(function() {
  $(".sortable").sortable();
  $(".sortable").disableSelection();
  $(".sortable1 input").keyup(inputKeyUp);
  $('#btnAddDataInput').click(function() {
    if (!HtmlHelper.hasEmpty($(".sortable1 input"))) {
      addItem("");
    }
  });
  $('#btnSaveSponsorAdminExercise').click(function() {
    SponsorRepository.saveSponsorAdminExercise(
      getData($('.sortable input[id^="text"]')),
      $('#sponsorAdminID').val(),
      $('#exerciseVariantLangID').val(),
      $('#sponsorAdminExerciseID').val(),
      'Spara...',
      'Spara'
    );
  });
  SponsorRepository.findSponsorAdminExerciseDataInputs($('#sponsorAdminExerciseID').val(), function(response) {
    var hasInputs = false;
    $.each(response.d, function(i, d) {
      addItem(d.content);
      hasInputs = true;
    });
    if (!hasInputs) {
      addItem("");
    }
  });

  function addItem(content) {
    $('.sortable, .sortable1').append("<li><input type='text' id='text" + count + "' value='" + content + "' name='text[]'></li>");
    $(".sortable1 input").keyup(inputKeyUp);
    count++;
  }

  function inputKeyUp() {
    $(".sortable input[id='" + this.id + "']").val(this.value);
  }
});
