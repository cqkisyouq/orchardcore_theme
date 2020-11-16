(function ($) {
    $(function () {
        function movItems(reIndex) {
            items.each(function (index, item) {
                $(item).stop(true, false).animate({ "left": 85 * reIndex * -1 + "px" }, 500)
            })
        }
        const $mask = document.querySelector("#mask");
        const $dialog = document.querySelector("#demo");
        fuckScrollChaining($dialog, $mask);
        touchItem($(".carousel-inner"), function (startX, endX) {
            var distance = Math.abs(startX - endX);
            var offset = 30;
            if (distance > offset) {
                if (startX > endX) {
                    $('.carousel-control-next').click();
                } else {
                    $('.carousel-control-prev').click();
                }
            }
        });
        let configuation = {
            subtree: true,
            childList: true,
            attributes: true,
            attributeOldValue: true
        };
        let lisent = new $.SourcePoint.Monitor(configuation);
        let items = $(".carousel-indicators li");
        let total = items.length;
        let start = 0;
        let max = 3;
        //modal 底部小图列表
        lisent.Observe($(".carousel-indicators")[0], function (a) {
            a.forEach(function (changeItem) {
                if (changeItem.attributeName != "class" || !changeItem.oldValue) return;
                if (changeItem.oldValue.indexOf("active") < 0) {
                    let index = items.index(changeItem.target);
                    start = index;
                    let reIndex = index - max;
                    if (index + max > total) {
                        reIndex = total - max;
                    }
                    reIndex += 2;
                    movItems(reIndex);
                }
            })
        })
        if (items.length) items[0].click();
        let oldStart = start;
        let reIndex = oldStart;
        touchItem($(".carousel-indicators"), function (startX, endX) {
            var distance = Math.abs(startX - endX);
            var offset = 30;
            if (start != oldStart) reIndex = start;
            if (distance > offset) {
                if (startX > endX) {
                    let min = total - max;
                    reIndex = Math.min(min, reIndex + 3);
                    if (reIndex <= min && total > max) reIndex += 1;
                    movItems(reIndex);
                } else {
                    reIndex = Math.max(0, reIndex - 3);
                    if (reIndex > 0) reIndex -= 1;
                    movItems(reIndex);
                }
            }
            oldStart = start;
        })
        $("#imgs img").each(function (index, item) {
            $(item).click(function () {
                if (items[index]) items[index].click();
            })
        })
    })
})(jQuery)