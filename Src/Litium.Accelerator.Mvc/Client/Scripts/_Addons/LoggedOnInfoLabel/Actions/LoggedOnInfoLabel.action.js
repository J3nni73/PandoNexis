import { get, post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';
import { LOGGED_ON_INFO_LABEL_LOAD, LOGGED_ON_INFO_LABEL_ERROR } from '../constants';

const rootRoute = '/api/loggedoninfolabel';

export const getPersonInfo = () => (dispatch, getState) => {
    get(rootRoute + '/getpersoninfo')
        .then(response => response.json())
        .then(personInfo => dispatch(setPersonInfo(personInfo)))
        .catch(ex => dispatch(catchError(ex, error => searchError(error))));
}

export const searchError = error => ({
    type: LOGGED_ON_INFO_LABEL_ERROR,
    payload: {
        error,
    }
})

export const setPersonInfo = personInfo => ({
    type: LOGGED_ON_INFO_LABEL_LOAD,
    payload: {
        personInfo,
    }
})
