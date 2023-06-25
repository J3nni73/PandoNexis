import {
    GENERIC_DATA_VIEW_LOAD,
    GENERIC_DATA_VIEW_RECEIVE,
    GENERIC_DATA_VIEW_SET_CURRENT_PAGE_ID,
    GENERIC_DATA_VIEW_SET_DATA_TYPE,
    GENERIC_DATA_VIEW_SET_NOT_LOGGED_ON,
    GENERIC_DATA_VIEW_ALL_ROWS,
    GENERIC_DATA_VIEW_SET_SEARCH_PENDING_STATUS,
    GENERIC_DATA_VIEW_UPDATE_CART_TABS,
    GENERIC_DATA_VIEW_GET_HEADER_INFORMATION_DATA,
    GENERIC_DATA_UPDATE_FIELDS,

    GENERIC_DATA_VIEW_SHOWEDITIMAGES,
    GENERIC_DATA_VIEW_IMAGES_WILLBEADDED,

    GENERIC_DATA_CONTAINER_UPDATE,
    GENERIC_DATA_CONTAINER_ERROR,
    GENERIC_DATA_CONTAINER_RECEIVE,

    // Modal
    GENERIC_DATA_VIEW_SHOW_MODAL,
    GENERIC_MODAL_DATA_CONTAINER_RECEIVE,
    GENERIC_MODAL_DATA_CONTAINER_UPDATE,
    GENERIC_MODAL_DATA_UPDATE_FIELDS,

    GENERIC_MODAL_DATA_VIEW_ALL_ROWS,
    GENERIC_MODAL_DATA_VIEW_RECEIVE,
    GENERIC_MODAL_DATA_VIEW_INIT,

    GENERIC_DATA_FIELD_UPDATE,
    GENERIC_DATA_FIELD_ERROR,
    GENERIC_DATA_FIELD_RECEIVE,
    GENERIC_DATA_FIELD_LIST_RECEIVE,
    GENERIC_DATA_FIELD_CONTAINER_UPDATE,
    GENERIC_DATA_FIELD_GET_DATA_LIST,
    GENERIC_DATA_FIELD_LOAD_ERROR,
    GENERIC_DATA_FIELD_GET_ORGANIZATION_LIST,
} from '../constants';

const DEFAULT_STATE = {
    modalDataContainers: [],
    dataContainers: [],
    isLoading: false,
    currentPageId: '',
    currentDataType: '',
    displayTypes: 'table',
    settings: {},
    modalSettings: {
        modalDisplayTypes: 'cards',
        modalPageSystemId: '',
        cardColumnsSmall: '1',
        cardColumnsMedium: '2',
        cardColumnsLarge: '3',
    },

    currentAutocompleteList: [],
    organizations: [],
    isSearching: false,
    dataViewTabs: null, // [],
    dataViewHasTabs: false,
    headerInformation: '',
    pnModal: { open: false, index: 0 },
    imagesWillBeAdded: [],

    isNotLoggedOn: false, // We use reversed condition due to object maybe not found
};

export const genericDataView = (state = DEFAULT_STATE, { type, payload }) => {
    //console.log('Payload: ' + 'Type=' + type +'\n' + JSON.stringify(payload));

    switch (type) {
        case GENERIC_DATA_VIEW_SET_CURRENT_PAGE_ID:
        case GENERIC_DATA_VIEW_SET_DATA_TYPE:
        case GENERIC_DATA_FIELD_GET_DATA_LIST:
        case GENERIC_DATA_FIELD_GET_ORGANIZATION_LIST:
        case GENERIC_DATA_VIEW_SET_SEARCH_PENDING_STATUS:
        case GENERIC_DATA_VIEW_SET_NOT_LOGGED_ON:
        case GENERIC_DATA_VIEW_ALL_ROWS:
        case GENERIC_MODAL_DATA_VIEW_ALL_ROWS:
        case GENERIC_DATA_VIEW_UPDATE_CART_TABS:
        case GENERIC_DATA_VIEW_GET_HEADER_INFORMATION_DATA:
        case GENERIC_MODAL_DATA_VIEW_INIT:
            return {
                ...state,
                ...payload,
            };
        case GENERIC_DATA_VIEW_SHOW_MODAL:
        case GENERIC_DATA_VIEW_SHOWEDITIMAGES:
            const { open, index } = payload.pnModal;
            return {
                ...state,
                pnModal: { open, index },
            };
        case GENERIC_DATA_VIEW_IMAGES_WILLBEADDED:
            return {
                ...state,
                imagesWillBeAdded: payload.images,
            };
        case GENERIC_DATA_VIEW_LOAD:
            return { ...state, isLoading: true };
        case GENERIC_DATA_VIEW_RECEIVE:
            return {
                ...state,
                isLoading: false,
                ...payload,
            };
        case GENERIC_DATA_UPDATE_FIELDS:
            return {
                ...state,
                dataContainers: payload.dataContainers
            };
        case GENERIC_DATA_CONTAINER_UPDATE:
            return {
                ...state,
                dataContainers: state.dataContainers.map((dataContainer) =>
                    dataContainer.fields === payload.fields
                        ? { ...dataContainer, isLoading: true, error: null }
                        : dataContainer
                ),
            };
        case GENERIC_MODAL_DATA_UPDATE_FIELDS:
        case GENERIC_MODAL_DATA_VIEW_RECEIVE:
            return {
                ...state,
                modalDataContainers: payload.dataContainers,
                modalSettings: payload.settings
            };
        case GENERIC_MODAL_DATA_CONTAINER_UPDATE:
            return {
                ...state,
                modalDataContainers: state.modalDataContainers.map((dataContainer) =>
                    dataContainer.fields === payload.fields
                        ? { ...dataContainer, isLoading: true, error: null }
                        : dataContainer
                ),
            };
        case GENERIC_DATA_CONTAINER_ERROR:
        case GENERIC_DATA_FIELD_LOAD_ERROR:
            return {
                ...state,
                dataContainers: state.dataContainers.map((dataContainer) =>
                    dataContainer.fields === payload.fields
                        ? {
                            ...dataContainer,
                            isLoading: false,
                            ...payload,
                        }
                        : dataContainer
                ),
            };
        case GENERIC_DATA_CONTAINER_RECEIVE:
            return {
                ...state,
                dataContainers: state.dataContainers.map((dataContainer) =>
                    dataContainer.fields === payload.fields
                        ? payload.response.fields
                            ? payload.response
                            : {
                                ...dataContainer,
                                isLoading: false,
                                fields: payload.response,
                            }
                        : dataContainer
                ),
            };
        case GENERIC_MODAL_DATA_CONTAINER_RECEIVE:
            return {
                ...state,
                modalDataContainers: state.modalDataContainers.map((dataContainer) =>
                    dataContainer.fields === payload.fields
                        ? payload.response.fields
                            ? payload.response
                            : {
                                ...dataContainer,
                                isLoading: false,
                                fields: payload.response,
                            }
                        : dataContainer
                ),
            };
        default:
            return state;
    }
};
