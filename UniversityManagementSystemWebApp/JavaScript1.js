$(document).ready(function () {
    var ddl = $("select[name$=ddlCountries]");
    var ddlState = $("select[name$=ddlState]");
    ddl.focus();
    ddl.bind("change keyup", function () {
        if ($(this).val() != "-1") {
            $('p[class$="para_Style"]')[0].innerText = "";
            loadStates($("select[name$=ddlCountries] option:selected").val(), $("select[name$=ddlCountries] option:selected").text());
            ddlState.fadeIn("slow");
        } else {
            ddlState.fadeOut("slow");
        }
    });
});
function loadStates(selectedIndex, selectedText) {
    $.ajax({
        type: "POST",
        url: "CascadingDropDown.aspx/FetchStates",
        data: "{countryID:" + parseInt(selectedIndex) + ",countryName:'" + selectedText + "'}",
        contentType: "application/json;char-set=utf-8",
        dataType: "json",
        async: true,
        success: function Success(data) {
            printStates(data.d);
            selectedIndex = 0;
            selectedText = '';
        }
    });
}
function printStates(data) {
    $("select[name*=ddlState]>option").remove();
    $("select[name*=ddlState]").append($("<option></option>").val(-1).html("(Please Select)"));
    $('p[class$="para_Style"]').append("You have Selected <b> " + $("select[name$=ddlCountries] option:selected").text() + "</b><br>State List is given below:<br>");
    for (var i = 0; i < data.length; i++) {
        $("select[name*=ddlState]").append($("<option></option>").val(data[i].StateID).html(data[i].StateName));
        $('p[class$="para_Style"]').append(data[i].StateName + "<br>");
    }
}







/*

$('#example2').cascadingDropdown({
    textKey: 'label',
    valueKey: 'value',
    selectBoxes: [
        {
            selector: '.step1',
            paramName: 'cId',
            url: '/api/country'
        },
        {
            selector: '.step2',
            requires: ['.step1'],
            paramName: 'sId',
            url: '/api/sector',
            valueKey: 'sectorId'
        },
        {
            selector: '.step3',
            requires: ['.step1', '.step2'],
            requireAll: true,
            url: '/api/offices',
            onChange: function(value){
                alert(value);
            }
        }
    ]
});


*/
