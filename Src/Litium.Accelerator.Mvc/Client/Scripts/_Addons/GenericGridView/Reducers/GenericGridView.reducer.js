import {
    GENERIC_GRID_VIEW_LOAD,
    GENERIC_GRID_VIEW_RECEIVE,
    GENERIC_GRID_VIEW_SET_DATA_TYPE,
    GENERIC_GRID_VIEW_SET_NOT_LOGGED_ON,
    GENERIC_GRID_VIEW_ALL_ROWS,
    GENERIC_GRID_VIEW_SET_SEARCH_PENDING_STATUS,
    GENERIC_GRID_VIEW_UPDATE_CART_TABS,
    GENERIC_GRID_VIEW_GET_HEADER_INFORMATION_DATA,
    GENERIC_GRID_UPDATE_FIELDS,

    GENERIC_GRID_VIEW_SHOWEDITIMAGES,
    GENERIC_GRID_VIEW_IMAGES_WILLBEADDED,

    GENERIC_GRID_ROW_UPDATE,
    GENERIC_GRID_ROW_ERROR,
    GENERIC_GRID_ROW_RECEIVE,

    GENERIC_GRID_FIELD_UPDATE,
    GENERIC_GRID_FIELD_ERROR,
    GENERIC_GRID_FIELD_RECEIVE,
    GENERIC_GRID_FIELD_LIST_RECEIVE,
    GENERIC_GRID_FIELD_ROW_UPDATE,
    GENERIC_GRID_FIELD_GET_DATA_LIST,
    GENERIC_GRID_FIELD_LOAD_ERROR,
    GENERIC_GRID_FIELD_GET_ORGANIZATION_LIST,
} from '../constants';

const DEFAULT_STATE = {
  dataRows: [],
  isLoading: false,
  currentDataType: '',
  currentAutocompleteList: [],
  organizations: [],
  isSearching: false,
  gridViewTabs: null, // [],
  gridViewHasTabs: false,
  headerInformation: '',
  showModal: { open: false, index: 0 },
  imagesWillBeAdded: [],

  isNotLoggedOn: false, // We use reversed condition due to object maybe not found
};

export const genericGridView = (state = DEFAULT_STATE, { type, payload }) => {
  //console.log("Payload: " + "Type=" + type +'\n' + JSON.stringify(payload));

  switch (type) {
    case GENERIC_GRID_VIEW_SET_DATA_TYPE:
    case GENERIC_GRID_FIELD_GET_DATA_LIST:
    case GENERIC_GRID_FIELD_GET_ORGANIZATION_LIST:
    case GENERIC_GRID_VIEW_SET_SEARCH_PENDING_STATUS:
    case GENERIC_GRID_VIEW_SET_NOT_LOGGED_ON:
    case GENERIC_GRID_VIEW_ALL_ROWS:
    case GENERIC_GRID_VIEW_UPDATE_CART_TABS:
    case GENERIC_GRID_VIEW_GET_HEADER_INFORMATION_DATA:
      return {
        ...state,
        ...payload,
      };
    case GENERIC_GRID_VIEW_SHOWEDITIMAGES:
      const { open, index } = payload.showModal;
      return {
        ...state,
        showModal: { open, index },
      };
    case GENERIC_GRID_VIEW_IMAGES_WILLBEADDED:
      return {
        ...state,
        imagesWillBeAdded: payload.images,
      };
    case GENERIC_GRID_VIEW_LOAD:
      return { ...state, isLoading: true };
    case GENERIC_GRID_VIEW_RECEIVE:
      return {
        ...state,
        isLoading: false,
        ...payload,
      };
    case GENERIC_GRID_UPDATE_FIELDS:
      return {
        ...state,
        dataRows: payload.dataRows
      };
    case GENERIC_GRID_ROW_UPDATE:
      return {
        ...state,
        dataRows: state.dataRows.map((row) =>
          row.fields === payload.fields
            ? { ...row, isLoading: true, error: null }
            : row
        ),
      };
    case GENERIC_GRID_ROW_ERROR:
    case GENERIC_GRID_FIELD_LOAD_ERROR:
      return {
        ...state,
        dataRows: state.dataRows.map((row) =>
          row.fields === payload.fields
            ? {
              ...row,
              isLoading: false,
              ...payload,
            }
            : row
        ),
      };
    case GENERIC_GRID_ROW_RECEIVE:
      return {
        ...state,
        dataRows: state.dataRows.map((row) =>
          row.fields === payload.fields
            ? payload.response.fields
              ? payload.response
              : {
                ...row,
                isLoading: false,
                fields: payload.response,
              }
            : row
        ),
      };

    default:
      return state;
  }
};
