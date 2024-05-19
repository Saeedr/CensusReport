function ShowValidationDialog(element) {
    var parent = findParent($(element), "formToolTip");
    if (parent) {
        var dialog = $(parent).find(".validationDialog").first();
        dialog.css("display", "block");
    }
}
function HideValidationDialog(element) {
    var parent = findParent($(element), "formToolTip");
    if (parent) {
        var dialog = $(parent).find(".validationDialog").first();
        dialog.css("display", "none");
    }
}

function findParent(element, className) {

    var parent = $(element).parent();

    if (parent.hasClass(className))
        return parent;

    if (parent.is($("body")))
        return undefined;

    return findParent(parent, className);
}

function ChangeClass(element, sourceClass, replcaeClass) {
    if ($(element).hasClass(sourceClass)) {
        $(element).removeClass(sourceClass);
        $(element).addClass(replcaeClass);
    }
    else
        $(element).addClass(replcaeClass);
}