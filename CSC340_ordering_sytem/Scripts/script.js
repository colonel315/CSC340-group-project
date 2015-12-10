$(document).ready(function() {
    var updateItemButton = $("[data-action=\"update-item\"]");

    updateItemButton.click(function() {
        var target = $(this).attr("data-target");
        $(target).submit();
    });

    var incrementButton = $("[data-action=\"increment\"");
    var decrementButton = $("[data-action=\"decrement\"");

    incrementButton.click(function() {
        var target = $(this).attr("data-target");
        var frontQtyInput = $(target);
        var hiddenQtyInput = $("[data-value=\"" + target + "\"]");
        var targetValue = parseInt(frontQtyInput.val()) + 1;

        if (targetValue > 10) {
            return false;
        }

        frontQtyInput.val(targetValue);
        hiddenQtyInput.val(targetValue);
    });

    decrementButton.click(function () {
        var target = $(this).attr("data-target");
        var frontQtyInput = $(target);
        var hiddenQtyInput = $("[data-value=\"" + target + "\"]");
        var targetValue = parseInt(frontQtyInput.val()) - 1;

        if (targetValue < 1) {
            return false;
        }

        frontQtyInput.val(targetValue);
        hiddenQtyInput.val(targetValue);
    });

});