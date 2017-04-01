var room = 1;

function ingestions_fields(count) {
    var options = document.getElementById("select").options;
    room++;
    var obj = "Ingections_Fields+" + count;
    var objTo = document.getElementById(obj);
    var divtest = document.createElement("div");
    divtest.setAttribute("class", "form-group removeclass" + room);
    var newInner =
        "<div class=\"col-sm-4 nopadding\"><div class=\"form-group\"><select name=\"food["+count+"][]\" class=\"form-control\" ><option value=\"\">-- Select Food --</option>";
    for(var i=0; i<options.length;i++) {
        newInner += "<option value=" + options[i].value+">" + options[i].text + "</option>";
    }
    newInner += "</select></div></div><label class=\"col-md-2 control-label\">Weight</label><div class=\"col-md-4\"><div class=\"form-group\"><div class=\"input-group\"><input name=\"Weight["+count+"][]\" class=\"form-control\" /><span class=\"input-group-btn\"><button class=\"btn btn-danger\" type=\"button\" onclick=\"remove_ingestions_fields(" + room + ");\"> <span class=\"glyphicon glyphicon-minus\" aria-hidden=\"true\"></span> </button></span></div></div></div>";
    divtest
        .innerHTML = newInner;
        

    objTo.appendChild(divtest);
}

function remove_ingestions_fields(rid) {
    $(".removeclass" + rid).remove();
    $("."+rid).remove();
}
