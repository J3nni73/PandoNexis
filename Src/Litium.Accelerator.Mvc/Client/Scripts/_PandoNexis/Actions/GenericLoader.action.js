let loaderCounter = 0;
export const toggleGenericLoader = (visible, type ='spinner') => () => {
   // type = 'ripple' or 'spinner'
    let loaderEl = document.getElementById('mainGenericLoader__' + type);

    if (!loaderEl) {
        loaderEl = document.createElement('div');
        loaderEl.id = 'mainGenericLoader__' + type;
        loaderEl.classList = 'generic-loader__' + type;

        if (type === 'ripple') {
            loaderEl.innerHTML = '<div></div><div></div>';
        }

        document.querySelector('body').append(loaderEl);
    }

    // Handle multiple calls
    if (visible) {
        loaderCounter++;
    }
    else {
        loaderCounter--;
        if (loaderCounter < 0)
            loaderCounter = 0;
    }

    if (visible && loaderEl.classList.contains('active')) {
        return;
    }

    if (loaderEl) {
        if (visible) {
            loaderEl.classList.add('active');
        } else if (loaderCounter < 1) {
            loaderEl.classList.remove('active');
        }
    }
    return true;
}