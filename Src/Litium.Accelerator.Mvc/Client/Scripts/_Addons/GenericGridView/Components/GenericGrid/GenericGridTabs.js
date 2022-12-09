import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { translate } from '../../../../Services/translation';
import {
  getGenericGridTabs,
  getGenericGridByTab,
} from '../../Actions/GenericGridView.action';

class GenericGridTabs extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isUpdating: false,
      dataRows: [],
    };
  }

  componentDidMount() {}

  componentDidUpdate(prevProps) {}

  onTabClick(id) {
    this.setState({ selectedTabId: id });
    const { getGenericGridByTab } = this.props;

    if (getGenericGridByTab) {
      getGenericGridByTab(id);
    }
  }

  render() {
    if (this.props.gridViewTabs && this.state.selectedTabId > 0) {
      if (selectedTabId > this.props.gridViewTabs.length - 1) {
        this.onTabClick(0);
      }
    }

    // Check if id contains in tab
    let selectedTabId = this.state.selectedTabId;
    if (
      this.props.gridViewTabs &&
      selectedTabId &&
      this.props.gridViewTabs.filter((tab) => tab.tabId === selectedTabId)
        .length < 1
    ) {
      selectedTabId = this.props.gridViewTabs[0].selectedTabId;
    }

    return (
      <Fragment>
        {this.props.gridViewTabs && this.props.gridViewTabs.length > 0 && (
          <nav className="tab__header-container">
            <ul>
              {this.props.gridViewTabs.map((tab, tabIndex) => (
                <li
                  className={`tab__header ${
                    (selectedTabId && selectedTabId === tab.tabId) ||
                    (!selectedTabId && tabIndex === 0)
                      ? 'tab__header--active'
                      : ''
                  }`}
                  onClick={(event) => this.onTabClick(tab.tabId)}
                  key={`tab${tabIndex}`}
                >
                  {tab.title}
                </li>
              ))}
            </ul>
          </nav>
        )}
      </Fragment>
    );
  }
}

const mapStateToProps = ({ genericGridView }) => genericGridView;

const mapDispatchToProps = (dispatch) => {
  return {
    getGenericGridTabs: () => dispatch(getGenericGridTabs()),
    getGenericGridByTab: (id) => dispatch(getGenericGridByTab(id)),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(GenericGridTabs);
