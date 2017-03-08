var room = 1;

function education_fields() {
    var options = document.getElementsByName("2");
    room++;
    var objTo = document.getElementById("FillingWorkout_fields");
    var divtest = document.createElement("div");
    divtest.setAttribute("class", "form-group removeclass" + room);
    var newInner = "<div class=\"col-md-2\"></div><div class=\"col-md-2\"><div class=\"form-group\"><select  Name=\"Exercises[]\" class=\"form-control\" >";
   for(var i=0; i<options.length; i++){newInner+="<option>"+options[i].innerHTML+"</option>";
    }
    newInner +=
        "</select></div></div><div class=\"col-sm-1 nopadding\"><div class=\"form-group\"><input type=\"text\" class=\"form-control\"   name=\"Sets[]\" value=\"\" placeholder=\"Counts of sets...\"></span></div></div><div class=\"col-sm-1 nopadding\"><div class=\"form-group\"><input type=\"text\" class=\"form-control\"  name=\"MinRep[]\" value=\"\" placeholder=\"Minimum repeats...\"><span asp-validation-for=RepeatMin class=\"text-danger\"></span></div></div><div class=\"col-sm-1 nopadding\"><div class=\"form-group\"><div class=\"input-group\"><input type=\"text\" class=\"form-control\"  name=\"MaxRep[]\" value=\"\" placeholder=\"Maximum repeats...\"><span asp-validation-for=RepeatMax class=\"text-danger\"></span><div class=\"input-group-btn\"><button class=\"btn btn-danger\" type=\"button\" onclick=\"remove_education_fields(" + room + ");\"> <span class=\"glyphicon glyphicon-minus\" aria-hidden=\"true\"></span> </button></div></div></div></div><div class=\"clear\"></div>";
    divtest
        .innerHTML = newInner;
        

    objTo.appendChild(divtest);
}

function remove_education_fields(rid) {
    $(".removeclass" + rid).remove();
    $("."+rid).remove();
}
