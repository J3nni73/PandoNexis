import React, { useEffect, useState } from 'react';
import CloseIcon from '../../Icons/close.svg?component';
import SettingsIcon from '../../Icons/settings.svg?component';

const GenericDataViewFieldSettings = ({
    fields = [],
    fieldsToShow,
    handleSetfieldsToShow,
}) => {
    const [isOpen, setIsOpen] = useState(false);
    //const [hiddenFieldsCount, setHiddenFieldsCount] = React.useState(() => {
    //    return fields.length - [...fieldsToShow].length;
    //});
    const handleShow = () => {
        setIsOpen((wasOpened) => !wasOpened);
    };
    //useEffect(() => {

    //    setHiddenFieldsCount(fields.length - [...fieldsToShow].length);
    //}, [fieldsToShow]);

    return (
        <section className={`field-settings ${isOpen ? 'active' : ''}`}>
            <div className={`field-settings--show ${(fields.length - fieldsToShow.length) > 0 ? 'isHidingFields' : ''
                }`}
                onClick={handleShow}
            >
                <div className="field-settings__icon-settings">
                    <SettingsIcon className="" width="16" height="16" />
                </div>
                <span className="badge primary">{fieldsToShow.length}</span>
            </div>
            
            <div className={`field-settings__list`}>
                <div className="field-settings__icon-close">
                    <CloseIcon width="16" height="16" onClick={handleShow} />
                </div>
                <div className="field-settings__data-view">
                    {fields.map(({ fieldName }, index) => (
                        // <div key={index}> {fieldName} </div>

                        <div key={index}>
                            <input
                                className="field-settings__check-box"
                                type="checkbox"
                                id={fieldName}
                                value={fieldName}
                                checked={fieldsToShow.includes(index)}
                                onChange={() => handleSetfieldsToShow(index)}
                            />
                            <label htmlFor={fieldName}>{fieldName}</label>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
};

export default GenericDataViewFieldSettings;
