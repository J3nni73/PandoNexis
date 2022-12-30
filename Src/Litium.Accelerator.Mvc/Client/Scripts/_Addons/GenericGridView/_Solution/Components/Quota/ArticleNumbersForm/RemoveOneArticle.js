import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import SweetAlert2, { withSwal } from 'react-sweetalert2';
import { translate } from '../../../../../../Services/translation';
import { removeArticleFromOuotaCategory } from '../../../Actions/Quota/QuotaQuickSearch.action';
import useBtnInProgress from '../../../../Hooks/useBtnInProgress';


const RemoveOneArticle = ({ variant, type, query }) => {

    const dispatch = useDispatch();
    const { setInProgress } = useBtnInProgress();

    const [show, setShow] = useState(false)
    const [swalProps2, setSwalProps2] = useState({
        show: true,
        title: 'Deleted!',
        text: 'Your file has been deleted.',
        icon: 'success',
        timer: 1000,
        showCancelButton: false,
        showConfirmButton: false
    });
    const [swalProps, setSwalProps] = useState();

    function handleRemoveArticle(e) {
        e.preventDefault();
        setSwalProps({
            show: true,
            title: `Selected Article from Quota ${query}`,
            text: 'Want to delete article number?',
            showCancelButton: true,
            confirmButtonText: 'OK',
            position: 'top',
            didOpen: () => {
                // run when swal is opened...
            },
            didClose: () => {
                // run when swal is closed...
                // setShow(false)
            },
            onConfirm: (result) => {
                // run when clieked in confirm and promise is resolved...
                if (result.isConfirmed) {
                    const data = [
                        {
                            articleNumber: variant.id,
                            baseProductSystemId: variant.baseProductSystemId,
                            quotaId: query,
                        },
                    ];
                    setShow(true);
                    setTimeout(() => {
                        dispatch(removeArticleFromOuotaCategory(type, data));
                    }, 200);
                }
            },
            onError: (error) => {
                // run when promise rejected...
            },
            onResolve: (result) => {
                // run when promise is resolved...
                setSwalProps({})
            }
        });
    }

    return (
        <div>
            <span className="article-numbers__remove" name={variant?.id} {...setInProgress(variant?.id)} onClick={(event) => handleRemoveArticle(event, variant)} >
                {/* <img className="generic-grid-view__organization-button" alt="Remove" src="/ui/images/bin.svg" /> */}
                <span className='action__icon action__icon--delete' title={translate('Remove')} ></span>
            </span>
            {/* {show && <SweetAlert2 {...swalProps2} />} */}
            <SweetAlert2 {...swalProps}>
                <ul>
                    <li key={variant.id}> {variant?.id} </li>
                </ul>
            </SweetAlert2>
        </div>
    );
}

export default RemoveOneArticle