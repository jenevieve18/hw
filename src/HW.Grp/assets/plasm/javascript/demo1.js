/*
* Mootools Simple Modal
* Version 1.0
* Copyright (c) 2011 Marco Dell'Anna - http://www.plasm.it
*/
window.addEvent("domready", function(e){
  
  
  /* Modal */
  /*$("modal").addEvent("click", function(e){
    e.stop();
    var SM = new SimpleModal({"btn_ok":"Confirm button"});
        // Aggiunge Bottone Conferma
        SM.addButton("Confirm", "btn primary", function(){
            alert("Action confirm modal");
            this.hide();
        });
        // Aggiunge Bottone annulla
        SM.addButton("Cancel", "btn");
        SM.show({
          "model":"modal",
          "title":"Modal Window Title",
          "contents":"<p ><img style='text-align:center' src='assets/images/simpleModalSmallWhite.png' />Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>"
        });
  });*/
  
  
  $('#modal').click(function() {
  	var SM = new SimpleModal({"btn_ok":"Confirm button"});
        // Aggiunge Bottone Conferma
        SM.addButton("Confirm", "btn primary", function(){
            alert("Action confirm modal");
            this.hide();
        });
        // Aggiunge Bottone annulla
        SM.addButton("Cancel", "btn");
        SM.show({
          "model":"modal",
          "title":"Modal Window Title",
          "contents":"<p ><img style='text-align:center' src='assets/images/simpleModalSmallWhite.png' />Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>"
        });
  });
  
});