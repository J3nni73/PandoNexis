import React from 'react'
import { useSelector, useDispatch } from 'react-redux';
import { getHeaderInformationData } from '../Actions/GenericGridView.action';

const HeaderInformationDataContainer = () => {
    const dispatch = useDispatch();
    const { headerInformation } = useSelector((state) => state.genericGridView);

    React.useEffect(() => {
        dispatch(getHeaderInformationData());
    }, [])
    return (
        <React.Fragment>{headerInformation?.result && headerInformation?.result.length > 0 &&
            <div className="header__main-info-container">
                <div className="header__main-info-content" dangerouslySetInnerHTML={{ __html: headerInformation.result }}></div>
            </div>}
        </React.Fragment>
    )
}

export default HeaderInformationDataContainer