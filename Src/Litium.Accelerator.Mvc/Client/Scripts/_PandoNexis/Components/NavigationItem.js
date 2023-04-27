import React, { Fragment, useRef, useState } from 'react';
import { translate } from '../../Services/translation';
import constants from '../../constants';
import PnIcon from './PnIcon';

const NavigationItem = ({ links = [], contentLink = null, subLevel = 0 }) => {
    const menuRef = useRef(null);
    const [mainMenuActive, setMainMenuActive] = useState(false);
    const toggleMenu = (e) => {
        e.preventDefault();
        const classList = menuRef?.current?.classList;
        if (classList) {
            classList.toggle('navbar__menu--show');
        }
        setMainMenuActive(!mainMenuActive);
    };
    const additionClass =
        contentLink && contentLink.attributes
            ? contentLink.attributes.cssValue
            : null;
    const selectedClass =
        contentLink && contentLink.isSelected ? 'navbar__link--selected' : '';
    const hasChildrenClass = links.length > 0 ? 'has-children' : null;
    const hasNameOrChildren = (link) =>
        link.name || (link.links || []).length > 0;

    const getLowerLevelLinkCountClass = (amount) => {
        if (amount > 40) {
            return "large";
        }
        else if (amount > 20) {
            return "medium";
        }
        return "small";
    };
    if (!contentLink && links?.length < 1) {
        return null;
    }
    return (
        <Fragment>
            {!contentLink && links.length > 0 ? (
                <button className={`navbar__link--block navbar__icon--menu navbar__icon nav__hamburger-icon ${mainMenuActive ? 'active' : ''}`}

                    onClick={toggleMenu}
                    rel="nofollow"
                    title={translate('general.menu') || 'menu'}>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </button>
            ) : (
                <Fragment>
                    <a
                        className={`navbar__link ${selectedClass} ${hasChildrenClass || ''
                            } ${additionClass || ''}`}
                        href={contentLink?.url || '#'}
                        dangerouslySetInnerHTML={{ __html: contentLink.name }}
                    ></a>
                    {links.length > 0 && (
                        <i
                            className="navbar__icon--caret-right navbar__icon navbar__icon--open"
                            onClick={toggleMenu}
                        ></i>
                    )}
                </Fragment>
            )}

            {links.length > 0 && (
                <div className="navbar__menu" ref={menuRef}>
                    <div className="navbar__menu-header">
                        {!contentLink ? (
                            <span
                                className="navbar__icon navbar__icon--close"
                                onClick={toggleMenu}
                            > <PnIcon iconName="close" title="close" width="24" height="24" cssClass="" /></span>
                        ) : (
                            <Fragment>
                                <i
                                    className="navbar__icon--caret-left navbar__icon"
                                    onClick={toggleMenu}
                                ></i>
                                <span
                                    className="navbar__title"
                                    onClick={toggleMenu}
                                    dangerouslySetInnerHTML={{
                                        __html: contentLink.name,
                                    }}
                                ></span>
                            </Fragment>
                        )}
                    </div>
                    <ul className={`navbar__menu-links sub-level-${subLevel} sub-links-count-${links?.length || 0} ${subLevel === 2 ? getLowerLevelLinkCountClass(links?.length || 0) : ''}`}>
                        {links.length > 0 &&
                            links.map(
                                (link, index) =>
                                    hasNameOrChildren(link) && (
                                        <li
                                            className="navbar__item"
                                            key={index}
                                        >
                                            <NavigationItem
                                                links={link.links}
                                                contentLink={link}
                                                subLevel={subLevel + 1}
                                            />
                                        </li>
                                    )
                            )}
                    </ul>
                    {!contentLink && constants && constants.topLinkList &&
                        <ul className="navbar__menu-links navbar__top-menu-links">
                            {constants.topLinkList.map(
                                (link, index) => (
                                    <li
                                        className="navbar__item"
                                        key={`topItem${index}`}
                                    >
                                        <a className="navbar__link" href={link.href}>{link.text}</a>
                                    </li>
                                ))}
                        </ul>
                    }
                </div>
            )}
        </Fragment>
    );
};

export default NavigationItem;
