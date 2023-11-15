import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch, connect } from 'react-redux';
import { GenericDataView } from './GenericDataView/DataViews';
import CloseIcon from '../Icons/close.svg?component';
import {
  load as loadAction,
  getGenericDataViewForExport as getGenericDataViewForExportAction,
  toggleModal,
} from '../Actions/GenericDataView.action';
import { update as updateAction } from '../Actions/GenericDataContainer.action';
//import GenericDataViewTabs from '../Components/GenericData/GenericDataViewTabs';
//import Modal from '../Components/ModalComponent';
import { translate } from '../../../Services/translation';

//import GlobeIcon from './Icons/globe.svg?component';
//import Fuse from 'fuse.js'
//import { translate } from '../../../Services/translation';

//import Breadcrumbs from './Breadcrumbs';

const PNGenericDataViewContainer = ({ pageSystemId, ...props }) => {
  const genericDataView = useSelector((state) => state.genericDataView);

  const dispatch = useDispatch();
  const elementRef = useRef(null);
  const [showingPNGenericDataView, setShowingPNGenericDataView] = useState(
    false
  );

  const load = (params) => {
    dispatch(loadAction(pageSystemId, params));
  };

  const onDataContainerChange = (
    data,
    item,
    isInModal = false,
    entitySystemId = ''
  ) => {
    var currentEnvPageSystemId = isInModal
      ? modalPageSystemId || pageSystemId
      : pageSystemId;
    const selectedValueObject = {
      ...item,
      ...data,
    };
    dispatch(
      updateAction(
        currentEnvPageSystemId,
        selectedValueObject,
        item,
        isInModal,
        entitySystemId
      )
    );
  };

  const getGenericDataViewForExport = () => {
    dispatch(getGenericDataViewForExportAction());
  };

  const doToggleModal = () => {
    dispatch(toggleModal());
  };

  useEffect(() => {
    load();
  }, []);

  //useEffect(() => {
  //    load();
  //}, [modalDataContainers]);

  if (props.isNotLoggedOn) {
    // We use reversed condition due to object maybe not found
    return (
      <div className="generic-data-view--not-logged-on">
        {translate && (
          <>
            <h2>{translate('addons.genericgridview.notloggedon.title')}</h2>
            <div>
              {translate('addons.genericgridview.notloggedon.description')}
            </div>
          </>
        )}
      </div>
    );
  } else {
    const dataViewHasTabs = props.dataViewHasTabs || props.hasTabs;
    return (
      <Fragment>
        {genericDataView?.pnModal?.open &&
          genericDataView?.modalDataContainers?.length > 0 && (
            <div className="generic-data-view__modal-wrapper">
              <div
                className="generic-data-view__modal-icon-close"
                onClick={() => doToggleModal()}
              >
                <CloseIcon width="24" height="24" />
                <span>Close</span>
              </div>
              <div className={`generic-data-view__modal`}>
                <GenericDataView
                  onSettingsChange={load}
                  onExportSelected={getGenericDataViewForExport}
                  onDataContainerChange={onDataContainerChange}
                  isInModal={true}
                  {...props}
                  {...{
                    pageSystemId,
                    dataContainers: genericDataView.modalDataContainers,
                    settings: genericDataView.modalSettings,
                  }}
                />
              </div>
            </div>
          )}

        <div className="generic-data-view-container">
          <div id="portal"></div>

          {dataViewHasTabs && <GenericDataViewTabs />}
          <div
            className={` ${
              dataViewHasTabs
                ? 'generic-data-view__container--has-tabs'
                : 'generic-data-view__container'
            }`}
            style={{ maxWidth: genericDataView?.settings?.maxWidth || 'none' }}
          >
            <GenericDataView
              onSettingsChange={load}
              onExportSelected={getGenericDataViewForExport}
              onDataContainerChange={onDataContainerChange}
              {...{ pageSystemId }}
              {...props}
            />
          </div>
        </div>
      </Fragment>
    );
  }
};

const mapStateToProps = ({ genericDataView }) => genericDataView;

export default connect(mapStateToProps)(PNGenericDataViewContainer);
