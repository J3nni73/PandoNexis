import React, { Fragment } from 'react';
/*import DynamicComponent from '../../Components/DynamicComponent';*/
import SearchIcon from '../../_Solution/Icons/search.svg?component';
import CartIcon from '../../_Solution/Icons/shopping_cart.svg?component';
import AccountIcon from '../../_Solution/Icons/account.svg?component';
import WebsitesIcon from '../../_Solution/Icons/websites.svg?component';
import PoweredByIcon from '../../_Solution/Icons/powered_by.svg?component';
import LitiumIcon from '../../_Solution/Icons/litium.svg?component';

const PnIcon = ({ iconName, title, cssClass="", width=24, height=24 }) => {
   
    if (iconName === 'search') {
        return (<SearchIcon width={width} height={height} className={`pn-icon__${iconName} ${cssClass}`} title={title} />);
    }
    else if (iconName === 'cart') {
        return (<CartIcon width={width} height={height} className={`pn-icon__${iconName} ${cssClass}`} title={title} />);
    }
    else if (iconName === 'account') {
        return (<AccountIcon width={width} height={height} className={`pn-icon__${iconName} ${cssClass}`} title={title} />);
    }
    else if (iconName === 'websites') {
        return (<WebsitesIcon width={width} height={height} className={`pn-icon__${iconName} ${cssClass}`} title={title} />);
    }
    else if (iconName === 'powered-by') {
        return (<PoweredByIcon width={width} height={height} className={`pn-icon__${iconName} ${cssClass}`} title={title} />);
    }
    else if (iconName === 'litium') {
        return (<LitiumIcon width={width} height={height} className={`pn-icon__${iconName} ${cssClass}`} title={title} />);
    }
    return null;
};

export default PnIcon;
