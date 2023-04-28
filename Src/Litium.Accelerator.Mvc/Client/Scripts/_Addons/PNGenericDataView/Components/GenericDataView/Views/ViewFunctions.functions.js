
// click outside menu
const currentDdSelector = document.querySelectorAll('.dropdown-selector__current');
document.addEventListener('click', (event) => {
    const el = event.target;
    if (el.classList.contains('navbar__icon'))
        return;
    if (el) {
        const isInMenu = el.closest(".navbar__menu");
        if (!isInMenu) {
            const activeElements = document.querySelectorAll('.navbar__item.active, .navbar__menu--show, .navbar__dimmer--show');
            if (activeElements) {
                activeElements.forEach((menuEl) => {
                    menuEl.classList.remove('active', 'navbar__menu--show', 'navbar__dimmer--show');
                });
            }
            const navBar = document.querySelector('nav.navbar');
            if (navBar) {
                navBar.classList.remove('navbar__skip-animation');
            }
        }
    }

    // Check dropdown selector
    const isInDropdown = el.closest(".dropdown-selector");
    if (!isInDropdown) {
        currentDdSelector.forEach((ddEl) => {
            ddEl.classList.remove('active');
            const ddCheckbox = ddEl.parentElement.querySelector('.dropdown-selector__checkbox');
            if (ddCheckbox) {
                ddCheckbox.checked = false;
            }
        });
    }
});


if (currentDdSelector) {
    currentDdSelector.forEach((ddEl) => {
        ddEl.addEventListener('click', (event) => {
            ddEl.classList.toggle('active');
        });
    });
}