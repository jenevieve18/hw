$(function() {
  html.accordion('.accordion');
  $('.text').hide();
  $('.image').hover( function() {
      $(this).closest('tr').find('.text').fadeIn();
    }, function() {
      $(this).closest('tr').find('.text').fadeOut();
    }
  );
});
