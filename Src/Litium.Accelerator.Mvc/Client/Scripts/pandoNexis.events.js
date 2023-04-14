

ready(function () {
    
});


function ready(callbackFunc) {
    if (document.readyState !== 'loading') {
        // Document is already ready, call the callback directly
        callbackFunc();
    } else if (document.addEventListener) {
        // All modern browsers to register DOMContentLoaded
        document.addEventListener('DOMContentLoaded', callbackFunc);

    } else {
        // Old IE browsers
        document.attachEvent('onreadystatechange', function () {
            if (document.readyState === 'complete') {
                callbackFunc();
            }
        });
    }
}

// Polyfills
// -----------------

// replaceAll
if (!String.prototype.replaceAll) {
    String.prototype.replaceAll = function (str, newStr) {

        // If a regex pattern
        if (Object.prototype.toString.call(str).toLowerCase() === '[object regexp]') {
            return this.replace(str, newStr);
        }

        // If a string
        return this.replace(new RegExp(str, 'g'), newStr);

    };
}