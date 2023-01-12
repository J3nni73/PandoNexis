import React, { Fragment, useRef } from 'react';
import { translate } from '../../Services/translation';
import constants from '../../constants';

const NavigationItem = ({ links = [], contentLink = null }) => {
    const menuRef = useRef(null);
    const toggleMenu = (e) => {
        e.preventDefault();
        menuRef.current.classList.toggle('navbar__menu--show');
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

    return (
        <Fragment>
            {!contentLink ? (
                <a
                    className="navbar__link--block navbar__icon--menu navbar__icon"
                    onClick={toggleMenu}
                    rel="nofollow"
                    href="#"
                    title={translate('general.menu') || 'menu'}
                ></a>
            ) : (
                <Fragment>
                    <a
                        className={`navbar__link ${selectedClass} ${
                            hasChildrenClass || ''
                        } ${additionClass || ''}`}
                        href={contentLink.url || '#'}
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
                            ></span>
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
                    <ul className="navbar__menu-links">
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
