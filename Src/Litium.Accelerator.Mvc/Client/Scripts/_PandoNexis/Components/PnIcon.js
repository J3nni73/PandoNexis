import React, { Fragment } from 'react';
import DynamicComponent from '../../Components/DynamicComponent';

const PnIcon = ({ type, title }) => {
    const Icon = DynamicComponent({
        loader: () => import('../../_Solution/Icons/'+type+'.svg?component'),
});
return (
    <Fragment>{
        Icon && 
        <Icon width="48" height="48" className={`pn-icon__${type}`} title={title} />
    }
        
    </Fragment >
);
};

export default PnIcon;
