
export const toggleGenericLoader = (visible) => () => {
    const loaderEl = document.getElementById('mainGenericLoader');

    if (visible && loaderEl.classList.contains('active')) {
        return;
    }

    if (loaderEl) {
        if (visible) {
            loaderEl.classList.add('active');
        } else {
            loaderEl.classList.remove('active');
        }
    }
    return;
}