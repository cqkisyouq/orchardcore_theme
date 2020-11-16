function fuckScrollChaining($mask, $modal) {
    const listenerOpts = { passive: false };
    $mask.addEventListener(
        "touchmove",
        e => {
            e.preventDefault();
        },
        listenerOpts
    );
    const modalHeight = $modal.clientHeight;
    const modalScrollHeight = $modal.scrollHeight;
    let startY = 0;

    $modal.addEventListener("touchstart", e => {
        startY = e.touches[0].pageY;
    });
    $modal.addEventListener(
        "touchmove",
        e => {
            let endY = e.touches[0].pageY;
            let delta = endY - startY;
            if (
                ($modal.scrollTop === 0 && delta > 0) ||
                ($modal.scrollTop + modalHeight === modalScrollHeight && delta < 0)
            ) {
                e.preventDefault();
            }
        },
        listenerOpts
    );
}
function touchItem($item, touchendCallback) {
    var startX, endX;
    $item.on('touchstart', function (e) {
        startX = e.originalEvent.touches[0].clientX;
    });
    $item.on('touchmove', function (e) {
        endX = e.originalEvent.touches[0].clientX;
    });
    if (touchendCallback && typeof touchendCallback == "function") {
        $item.on('touchend', (e) => touchendCallback(startX, endX));
    }
}

(function ($) {
    var lisent = new $.SourcePoint.Monitor({
        subtree: true,
        childList: true,
        attributes: false,
        attributeOldValue: false
    }); 
    let observe = lisent.Observe(document, function (a) {
        a.forEach(function (item) {
            if (item.addedNodes) {
                item.addedNodes.forEach(function (node) {
                    if (node.nodeName == "IMG") {
                        node.dataset.original = node.src;
                        node.src = "/FlyingRat.Braksn/images/loadding.gif";
                    }
                })
            }
        })
    });
    $(function () {
        observe.disconnect();
        var lazyoption = { event: "scroll mouseover", effect: "fadeIn", threshold: 100 };
        $("img").lazyload(lazyoption);
        window.onclick = function () {
            setTimeout(function () {
                lazyoption.update();
            }, 260)
        }
        $("[data-toggle=tooltip]").tooltip();
    })
})(jQuery)