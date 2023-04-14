import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
class IconField extends Component {

    constructor(props) {
        super(props);
        this.state = { ...props };
        this.clickHandler = this.clickHandler.bind(this);
        this.lastClickedNode = null;
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

    componentWillUpdate({ showFullForm } = props) {

    }

    render() {
        const { fieldId, entitySystemId, defaultValue, fieldSettings, rowIndex } = this.props;
        return (
            <Fragment>
                {defaultValue && defaultValue.length > 0 && <i className={defaultValue} title={fieldSettings.buttonText || ''}></i>}
            </Fragment>

        );
    }
}

const mapStateToProps = ({ genericGridView }) => genericGridView;

const mapDispatchToProps = dispatch => {
    return {

    }
}


export default connect(mapStateToProps, mapDispatchToProps)(IconField);