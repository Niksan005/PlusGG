// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

///
/// Bootstrap 4 tooltip
/// https://getbootstrap.com/docs/4.0/components/tooltips/
///

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});





///
/// Source
/// https://codepen.io/chriscoyier/pen/LEGXOK
/// 

var FormStuff = {

    init: function () {
        this.bindUIActions();
        this.applyConditionalReveal();
        $("input:checked").each(function () {
            $(this).change();
        });
    },

    bindUIActions: function () {
        $("input[type='radio'][data-bind=reveal]").on("change", this.applyConditionalReveal);
        $("input[type='radio']").on("change", this.applyVisualisation);
    },

    applyConditionalReveal: function () {
        var changedEl = $(this);
        $(".reveal-if-active").each(function () {
            var el = $(this);
            if ($(el.data("reveal-pair")).is(":checked")) {
                el.addClass("active");
            } else {
                el.removeClass("active");
                el.find('input').prop('checked', false);
            }
            if (changedEl.attr('name') == $(el.data("reveal-pair")).attr('name')) {
                el.find('img').removeClass("inactive");
            }
        });

    },

    applyVisualisation: function () {
        var name = $(this).attr("name");
        var hasChecked = $("input[name='" + name + "']:checked").length;
        $("input[name='" + name + "']").each(function () {
            if (hasChecked && !$(this).is(":checked")) {
                $(this).parent().find("img").addClass("inactive");
            } else {
                $(this).parent().find("img").removeClass("inactive");
            }
        });
    }

};

FormStuff.init();

///
/// https://jsfiddle.net/yetfb89n/35/
/// https://yuilibrary.com/yui/docs/sortable/sortable-events.html
///

var sortable;
if ($("#item-list").length) { 
    YUI().use('sortable', function (Y) {

        var log;

        // The node where we'll output the drag-and-drop events.
        log = Y.one('#log');

        // Our sortable list instance.
        sortable = new Y.Sortable({
            container: '#item-list ul',
            nodes: 'li',
            opacity: '0.1'
        });

        sortable.delegate.after('drag:end', function (e) {
            fillIds();
        });

    });
}
var cnt = 1;

function AddLi(tag) {
    $("#list").append(tag);
    fillIds();
}


function RemoveThis(element) {
    $(element).parent().remove();
    fillIds();
}

function fillIds() {
    var ids = "";
    $("#list").children().each(function () {
        ids = ids + "," + $(this).attr("data-id");
    });
    $("#ItemIDs").val(ids);
    sortable.sync();

}

///
/// https://jsfiddle.net/6Lzcgdo1/5/
///


$('.ui.search-item')
    .search({
        apiSettings: {
            url: '/MatchUp/GetItems?q={query}'
        },
        fields: {
            title: "title"
        },
        onSelect: function (result, response) {
            AddLi('<li data-id="' + result.id + '">' + result.title + " <button onclick='RemoveThis(this)'>X</button></li>");
            $(".ui.search").find("input").val("");
            $(".ui.search").find("input").blur();
            return false;
        }
    });


$('.ui.search-main-champion')
    .search({
        apiSettings: {
            url: '/MainPage/GetMainChampoin?q={query}'
        },
        fields: {
            title: "title"
        },
        onSelect: function (result, response) {

            window.location.href = result.link;
            return false;
        }
    });

$('.ui.search-vs-champion')
    .search({
        apiSettings: {
            url: '/MainPage/GetVsChampion?q={query}&cid=' + $("#cid").val()
        },
        fields: {
            title: "title"
        },
        onSelect: function (result, response) {

            window.location.href = result.link;
            return false;
        }
    });