import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { getOrganizationData, setFieldData } from '../../../Actions/GenericGridSpecialField.action';
class OrganizationSelection extends Component {

    constructor(props) {
        super(props);
        this.state = { ...props };
        this.state.showOrgSelection = false;
    }

    componentDidMount() {
    
    }

    componentWillUnmount() {
    
    }

    
    componentWillUpdate({ showFullForm } = props) {

    }

    toggleSelection(entitySystemId) {
        this.props.getData(entitySystemId);
    }

    closeSelection(event) {
        const { nodes} = this.props.organizations;
        this.props.setData('OrganizationSelection', this.props.rowIndex,
            {
                value: JSON.stringify(nodes),
                entitySystemId: event.target.dataset.entitysystemid
            }
        );
        this.props.getData(); //empty will close popup
    }

    setCheckbox(event){
        const { nodes} = this.props.organizations;
        
        var node = nodes.find(i=>i.id == event.target.dataset.id);
        node.value = !node.value;
        if (event.target.dataset.index == 0){
            for (var i= 1;i<nodes.length;i++)
                nodes[i].value = node.value;
        }
        else if (!node.value){
            nodes[0].value = node.value;
        }
        else {
            var allOn = true;
            for (var i=1;i<nodes.length;i++){
                if (!nodes[i].value)
                    allOn = false;
            }
            nodes[0].value = allOn;
        }
        this.setState({...this.state, nodes: nodes});
    }

    render() {
        const { entitySystemId, defaultValue, organizations } = this.props;
        return (
            <Fragment>
            <div className="generic-grid-view__button-field-container" onClick={(event) => this.toggleSelection(entitySystemId)}>
                <img className="generic-grid-view__organization-button" src={`/ui/images/${defaultValue}.png`}/>
            </div>
            {organizations && organizations.entitySystemId === entitySystemId &&
                <div className="generic-grid-view__organization-selection">
                    {organizations.nodes && organizations.nodes.map((item, index) => (
                        <div key={`org-${index}`}>
                            <input type="checkbox" data-id={item.id} data-index={index} checked={item.value} onChange={(event) => this.setCheckbox(event)}/>
                            <label htmlFor={item.id}>{item.name}</label>
                        </div>
                    ))}
                    <i className="fas fa-times-circle" data-entitysystemid={organizations.entitySystemId} onClick={(event) => this.closeSelection(event)}></i>
                </div>
            }
            </Fragment>
        );
    }
}

const mapStateToProps = ({ genericGridView }) => genericGridView;

const mapDispatchToProps = dispatch => {
    return {
        getData: (entitySystemId) => dispatch(getOrganizationData(entitySystemId)),
        setData: (fieldId, rowIndex, nodes) => dispatch(setFieldData(fieldId, rowIndex, nodes))
    }
}


export default connect(mapStateToProps, mapDispatchToProps)(OrganizationSelection);