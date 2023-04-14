import React, { useEffect, Fragment } from 'react';
import { connect, useSelector, useDispatch } from 'react-redux';
import { translate } from '../../../../Services/translation';
import { getGenericDataViewForExport } from '../../Actions/GenericDataView.action';

const ExportButton = () => {
  const dispatch = useDispatch();
  const genericGridView = useSelector((state) => state.genericGridView);

  function exportData(event) {
    event.preventDefault();
      dispatch(getGenericDataViewForExport());
  }

  return (
    <Fragment>
      <button className="generic-data-view__button-export secondary" onClick={(event) => exportData(event)} >
        {translate('addons.genericgridview.exportbutton.text')}
      </button>
    </Fragment>
  )
}

export default ExportButton


// class ExportButton0 extends React.Component {
//   constructor(props) {
//     super(props);
//     this.state = { ...props };
//   }

//   componentDidMount() {
//     document.addEventListener('mousedown', this.clickHandler);
//   }

//   componentWillUnmount() {
//     document.removeEventListener('mousedown', this.clickHandler);
//   }

//   clickHandler(event) {
//     this.lastClickedNode = event.target;
//   }

//   exportData() {
//     const { getGenericGridViewForExport } = this.props;
//     console.log('exportData ', getGenericGridViewForExport);
//     if (getGenericGridViewForExport) {
//       getGenericGridViewForExport();
//     }
//   }

//   componentWillUpdate({ showFullForm } = props) { }

//   render() {
//     const { } = this.props;
//     return (
//       <Fragment>
//         <button
//           className="generic-data-view__button-export secondary"
//           onClick={(event) => this.exportData()}
//         >
//           {translate('addons.genericgridview.exportbutton.text')}
//         </button>
//       </Fragment>
//     );
//   }
// }

// const mapStateToProps = ({ genericGridView }) => genericGridView;

// const mapDispatchToProps = (dispatch) => {
//   return {
//     getGenericGridViewForExport: () => dispatch(getGenericGridViewForExport()),
//   };
// };

// export default connect(mapStateToProps, mapDispatchToProps)(ExportButton0);