import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { translate } from '../../../../../Services/translation';
import {
  autocompleteGetList,
  setFieldData,
} from '../../../Actions/GenericGridSpecialField.action';
class AutocompleteField extends Component {
  constructor(props) {
    super(props);
    this.state = { ...props };
    this.clickHandler = this.clickHandler.bind(this);
    this.lastClickedNode = null;
    this.autocompleteList = [];
    this.state.showAutocomplete = false;
    this.state.selectedValueObject = null;
    this.state.updating = false;
    this.state.toFewChars = true;
  }

  componentDidMount() {
    document.addEventListener('mousedown', this.clickHandler);
  }

  componentWillUnmount() {
    document.removeEventListener('mousedown', this.clickHandler);
  }

  clickHandler(event) {
    this.lastClickedNode = event.target;
  }

  onBlur() {
    const scope = this;
    if (!this.autocompleteContainer.contains(this.lastClickedNode)) {
      scope.setState({ ...scope.state, showAutocomplete: false });
    }
  }

  getList(query, removeSelectedValue = false) {
    const scope = this;
    scope.setState({ showAutocomplete: true });
    let minimumCharsRequired = this.props.fieldSettings.minimumCharsRequired;
    if (!minimumCharsRequired) {
      minimumCharsRequired = 3;
    }
    const toFew = query.length < minimumCharsRequired;
    scope.state.toFewChars = toFew;

    if (query.length < minimumCharsRequired || this.state.updating) {
      return;
    }

    const fieldId = this.props.fieldId;

    if (removeSelectedValue) {
      this.props.autocompleteGetList(fieldId, query);
      scope.state.selectedValueObject = null;
    }
  }

  getClosest(elem, selector) {
    // Element.matches() polyfill
    if (!Element.prototype.matches) {
      Element.prototype.matches =
        Element.prototype.matchesSelector ||
        Element.prototype.mozMatchesSelector ||
        Element.prototype.msMatchesSelector ||
        Element.prototype.oMatchesSelector ||
        Element.prototype.webkitMatchesSelector ||
        function (s) {
          var matches = (this.document || this.ownerDocument).querySelectorAll(
              s
            ),
            i = matches.length;
          while (--i >= 0 && matches.item(i) !== this) {}
          return i > -1;
        };
    }

    // Get the closest matching element
    for (; elem && elem !== document; elem = elem.parentNode) {
      if (elem.matches(selector)) return elem;
    }
    return null;
  }

  setValue(event, val, name) {
    const entitySystemId = this.props.entitySystemId;
    const scope = this;
    this.setState({
      ...this.state,
      selectedValueObject: { value: val, name, entitySystemId },
      showAutocomplete: false,
      updating: true,
    });

    const target = event.target;
    const parentEl = this.getClosest(
      target,
      '.generic-grid-view__autocomplete-container'
    );

    var inputEl = parentEl.querySelector('.generic-grid-view__input');
    if (inputEl) {
      inputEl.value = name;
    }
    setTimeout(function () {
      scope.setState({ ...scope.state, updating: false });
    }, 1000);
  }

  postValue() {
    const rowIndex = this.props.rowIndex;
    //fieldId, value, rowIndex, entitySystemId
    if (this.state.selectedValueObject) {
      const fieldId = this.props.fieldId;
      const { setFieldData } = this.props;
      if (setFieldData) {
        setFieldData(fieldId, rowIndex, this.state.selectedValueObject);
      }
      // this.props.autocompleteGetList(this.state.selectedValueObject);
    }
  }

  componentWillUpdate({ showFullForm } = props) {}

  render() {
    const {
      fieldId,
      entitySystemId,
      defaultValue,
      fieldSettings,
      rowIndex,
      currentAutocompleteList,
      isSearching,
    } = this.props;
    return (
      <div
        className="generic-grid-view__autocomplete-container"
        ref={(elem) => (this.autocompleteContainer = elem)}
        onBlur={(event) => this.onBlur()}
      >
        <input
          className="generic-grid-view__input"
          onChange={(event) => this.getList(event.target.value, true)}
          onFocus={(event) => this.getList(event.target.value)}
          placeholder={defaultValue || fieldSettings.placeholderText}
        />
        {this.state.selectedValueObject && (
          <button
            className="generic-grid-view__autocomplete-post-button"
            onClick={(event) => this.postValue()}
          >
            {fieldSettings.buttonText}
          </button>
        )}

        <ul
          className={`generic-grid-view__autocomplete-list ${
            this.state.showAutocomplete
              ? 'generic-grid-view__autocomplete-list--active'
              : ''
          }`}
        >
          {!this.state.toFewChars &&
            !isSearching &&
            currentAutocompleteList &&
            currentAutocompleteList.map((item, autocompleteIndex, array) => (
              <Fragment key={`autocompleteListIndex-${autocompleteIndex}`}>
                <li
                  className=""
                  onClick={(event) =>
                    this.setValue(event, item.value, item.name)
                  }
                >
                  <span>{item.name}</span>
                </li>
              </Fragment>
            ))}
          {(this.state.toFewChars ||
            currentAutocompleteList.length === 0 ||
            isSearching) &&
            this.state.showAutocomplete && (
              <Fragment>
                <li className="generic-grid-view__autocomplete-empty-list">
                  <span>
                    {this.state.toFewChars ? (
                      <Fragment>
                        {translate(
                          'common.articlerelation.autocomplete.minchars'
                        ).replace(
                          '[x]',
                          fieldSettings.minimumCharsRequired || '3'
                        )}
                      </Fragment>
                    ) : (
                      <Fragment>
                        {isSearching ? (
                          <Fragment>
                            {translate(
                              'common.articlerelation.autocomplete.searchpendingtext'
                            )}
                          </Fragment>
                        ) : (
                          <Fragment>{fieldSettings.emptyResultText}</Fragment>
                        )}
                      </Fragment>
                    )}
                  </span>
                </li>
              </Fragment>
            )}
        </ul>
      </div>
    );
  }
}

const mapStateToProps = ({ genericGridView }) => genericGridView;

const mapDispatchToProps = (dispatch) => {
  return {
    autocompleteGetList: (fieldId, query) =>
      dispatch(autocompleteGetList(fieldId, query)),
    setFieldData: (fieldId, rowIndex, setValueObject) =>
      dispatch(setFieldData(fieldId, rowIndex, setValueObject)),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(AutocompleteField);
