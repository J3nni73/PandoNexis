import React, { useEffect, Fragment } from 'react';
import { connect } from 'react-redux';
import { GenericGridView } from '../Components/GenericGrid';
import {
  load,
  categoryById,
  getGenericGridViewForExport,
} from '../Actions/GenericGridView.action';
import { update } from '../Actions/GenericGridRow.action';
import GenericGridTabs from '../Components/GenericGrid/GenericGridTabs';
import Modal from '../Components/ModalComponent';
import { translate } from '../../../Services/translation';
/**
 *
 * @param {string} props.type - The type of grid, which is used to select endpoint.
 * @param {string} props.load - Generic data loading function to load data into redux store.
 * @param {string} props.update - Row data update function.
 */
const GenericGridViewContainer = ({
  type,
  load,
  update,
  categoryById,
  ...props
}) => {
  useEffect(() => {
    load();
  }, []);
  const handlecategoryById = (data) => {
    categoryById(data);
  };

  if (props.isNotLoggedOn) {
    // We use reversed condition due to object maybe not found
    return (
      <div className="generic-grid-view--not-logged-on">
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
    const gridViewHasTabs = props.gridViewHasTabs || props.hasTabs;
    return (
      <Fragment>
        <div id='portal'></div>
        <div className="form__container">
          <Modal gridType={type} handlecategoryById={handlecategoryById} />
        </div>
        {gridViewHasTabs && <GenericGridTabs />}
        <div
          className={`row ${gridViewHasTabs
            ? 'generic-grid-view__container--has-tabs'
            : 'generic-grid-view__container'
            }`}
        >
          <GenericGridView
            //onSettingsChange={categoryById}
            onSettingsChange={load}
            onExportSelected={getGenericGridViewForExport}
            onRowChange={update}
            {...props}
          />
        </div>
      </Fragment>
    );
  }
};

const mapStateToProps = ({ genericGridView }) => genericGridView;

const mapDispatchToProps = (dispatch, { type }) => ({
  categoryById: (params) => dispatch(categoryById(type, params)),
  load: (params) => dispatch(load(type, params)),
  update: (data, item) => dispatch(update(type, data, item)),
  getGenericGridViewForExport: () => dispatch(getGenericGridViewForExport()),
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(GenericGridViewContainer);
