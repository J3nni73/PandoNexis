let loaderCounter = 0;
export const toggleGenericLoader = (visible) => () => {
    let loaderEl = document.getElementById('mainGenericLoader');

    if (!loaderEl) {
        loaderEl = document.createElement('div');
        loaderEl.id = 'mainGenericLoader';
        loaderEl.classList = 'generic-loader';
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