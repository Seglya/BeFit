var num = 0;
function exercise_fields(count) {
    var options = document.getElementById("select").options;
    num++;
    var nom = num+count;
    var obj = "Exercise_Fields";
    var objTo = document.getElementById(obj);
    var divtest = document.createElement("div");
    divtest.setAttribute("class", "form-group removeclass" + nom);
    var newInner =
        "<label class=\"col-md-1 control-label\">Exercise N"+nom+"</label><div class=\"form-group\"><div ><div class=\"form-group\"><div class=\"col-sm-2 nopadding\"><div class=\"form-group\"><select id=\"select\" name=\"Exercise[]\" class=\"form-control\" asp-items=\"ViewBag.ExerciseID\">-- Select Exercise --</option>";
    newInner += "<option value=\"\">--Seect exercise--</option>";
    for (var i = 1; i < options.length; i++) {
        newInner += "<option value=" + options[i].value+">" + options[i].text + "</option>";
    }
    newInner += "</select></div></div><label class=\"col-md-1 control-label\">Repeats</label><div class=\"col-sm-2 nopadding\"><div class=\"form-group\"><input name=\"Reapeats\" class=\"form-control\"/><span asp-validation-for=\"@item.Repeat\" class=\"text-danger\"></span></div></div><label class=\"col-md-1 control-label\">Sets</label><div class=\"col-md-2\"><div class=\"form-group\"><div class=\"input-group\"><input name=\"Sets[]\" class=\"form-control\"/><span asp-validation-for=\"@item.Sets\" class=\"text-danger\"></span><span class=\"input-group-btn\"><button class=\"btn btn-danger\" type=\"button\" onclick=\"remove_exercise_fields("+nom+");\"> <span class=\"glyphicon glyphicon-minus\" aria-hidden=\"true\"></span> </button></span></div></div></div>";
    divtest
        .innerHTML = newInner;
        

    objTo.appendChild(divtest);
}

function remove_exercise_fields(rid) {
    $(".removeclass" + rid).remove();
    $("."+rid).remove();
}
