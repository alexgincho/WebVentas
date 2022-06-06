$(document).ready(function(){
    $("#buscar").on("keyup", function() {
      var value = $(this).val().toLowerCase();
      $("#catalogo-productos div").filter(function() {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
      });
    });
  });