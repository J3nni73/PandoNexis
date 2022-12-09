import React, { Fragment } from 'react';
import { useForm, FormProvider } from 'react-hook-form';
import FormControl from './FormControl';
import { useDispatch, useSelector } from 'react-redux';
import { getGenericGridForm } from '../../Actions/GenericGridForm.action';
import { v4 as uuidv4 } from 'uuid';
import { translate } from '../../../../Services/translation';
import LoadingSpinner from '../LoadingSpinner';

function EnrollmentForm({ gridType, handlecategoryById, hide }) {
    const dispatch = useDispatch();
    const data = useSelector((state) => state.genericGridForm);
    const { register, setValue, handleSubmit, getValues, watch, control, getFieldState, formState } = useForm();

    let checkGridIfLoaded = document.getElementsByClassName('generic-grid-view__no-result');

    const onSubmit = (fieldsData) => {
        const getQueryFieldState = getFieldState('QuotaFormField').isDirty;

        if (!getQueryFieldState && checkGridIfLoaded.length === 0) {
            hide();
        } else {
            handlecategoryById(fieldsData);
            hide();
        }
    };

    React.useEffect(() => {
        dispatch(getGenericGridForm(gridType));
    }, [dispatch, gridType]);

    const methods = { register, getValues, setValue, handleSubmit, watch, control, getFieldState, gridType, data }
    return (
        <Fragment>
            <FormProvider {...methods} >
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="form-info11 data-abide novalidate">
                        {data?.isLoading ?
                            <Fragment>
                                {/* <div className="generic-loader active"></div> */}
                                <LoadingSpinner text={translate('addons.genericgridview.loadingtext')} />
                            </Fragment>
                            :
                            data.dataRows.map((item, index) => {
                                return (
                                    <FormControl
                                        key={`${uuidv4()}${index}`}
                                        controlForm={item?.fieldType}
                                        type={item?.fieldType}
                                        label={item?.fieldName}
                                        name={item.fieldName}
                                        setValue={setValue}
                                        register={register}
                                        options={item?.dropDownOptions}
                                        settings={item?.settingsNew}
                                        style={item?.style}
                                        gridType={gridType}
                                    />
                                );
                            })}
                    </div>
                    <div className="row">
                        <div className="small-12 columns  text--center">
                            {gridType != 'Quota' && <input className={`form__button  ${!data?.btnIsActive ? "form__button--expand--disabled" : ""}`} disabled={!data?.btnIsActive} type="submit"
                                value={translate('addons.genericgridview.form.button.submit.' + gridType.toLowerCase())} />
                            }
                        </div>
                    </div>
                </form>
            </FormProvider>
        </Fragment>
    );
}

export default EnrollmentForm;
